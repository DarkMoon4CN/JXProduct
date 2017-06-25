using JXProduct.Component.Model;
using JXProduct.Component.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXProduct.Component.BLL
{
    public class KeywordBLL
    {
        private static KeywordBLL _instance;
        private static KeywordDAL dal;
        private KeywordBLL() { dal = new KeywordDAL(); }
        public static KeywordBLL Instance
        {
            get { return _instance ?? (_instance = new KeywordBLL()); }
        }


        #region Keyword_GetList(ids,Name)

        public KeywordInfo Keyword_Get(int keywordid)
        {
            return dal.Keyword_Get(keywordid);
        }

        // 根据keywordIDs 获取集合数据
        /// <summary>
        /// 根据keywordIDs 获取集合数据
        /// </summary>
        /// <param name="keywordIDs"></param>
        /// <returns></returns>
        public Dictionary<int, string> Keyword_GetList(IList<int> keywordIDs)
        {
            if (keywordIDs != null && keywordIDs.Count > 0)
            {
                var ids = string.Join(",", keywordIDs);
                return dal.Keyword_GetList(ids);
            }
            return new Dictionary<int, string>();
        }
        // 根据keywordIDs 获取集合数据
        /// <summary>
        /// 根据keywordIDs 获取集合数据
        /// </summary>
        /// <param name="keywordIDs"></param>
        /// <returns></returns>
        public List<KeywordInfo> Keyword_GetByName(string name, bool isLike = false)
        {
            return dal.Keyword_GetByName(name, isLike);
        }



        #endregion

        #region Update

        public KeywordInfo Keyword_Insert(KeywordInfo info)
        {
            info.KeywordID = dal.Keyword_Insert(info);
            return info;
        }

        public bool Keyword_Update(KeywordInfo info)
        {
            return dal.Keyword_Update(info);
        }
        #endregion

        /// <summary>
        /// 查询商品关联标签
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public List<KeywordInfo> KeywordProduct_ByProductID(int productId)
        {
            return dal.KeywordProduct_ByProductID(productId);
        }

        /// <summary>
        /// 处理关键词关联商品
        /// </summary>
        /// <param name="keywordID">关键词ID</param>
        /// <param name="productID">商品ID</param>
        /// <param name="sort">排序</param>
        /// <param name="inputTypeID">1=删除 2=排序 0=新增</param>
        /// <returns>返回数据：0=正常</returns>
        public string KeywordProduct_Save(int keywordID, int productID, int sort, int inputTypeID)
        {
            return dal.KeywordProduct_Save(keywordID, productID, sort, inputTypeID);
        }
    }
}