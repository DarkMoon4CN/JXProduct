﻿using JXAPI.Common;
using JXAPI.Component.IBLL.IMysqlBLL;
using JXAPI.Component.SQLServerDAL.MySqlDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JXAPI.Component.BLL.MySqlBLL
{
    public class OrderMySqlBLL : IProductPlanMySqlBLL
    {
        private OrderMySqlBLL() { }
        private JXOrderMySqlDAL dal = new JXOrderMySqlDAL();
        public static OrderMySqlBLL Instance
        {
            get
            {
                return new OrderMySqlBLL();
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
    }
}
