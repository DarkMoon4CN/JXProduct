using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using PanGu;
using Lucene.Net.Index;
using Lucene.Net.Documents;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Search;
using Lucene.Net.QueryParsers;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.PanGu;

using JXSearch.Engine.Cache;
using JXAPI.Common.Config;

namespace JXSearch.Engine.Keywords
{
    internal class KeywordSearchProvider : JXSearchProvider
    {
        private static string key = "KeywordEngine";
        private EnginesElement config = SearchEngineConfig.Instance.Engines[key];

        public override string INDEX_DIR
        {
            get { return config.IndexDir; }
        }

        public override string CURR_INDEX_DIR
        {
            get
            {
                SearchDictDirCacheProvider.SearchDictDirCache info = SearchDictDirCacheProvider.Get(key);
                if (info != null)
                    return info.dictDir;
                else
                    return string.Format("{0}/{1}", config.IndexDir, config.DictDir);
            }
        }

        public override string DICT_DIR
        {
            get { return config.DictDir; }
        }

        /// <summary>
        /// 关键词搜索
        /// </summary>
        /// <param name="q">关键词</param>
        /// <param name="pageLen">条数</param>
        /// <param name="pageNo">页码</param>
        /// <param name="recCount">总记录数</param>
        /// <returns>搜索结果</returns>
        public override JXSearchEntityResult Search(string q, int pageLen, int pageNo, out int recCount)
        {
            IndexSearcher search = new IndexSearcher(CURR_INDEX_DIR);

            //  关键词检索字段
            string[] fields = { "ChineseName", "PinyinName" };
            BooleanClause.Occur[] flags = new BooleanClause.Occur[fields.Length];
            for (int n = 0; n < fields.Length; n++)
            {
                flags[n] = BooleanClause.Occur.SHOULD;
            }

            //  关键词检索
            //  BooleanClause.Occur.MUST 表示 and
            //  BooleanClause.Occur.MUST_NOT 表示 not
            //  BooleanClause.Occur.SHOULD 表示 or
            q = GetKeyWordsSplitBySpace(q, new PanGuTokenizer());
            Query query1 = MultiFieldQueryParser.Parse(Lucene.Net.Util.Version.LUCENE_29, q, fields, flags, new PanGuAnalyzer(true));
            BooleanQuery query = new BooleanQuery();
            query.Add(query1, BooleanClause.Occur.MUST);

            //  查询
            Hits hits = search.Search(query);

            //  绑定
            recCount = hits.Length();       //  总记录数 
            int i = (pageNo - 1) * pageLen;
            List<JXSearchEntity> result = new List<JXSearchEntity>();
            while (i < recCount && result.Count < pageLen)
            {
                JXSearchEntity info = null;
                try
                {
                    info = new JXSearchEntity();
                    info.id = int.Parse(hits.Doc(i).Get("KeywordID"));
                    info.chineseName = hits.Doc(i).Get("ChineseName");
                    info.productCount = int.Parse(hits.Doc(i).Get("ProductCount"));
                    info.typeID = int.Parse(hits.Doc(i).Get("TypeID"));
                }
                catch { }
                finally
                {
                    if (info != null)
                        result.Add(info);
                    i++;
                }
            }
            //  关闭搜索
            search.Close();

            //  返回
            if (result == null || result.Count <= 0)
                return null;
            else
            {
                return new JXSearchEntityResult()
                {
                    listKeyword = result,
                    totalPage = Convert.ToInt32(Math.Ceiling((double)recCount / pageLen)),
                    resultCode = "SUCCEED",
                    resultMsg = "SUCCEED"
                };
            };
        }

        public override JXSearchProductResult Search(RequestForm form, out int recCount)
        {
            recCount = 0;
            return null;
        }
    }

}
