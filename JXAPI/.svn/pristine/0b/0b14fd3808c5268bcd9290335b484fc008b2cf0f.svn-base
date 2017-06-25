using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using PanGu;
using Lucene.Net.Index;
using Lucene.Net.Documents;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Search;
using Lucene.Net.QueryParsers;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.PanGu;

using System.Data;
using System.Collections;
using System.Text.RegularExpressions;
using JXAPI.Common.Utils;

namespace JXSearch.Engine
{
    public abstract class JXSearchProvider
    {
        /// <summary>
        /// 索引目录
        /// </summary>
        public abstract string INDEX_DIR { get; }

        /// <summary>
        /// 当前索引目录
        /// </summary>
        public abstract string CURR_INDEX_DIR { get; }

        public abstract string DICT_DIR { get; }

        private static IndexWriter writer = null;

        public int MaxMergeFactor
        {
            get
            {
                if (null != writer)
                    return writer.GetMergeFactor();
                else
                    return 0;
            }
            set
            {
                if (null != writer)
                    writer.SetMergeFactor(value);
            }
        }

        public int MaxMergeDocs
        {
            get
            {
                if (null != writer)
                    return writer.GetMaxMergeDocs();
                else
                    return 0;
            }
            set
            {
                if (null != writer)
                    writer.SetMaxMergeDocs(value);
            }
        }

        public int MinMergeDocs
        {
            get
            {
                if (null != writer)
                    return writer.GetMaxBufferedDocs();
                else
                    return 0;
            }
            set
            {
                if (null != writer)
                    writer.SetMaxBufferedDocs(value);
            }
        }

        /// <summary>
        /// 创建索引
        /// </summary>
        /// <param name="indexDir"></param>
        public void CreateIndex(String indexDir)
        {
            try
            {
                writer = new IndexWriter(indexDir, new PanGuAnalyzer(), false);     //  重新索引
            }
            catch {
                writer = new IndexWriter(indexDir, new PanGuAnalyzer(), true);      //  增量索引
            }
        }

        /// <summary>
        /// 重建索引
        /// </summary>
        /// <param name="indexDir"></param>
        public void Rebuild(String indexDir)
        {
            writer = new IndexWriter(indexDir, new PanGuAnalyzer(), true);
        }

