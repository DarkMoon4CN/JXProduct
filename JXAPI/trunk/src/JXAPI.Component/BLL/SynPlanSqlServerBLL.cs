﻿using JXAPI.Common;
using JXAPI.Component.SQLServerDAL;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JXAPI.Component.BLL
{
    public class SynPlanSqlServerBLL
    {
        private Database context;
        private SynPlanSqlServerDAL dal;
        public SynPlanSqlServerBLL(string connectionName)
        {
            this.context = DatabaseFactory.CreateDatabase(connectionName);
            dal = new SynPlanSqlServerDAL(this.context);
        }
      
        public OperationResult<IList<string>> GetColumnsName(string tableName) 
        {
            return dal.GetColumnsName(tableName);
        }

        public OperationResult<int> GetMaxID(string tableName, string primaryId) 
        {
            return dal.GetMaxID(tableName, primaryId);
        }
        public OperationResult<DataTable> GetList(string tableName, string primaryId, string columnNames, int startCount, int endCount) 
        {
            return dal.GetList(tableName, primaryId,columnNames, startCount, endCount);
        }
        public OperationResult<DataTable> GetList(string sql)
        {
            return dal.GetList(sql);
        }

        public OperationResult<bool> InsertData(string tableName, string columnNames, string pColumnNames, DataTable dt, Dictionary<string, string> inserColumnsName)
        {
            return dal.InsertData(tableName, columnNames, pColumnNames, dt, inserColumnsName);
        }

        public OperationResult<bool> UpdateData(string tableName, string pKeyFlag, string pValueFlag, DataTable dt, Dictionary<string, string> updateColumnsName)
        {
            return dal.UpdateData(tableName, pKeyFlag, pValueFlag, dt, updateColumnsName);
        }
    }
}
