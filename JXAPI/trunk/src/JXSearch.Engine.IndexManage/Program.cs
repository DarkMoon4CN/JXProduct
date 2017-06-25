using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using JXSearch.Engine.Cache;

namespace JXSearch.Engine.IndexManage
{
    class Program
    {
        static DateTime currDate = DateTime.Now;
        static void Main(string[] args)
        {
            ////  索引
            CreateProductIndex();
            CreateKeywordIndex();
            CreateSearchKeywordIndex();

            //  推荐搜索关键词商品
            if (currDate.Day == 17)
            {
                UpdateSearchKeyword();
            }
            Console.WriteLine("=============================================");
            Console.Write("");
        }

        /// <summary>
        /// 创建商品索引
        /// </summary>
        private static void CreateProductIndex()
        {
            try
            {
                Console.WriteLine("Product === 开始生成索引");
                //  查询商品
                string strWhere = " AND p.[Status] = 0 AND p.Selling=1";
                DataTable dTable = JXAPI.Component.BLL.ProductBLL.Instance.Product_ListForSearch(strWhere);
                if (dTable == null || dTable.Rows.Count <= 0)
                {
                    Console.WriteLine("Product === 未找到需要索引数据");
                    return;
                }
                Console.WriteLine(string.Format("Product === 准备索引：{0}条数据", dTable.Rows.Count));

                //  设置索引
                string IndexName = currDate.ToString("yyyyMMdd");
                JXSearchProvider provider = JXSearchProviderCreator.CreateProvider(JXSearchType.Product);
                provider.CreateIndex(GetIndexPath(provider.INDEX_DIR, out IndexName));
                provider.MaxMergeFactor = 301;
                provider.MinMergeDocs = 301;

                //  遍历商品
                int recordCount = dTable.Rows.Count;
                for (int i = 0; i <= dTable.Rows.Count - 1; i++)
                {
                    //  写入索引
                    provider.IndexString(dTable.Rows[i], JXSearchType.Product);
                    if (i % 300 == 0)
                    {
                        provider.CloseWithoutOptimize();
                        provider.CreateIndex(string.Format("{0}\\{1}", provider.INDEX_DIR, IndexName));
                        provider.MaxMergeFactor = 301;
                        provider.MinMergeDocs = 301;
                    }
                }

                //  关闭流
                provider.Close();
                Console.WriteLine(string.Format("Product === 插入{0}行商品索引数据", recordCount));
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Product === 生成索引失败：{0}", ex.Message.ToString()));
            }

            //  缓存索引文件名
            try
            {
                SearchDictDirCacheProvider.Remove("ProductEngine");
                SearchDictDirCacheProvider.Set("ProductEngine");
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Product === 缓存索引文件名失败：{0}", ex.Message.ToString()));
            }
            finally
            {
                Console.WriteLine("Product === 索引生成完毕\r\n\r\n");
            }
        }

        /// <summary>
        /// 关键词索引
        /// </summary>
        private static void CreateKeywordIndex()
        {
            try
            {
                Console.WriteLine("Keyword === 开始生成索引");
                //  查询关键词
                int recordCount = 0;
                string strWhere = " AND k.Status=0";
                DataTable dTable = JXAPI.Component.BLL.SqlBLL.Instance.Keyword_GetListForSearch(strWhere);
                if (dTable == null || dTable.Rows.Count <= 0)
                {
                    Console.WriteLine("Keyword === 未找到需要索引数据");
                    return;
                }
                Console.WriteLine(string.Format("Keyword === 准备索引：{0}条数据", dTable.Rows.Count));              

                //  设置索引
                string IndexName = currDate.ToString("yyyyMMdd");
                JXSearchProvider provider = JXSearchProviderCreator.CreateProvider(JXSearchType.Keywords);
                provider.CreateIndex(GetIndexPath(provider.INDEX_DIR, out IndexName));
                provider.MaxMergeFactor = 301;
                provider.MinMergeDocs = 301;

                //  定义商品搜索
                int recCount = 0;
                JXSearchProductResult result = null;
                JXSearchProvider providerProduct = JXSearchProviderCreator.CreateProvider(JXSearchType.Product);

                //  遍历关键词
                int i = 0;
                recordCount = dTable.Rows.Count;
                foreach (DataRow row in dTable.Rows)
                {
                    i++;
                    
                    #region 关键词下商品数
                    try
                    {
                        result = providerProduct.Search(new RequestForm()
                        {
                            pageForm = new PageForm() { page = 1, size = 1 },
                            queryForm = new QueryForm() { keyword = row["ChineseName"].ToString() }
                        }, out recCount);
                        row["ProductCount"] = recCount;
                    }
                    catch { }
                    #endregion
                    
                    //  写入索引
                    provider.IndexString(row, JXSearchType.Keywords);
                    if (i % 300 == 0)
                    {
                        provider.CloseWithoutOptimize();
                        provider.CreateIndex(string.Format("{0}\\{1}", provider.INDEX_DIR, IndexName));
                        provider.MaxMergeFactor = 301;
                        provider.MinMergeDocs = 301;
                    }
                }

                //  关闭流
                provider.Close();
                Console.WriteLine(string.Format("Keyword === 插入{0}行关键字索引数据", recordCount));
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Keyword === 生成索引失败：{0}", ex.Message.ToString()));
            }

            //  缓存索引文件名
            try
            {
                SearchDictDirCacheProvider.Remove("KeywordEngine");
                SearchDictDirCacheProvider.Set("KeywordEngine");
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Keyword === 缓存索引文件名失败：{0}", ex.Message.ToString()));
            }
            finally
            {
                Console.WriteLine("Keyword === 索引生成完毕\r\n\r\n");
            }
        }