        /// <summary>
        /// 创建索引文档
        /// </summary>
        /// <param name="info">商品信息</param>
        /// <returns></returns>
        private Document CreateDocument(DataRow info)
        {
            Document doc = new Document();

            //  商品名称
            Field field = new Field("ChineseName", info["ChineseName"].ToString(), Field.Store.YES, Field.Index.ANALYZED);
            float boost = 30f;
            if (info["ProductType"].ToString() == "0" || info["ProductType"].ToString() == "3")
            {
                boost -= 20f;
            }
            field.SetBoost(boost);
            doc.Add(field);

            //  商品标签
            field = new Field("ProductTag", info["ProductTag"].ToString(), Field.Store.NO, Field.Index.ANALYZED);
            field.SetBoost(10f);
            doc.Add(field);

            //  功能主治
            doc.Add(new Field("Efficacy", info["Efficacy"].ToString(), Field.Store.NO, Field.Index.ANALYZED));

            //  关键字
            string pattern = "[“”；。、，. %［ ］()（）Ⅱ,*：X+-]";
            string keywords = Regex.Replace(info["Keywords"].ToString(), pattern, " ");
            field = new Field("Keywords", keywords, Field.Store.NO, Field.Index.ANALYZED);
            doc.Add(field);

            //  通用名
            string cadn = Regex.Replace(info["CADN"].ToString(), pattern, " ");
            field = new Field("CADN", cadn, Field.Store.YES, Field.Index.ANALYZED);
            doc.Add(field);

            //  拼音  【商品名称、通用名】
            string chineseName = Regex.Replace(info["ChineseName"].ToString(), pattern, " ");
            pattern = "[a-zA-Z0-9袋盒片剂粒]";
            string combinName = Regex.Replace(string.Format("{0} {1}", chineseName, cadn), pattern, " ");
            field = new Field("PinyinName", PinyinUtil.ConvertToPinyin(GetKeyWordsSplitFilter(combinName, new PanGuTokenizer())), Field.Store.YES, Field.Index.ANALYZED);
            doc.Add(field);
            
            //  名称截取
            field = new Field("ChineseNameWord", GetWordByChineseName(chineseName), Field.Store.YES, Field.Index.ANALYZED);
            doc.Add(field);

            //  长名称、厂商
            doc.Add(new Field("LongName", info["LongName"].ToString(), Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("Manufacturer", StringUtil.FilterManufacter(info["Manufacturer"].ToString()), Field.Store.YES, Field.Index.ANALYZED));

            //  其他索引信息  【评论、上架、上架时间、销量】
            doc.Add(new Field("Favorite", info["Favorite"].ToString(), Field.Store.YES, Field.Index.NO));
            doc.Add(new Field("Comments", info["Comments"].ToString(), Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("Selling", info["Selling"].ToString(), Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("SellingTime", info["SellingTime"].ToString(), Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("Stock", (int.Parse(info["Stock"].ToString()) > 0 ? "1" : "0"), Field.Store.NO, Field.Index.ANALYZED));
            doc.Add(new Field("SellCount", info["SellCount"].ToString(), Field.Store.YES, Field.Index.NO));
            //  精品推荐（毛利率）
            decimal tradePrice = decimal.Parse(info["TradePrice"].ToString());
            decimal profit = tradePrice - decimal.Parse(info["CostPrice"].ToString());
            doc.Add(new Field("Recommend", profit.ToString("0.00"), Field.Store.YES, Field.Index.NO));
            //  优惠推荐（折扣）
            decimal discount = 0;
            if (tradePrice > 0)
            {
                discount = tradePrice / decimal.Parse(info["MarketPrice"].ToString());
            }
            doc.Add(new Field("Preferential", discount.ToString("0.00"), Field.Store.YES, Field.Index.NO));
            
            //  商品基本信息
            doc.Add(new Field("ProductID", info["ProductID"].ToString(), Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("ProductCode", info["ProductCode"].ToString(), Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("ProductType", info["ProductType"].ToString(), Field.Store.YES, Field.Index.NO));
            doc.Add(new Field("Specifications", info["Specifications"].ToString(), Field.Store.YES, Field.Index.NO));
            doc.Add(new Field("Images", info["Images"].ToString(), Field.Store.YES, Field.Index.NO));
            doc.Add(new Field("Actions", info["Actions"].ToString(), Field.Store.YES, Field.Index.NO));
            doc.Add(new Field("MarketPrice", info["MarketPrice"].ToString(), Field.Store.YES, Field.Index.NO));
            doc.Add(new NumericField("TradePrice", Field.Store.YES, true).SetDoubleValue(double.Parse(info["TradePrice"].ToString())));

            //  品牌名称
            doc.Add(new Field("BrandID", info["BrandID"].ToString(), Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("BrandName", info["BrandName"].ToString(), Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("BrandLetter", info["BrandLetter"].ToString().ToUpper(), Field.Store.YES, Field.Index.NO));

            //  一级类目
            doc.Add(new Field("CFID1", info["CFID1"].ToString(), Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("ParentID1", info["ParentID1"].ToString(), Field.Store.YES, Field.Index.NO));
            field = new Field("CFName1", info["CFName1"].ToString(), Field.Store.YES, Field.Index.ANALYZED);
            field.SetBoost(10f);
            doc.Add(field);
            //  二级类目
            doc.Add(new Field("CFID2", info["CFID2"].ToString(), Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("ParentID2", info["ParentID2"].ToString(), Field.Store.YES, Field.Index.NO));
            field = new Field("CFName2", info["CFName2"].ToString(), Field.Store.YES, Field.Index.ANALYZED);
            field.SetBoost(10f);
            doc.Add(field);
            //  三级类目
            doc.Add(new Field("CFID3", info["CFID3"].ToString(), Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("ParentID3", info["ParentID3"].ToString(), Field.Store.YES, Field.Index.NO));
            field = new Field("CFName3", info["CFName3"].ToString(), Field.Store.YES, Field.Index.ANALYZED);
            field.SetBoost(10f);
            doc.Add(field);

            return doc;
        }

        /// <summary>
        /// 创建索引
        /// </summary>
        /// <param name="info">索引信息</param>
        /// <returns></returns>
        private Document CreateKeywordDocument(DataRow info)
        {
            Document doc = new Document();
            //  名称
            Field field = new Field("ChineseName", info["ChineseName"].ToString(), Field.Store.YES, Field.Index.ANALYZED);
            field.SetBoost(30f);
            doc.Add(field);

            //  拼音 
            field = new Field("PinyinName", PinyinUtil.ConvertToPinyin(info["ChineseName"].ToString()), Field.Store.NO, Field.Index.ANALYZED);
            field.SetBoost(10f);
            doc.Add(field);

            //  其他索引信息
            doc.Add(new Field("ProductCount", info["ProductCount"].ToString(), Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("TypeID", info["TypeID"].ToString(), Field.Store.YES, Field.Index.ANALYZED));

            //  商品基本信息
            doc.Add(new Field("KeywordID", info["KeywordID"].ToString(), Field.Store.YES, Field.Index.ANALYZED));
            return doc;
        }
        
        /// <summary>
        /// 商品名称切词
        /// </summary>
        /// <param name="chineseName"></param>
        /// <returns></returns>
        public string GetWordByChineseName(string chineseName)
        {
            try
            {
                //  替换无效字符
                Regex regex = new Regex("[a-zA-Z0-9-/Ⅱ.*:+() （）Ⅰ【】]+", RegexOptions.Compiled);
                chineseName = regex.Replace(chineseName, " ");

                //  双字节直接退出
                int i = chineseName.Length;
                if (i == 2)
                {
                    return string.Empty;
                }

                //  遍历字符串
                StringBuilder s = new StringBuilder();
                string tempWord = string.Empty;
                string tempChineseName = chineseName.Substring(1);
                while (i > 2)
                {
                    //  顺序截取
                    tempWord = chineseName.Substring(0, 2);
                    if (tempWord.Trim().Length > 1)
                    {
                        s.Append(tempWord.Trim() + " ");
                    }
                    chineseName = chineseName.Substring(2);

                    //  递减首字
                    tempWord = tempChineseName.Substring(0, 2);
                    if (tempWord.Trim().Length > 1)
                    {
                        s.Append(tempWord.Trim() + " ");
                    }
                    tempChineseName = tempChineseName.Substring(2);
                    i = i - 2;
                }
                return s.ToString();
            }
            catch { return string.Empty; }
        }

        /// <summary>
        /// 索引文档
        /// </summary>
        /// <param name="indexDir">索引目录</param>
        /// <param name="info">商品对象</param>
        /// <returns></returns>
        public int IndexString(DataRow info, JXSearchType t)
        {
            switch (t)
            {
                case JXSearchType.Product:
                    writer.AddDocument(CreateDocument(info));
                    break;
                default:
                    writer.AddDocument(CreateKeywordDocument(info));
                    break;
            }
            int num = writer.NumDocs();
            return num;
        }

        /*
        /// <summary>
        /// 删除商品索引
        /// </summary>
        /// <param name="indexDir">文件地址</param>
        /// <param name="ProductID">商品编码</param>
        public void DeleteProductIndex(String indexDir, int ProductID)
        {
            try
            {
                CreateIndex(indexDir);  //  增量索引
                Term term = new Term("ProductID", ProductID.ToString());
                writer.DeleteDocuments(term);
            }
            finally
            {
                Close();
            }
        }

        /// <summary>
        /// 修改商品索引
        /// </summary>
        /// <param name="indexDir">索引文件地址</param>
        /// <param name="info">商品信息</param>
        /// <returns></returns>
        public void UpdateProductIndex(String indexDir, DataRow info)
        {
            try
            {
                CreateIndex(indexDir);  //  增量索引
                Term term = new Term("ProductID", info["ProductID"].ToString());
                writer.UpdateDocument(term, CreateDocument(info));
            }
            finally
            {
                Close();
            }
        }
        */

        public void Close()
        {
            writer.Optimize();
            writer.Close();
        }

        public void CloseWithoutOptimize()
        {
            writer.Close();
        }

        public List<string> SplitKeyWords(string keywords, Analyzer analyzer)
        {
            System.IO.StreamReader reader = new System.IO.StreamReader(PanGu.Framework.Stream.WriteStringToStream(keywords, Encoding.UTF8), Encoding.UTF8);
            TokenStream tokenStream = analyzer.TokenStream("", reader);
            global::Lucene.Net.Analysis.Token token = tokenStream.Next();
            List<string> result = new List<string>();
            while (token != null)
            {
                result.Add(keywords.Substring(token.StartOffset(), token.EndOffset() - token.StartOffset()));
                token = tokenStream.Next();
            }
            return result;
        }

        public string GetKeyWordsSplitBySpace(string keywords, PanGuTokenizer ktTokenizer)
        {
            StringBuilder result = new StringBuilder();
            ICollection<WordInfo> words = ktTokenizer.SegmentToWordInfos(keywords);
            foreach (WordInfo word in words)
            {
                if (word == null)
                {
                    continue;
                }
                result.AppendFormat("{0}^{1}.0 ", word.Word, (int)Math.Pow(3, word.Rank));
            }
            return result.ToString().Trim();
        }

        /// <summary>
        /// 字符串分词
        /// </summary>
        /// <param name="keywords">字符串</param>
        /// <param name="ktTokenizer">分词</param>
        /// <returns></returns>
        public string GetKeyWordsSplitFilter(string keywords, PanGuTokenizer ktTokenizer)
        {
            //  拆分字符串
            List<string> list = new List<string>();
            ICollection<WordInfo> words = ktTokenizer.SegmentToWordInfos(keywords);
            foreach (WordInfo word in words)
            {
                if (word == null)
                    continue;
                else if (word.Word.Length > 1 && !list.Contains(word.Word))
                    list.Add(word.Word);
            }
            return string.Join(" ", list.ToArray());
        }
        
        /// <summary>
        /// 关键词搜索
        /// </summary>
        /// <param name="q">关键词</param>
        /// <param name="pageLen">条数</param>
        /// <param name="pageNo">页码</param>
        /// <param name="recCount">总记录数</param>
        /// <returns>搜索结果列表</returns>
        public abstract JXSearchEntityResult Search(string q, int pageLen, int pageNo, out int recCount);

        /// <summary>
        /// 关键词多条件搜索 商品
        /// </summary>
        /// <param name="keyword">关键词</param>
        /// <param name="brand">品牌</param>
        /// <param name="cfid">分类</param>
        /// <param name="pricelimit">价格区间</param>
        /// <param name="store">是否有货</param>
        /// <param name="order">排序</param>
        /// <param name="pageLen">条数</param>
        /// <param name="pageNo">页码</param>
        /// <param name="recCount">总记录数</param>
        /// <returns>搜索结果列表</returns>
        public abstract JXSearchProductResult Search(RequestForm form, out int recCount);
    }
}
