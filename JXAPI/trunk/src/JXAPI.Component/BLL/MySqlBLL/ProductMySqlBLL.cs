using JXAPI.Common;
using JXAPI.Component.IBLL.IMysqlBLL;
using JXAPI.Component.SQLServerDAL.MySqlDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JXAPI.Component.BLL.MySqlBLL
{
    public class ProductMySqlBLL : IProductPlanMySqlBLL
    {
        private ProductMySqlBLL() { }
        private JXProductMySqlDAL dal = new JXProductMySqlDAL();
        public static ProductMySqlBLL Instance
        {
            get
            {
                return new ProductMySqlBLL();
            }
        }

        #region CURD

        public int GetMaxID(string tableName,string primaryId)
        {
            return dal.GetMaxID(tableName, primaryId);
        }

        public OperationResult<bool> Add(string tableName, string[] colName, string[] sqlServerColName, DataRow dr)
        {
            return dal.Add(tableName, colName,sqlServerColName, dr);
        }

        public OperationResult<bool> Update(string tableName, string[] colName, string[] sqlServerColName, DataRow dr)
        {
            return dal.Update(tableName, colName, sqlServerColName, dr);
        }

        public OperationResult<bool> Delete(DataRow dr)
        {
            return dal.Delete(dr);
        }


        #endregion

        public bool ProductMySql_UpIsPush(int productID, int userID, int isPush)
        {
            return dal.ProductMySql_UpIsPush(productID, userID, isPush);
        }

        public OperationResult<bool> UpdateEvaluationCount()
        {
            return dal.UpdateEvaluationCount();
        }

        public OperationResult<bool> UpdateEvaluationPraiseRate()
        {
            return dal.UpdateEvaluationPraiseRate();
        }

    }
}