        /// <summary>
        /// 搜索关键词索引
        /// </summary>
        private static void CreateSearchKeywordIndex()
        {
            try
            {
                Console.WriteLine("SearchKeyword === 开始生成索引");
                //  查询关键词
                int recordCount = 0;
                DataTable dTable = JXAPI.Component.BLL.SqlBLL.Instance.SearchKeyword_GetListForSearch();
                if (dTable == null || dTable.Rows.Count <= 0)
                {
                    Console.WriteLine("SearchKeyword === 未找到需要索引数据");
                    return;
                }
                Console.WriteLine(string.Format("SearchKeyword === 准备索引：{0}条数据", dTable.Rows.Count));

                //  设置索引
                string IndexName = currDate.ToString("yyyyMMdd");
                JXSearchProvider provider = JXSearchProviderCreator.CreateProvider(JXSearchType.Keywords);
                provider.CreateIndex(GetIndexPath(provider.INDEX_DIR, out IndexName));
                provider.MaxMergeFactor = 301;
                provider.MinMergeDocs = 301;
                
                //  遍历关键词
                int i = 0;
                recordCount = dTable.Rows.Count;
                foreach (DataRow row in dTable.Rows)
                {
                    i++;
                    //  写入索引
                    provider.IndexString(row, JXSearchType.Keywords);
                    if (i % 300 == 0)
                    {
                        provider.CloseWithoutOptimize();
                        provider.CreateIndex(string.Format("{0}\\{1}", provider.INDEX_DIR, IndexName));
                        provider.MaxMergeFactor = 301;
                        provider.MinMergeDocs = 301;
                    }
                }

                //  关闭流
                provider.Close();
                Console.WriteLine(string.Format("SearchKeyword === 插入{0}行关键字索引数据", recordCount));
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("SearchKeyword === 生成索引失败：{0}", ex.Message.ToString()));
            }

