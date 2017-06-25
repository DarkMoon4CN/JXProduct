using System;
using System.Collections.Generic;
using JXProduct.Component.Model;
namespace JXProduct.Component.BLL
{
    /// <summary>
    /// 商品评论
    /// </summary>
    public class ProductEvaluationBLL
    {
        private ProductEvaluationBLL() { }
        private static ProductEvaluationBLL _instance;
        public static ProductEvaluationBLL Instance { get { if (_instance == null) { _instance = new ProductEvaluationBLL(); } return _instance; } }

        private static readonly JXProduct.Component.SQLServerDAL.ProductEvaluationDAL dal = new JXProduct.Component.SQLServerDAL.ProductEvaluationDAL();

        /// <summary>
        /// Insert
        ///</summary>
        public int ProductEvaluation_Insert(ProductEvaluationInfo productevaluationinfo)
        {
            return dal.ProductEvaluation_Insert(productevaluationinfo);
        }

        /// <summary>
        /// GetModel
        /// </summary>
        public ProductEvaluationInfo ProductEvaluation_Get(Int64 evaluationid)
        {
            return dal.ProductEvaluation_Get(evaluationid);
        }

        /// <summary>
        /// GetList
        /// </summary>
        public IList<ProductEvaluationInfo> ProductEvaluation_GetList(int pageIndex, int pageSize, string orderType, string strWhere, out int recordCount)
        {
            return dal.ProductEvaluation_GetList(pageIndex, pageSize, orderType, strWhere, out recordCount);
        }
        /// <summary>
        /// ProductEvaluation_GetByPName
        /// </summary>
        public IList<ProductEvaluationInfo> ProductEvaluation_GetByPName(int pageIndex, int pageSize, string orderType,string pName ,string strWhere, out int recordCount)
        {
            return dal.ProductEvaluation_GetByPName(pageIndex, pageSize, orderType,pName, strWhere, out recordCount);
        }

        /// <summary>
        /// update
        ///</summary>
        public bool ProductEvaluation_Update(int evalID, int statusType)
        {
            return dal.ProductEvaluation_Update(evalID, statusType);
        }
    }
}
