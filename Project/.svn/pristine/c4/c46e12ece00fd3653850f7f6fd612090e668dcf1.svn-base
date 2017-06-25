using System;
using System.Collections.Generic;
using JXProduct.Component.Model;
namespace JXProduct.Component.BLL
{
    /// <summary>
    /// JXClassification 
    /// </summary>
    public class JXClassificationBLL
    {
        private JXClassificationBLL() { }
        private static JXClassificationBLL _instance;
        public static JXClassificationBLL Instance
        {
            get
            {
                return _instance ?? (_instance = new JXClassificationBLL());
            }
        }

        private static readonly JXProduct.Component.SQLServerDAL.JXClassificationDAL dal = new JXProduct.Component.SQLServerDAL.JXClassificationDAL();

        #region JX分类

        //获取分类
        public List<JXClassificationInfo> JXClassification_GetList()
        {
            return dal.JXClassification_GetList();
        }

        #endregion

        #region JX分类属性  JXClassification + ProductJXParameterValue

        //获取分类->说明书字段
        public List<JXClassificationParameterInfo> JXClassificationParameter_GetByJXCFID(int jxcfid)
        {
            return dal.JXClassificationParameter_GetByJXCFID(jxcfid);
        }

        //插入特定值 ->ProductJXParameterValue
        public bool ProductJXParameterValue_Add(int productid, Dictionary<int, string> dic)
        {
            return dal.ProductJXParameterValue_Add(productid, dic);
        }

        #endregion

        #region 商品ID -> 说明书value

        //获取当前分类下的value值
        public List<JXProductParameterValueInfo> ProductJXParameterValue_GetList(int productID)
        {
            return dal.ProductJXParameterValue_GetList(productID);
        }

        #endregion

    }
}