            //  缓存索引文件名
            try
            {
                SearchDictDirCacheProvider.Remove("KeywordEngine");
                SearchDictDirCacheProvider.Set("KeywordEngine");
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("SearchKeyword === 缓存索引文件名失败：{0}", ex.Message.ToString()));
            }
            finally
            {
                Console.WriteLine("SearchKeyword === 索引生成完毕\r\n\r\n");
            }
        }

        /// <summary>
        /// 更新搜索关键词
        /// </summary>
        private static void UpdateSearchKeyword()
        {
            try
            {
                Console.WriteLine("SearchKeyword === 开始执行搜索关键词推荐商品");
                //  查询关键词
                int recordCount = 0;
                DataTable dTable = JXAPI.Component.BLL.SqlBLL.Instance.SearchKeyword_GetListForSearch();
                if (dTable == null || dTable.Rows.Count <= 0)
                {
                    Console.WriteLine("SearchKeyword === 未找到搜索关键词");
                    return;
                }
                Console.WriteLine(string.Format("SearchKeyword === 准备：{0}条数据", dTable.Rows.Count));

                //  定义商品搜索
                int recCount = 0;
                JXSearchProductResult result = null;
                JXSearchProvider providerProduct = JXSearchProviderCreator.CreateProvider(JXSearchType.Product);

                //  定义关键词搜索
                JXSearchEntityResult entityResult = null;
                JXSearchProvider providerKeyword = JXSearchProviderCreator.CreateProvider(JXSearchType.Keywords);

                //  
                string related = string.Empty, salled = string.Empty, recommend = string.Empty, preferential = string.Empty, hotSalled = string.Empty;

                //  遍历关键词
                foreach (DataRow row in dTable.Rows)
                {
                    #region 关键词相关
                    try
                    {
                        entityResult = providerKeyword.Search(row["ChineseName"].ToString(), 10, 1, out recCount);
                        if (entityResult == null || entityResult.listKeyword == null || entityResult.listKeyword.Count <= 0)
                            related = string.Empty;
                        else
                            related = string.Join(",", entityResult.listKeyword.Select(i => i.chineseName).ToArray());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(string.Format("SearchKeyword === 推荐（0）：{0}", ex.Message));
                    }
                    #endregion

                    #region 关键词下商品
                    try
                    {
                        result = providerProduct.Search(new RequestForm()
                        {
                            pageForm = new PageForm() { page = 1, size = 100 },
                            queryForm = new QueryForm() { keyword = row["ChineseName"].ToString() }
                        }, out recCount);
                        if (result == null || result.listProductList == null || result.listProductList.Count <= 0)
                        {
                            salled = string.Empty; recommend = string.Empty; preferential = string.Empty; hotSalled = string.Empty;
                        }
                        else
                        {
                            salled = string.Join(",", result.listProductList.OrderByDescending(i => i.sellCount).Take(10).Select(i => i.productID).ToArray());
                            recommend = string.Join(",", result.listProductList.OrderByDescending(i => i.recommend).Take(10).Select(i => i.productID).ToArray());
                            preferential = string.Join(",", result.listProductList.OrderByDescending(i => i.preferential).Take(10).Select(i => i.productID).ToArray());
                            hotSalled = string.Join(",", result.listProductList.OrderByDescending(i => i.preferential).ThenBy(i => i.sellCount).Take(10).Select(i => i.productID).ToArray());
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(string.Format("SearchKeyword === 推荐（1）：{0}", ex.Message));
                    }
                    #endregion

                    #region 保存
                    try
                    {
                        JXAPI.Component.BLL.SqlBLL.Instance.Update_SearchKeyword(related, salled, recommend, preferential, hotSalled, recCount, int.Parse(row["keywordID"].ToString()));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(string.Format("SearchKeyword === 推荐（2）：{0}", ex.Message));
                    }
                    #endregion
                }
                Console.WriteLine(string.Format("SearchKeyword === {0}条搜索关键词，推荐完成", recordCount));
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("SearchKeyword === 搜索关键词推荐商品失败：{0}", ex.Message.ToString()));
            }
        }

        /// <summary>
        /// 索引文件夹
        /// </summary>
        /// <param name="index_dir">路劲</param>
        /// <param name="indexName">返回文件夹名称</param>
        /// <returns></returns>
        private static string GetIndexPath(string index_dir, out string indexName)
        {
            int n = 1;
            indexName = currDate.ToString("yyyyMMdd");
            string path = string.Format("{0}\\{1}", index_dir, indexName);
            while (Directory.Exists(path))
            {
                indexName = string.Format("{0}-{1}", currDate.ToString("yyyyMMdd"), n);
                path = string.Format("{0}\\{1}", index_dir, indexName);
                n++;
            }
            return path;
        }

        /// <summary>
        /// 记录字符串
        /// </summary>
        static IList<string> key = new List<string>();

        /// <summary>
        /// 商品名称拆分
        /// </summary>
        /// <returns></returns>
        private static void ChineseNameSplit(string chineseName)
        {
            try
            {
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("[a-zA-Z0-9-/Ⅱ.*:+() （）Ⅰ【】]+", System.Text.RegularExpressions.RegexOptions.Compiled);
                chineseName = regex.Replace(chineseName, " ");

                int i = chineseName.Length;
                if (i == 2)
                {
                    if (!key.Contains(chineseName))
                        key.Add(chineseName);
                }
                string tempWord = string.Empty;
                string tempChineseName = chineseName.Substring(1);
                while (i > 2)
                {
                    //  顺序截取
                    tempWord = chineseName.Substring(0, 2);
                    if (tempWord.Length > 1)
                    {
                        if (!key.Contains(tempWord) && tempWord.Trim().Length > 1)
                            key.Add(tempWord);
                    }
                    chineseName = chineseName.Substring(2);

                    //  递减首字
                    tempWord = tempChineseName.Substring(0, 2);
                    if (tempWord.Length > 1)
                    {
                        if (!key.Contains(tempWord) && tempWord.Trim().Length > 1)
                            key.Add(tempWord);
                    }
                    tempChineseName = tempChineseName.Substring(2);
                    i = i - 2;
                }
            }
            catch { }
        }
    }
}
