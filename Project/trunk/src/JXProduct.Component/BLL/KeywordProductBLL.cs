using JXProduct.Component.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXProduct.Component.BLL
{
    public class KeywordProductBLL
    {
        private KeywordProductBLL() { }
        private static KeywordProductBLL _instance;
        public static KeywordProductBLL Instance { get { return _instance ?? (_instance = new KeywordProductBLL()); } }



        private static readonly KeywordProductDAL dal = new KeywordProductDAL();

        /// <summary>
        /// 检索出商品相关“关键字ID”
        /// </summary>
        public List<int> KeywordProduct_GetList(int productid)
        {
            return dal.KeywordProduct_GetList(productid);
        }

        /// <summary>
        /// 添加关键字
        /// </summary>
        public bool KeywordProduct_Insert(int productID, int keywordID, int sort)
        {
            return dal.KeywordProduct_Insert(productID, keywordID, sort);
        }
    }
}