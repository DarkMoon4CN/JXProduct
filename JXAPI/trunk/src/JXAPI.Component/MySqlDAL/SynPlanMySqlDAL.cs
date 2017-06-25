﻿using JXAPI.Common;
using JXAPI.Common.Utils;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace JXAPI.Component.MySqlDAL
{
    public class SynPlanMySqlDAL
    {
        private  Database context;
        public SynPlanMySqlDAL(Database context)
        {
            this.context = context;
        }

        public OperationResult<IList<string>> GetColumnsName(string tableName)
        {
            IList<string> columnNameList = new List<string>();
            try
            {
                string sql = string.Empty;
                sql += "SELECT COLUMN_NAME FROM information_schema.columns WHERE table_name='{0}'";
                sql = string.Format(sql, tableName);
                DbCommand cmd = context.GetSqlStringCommand(sql);
                using (IDataReader dataReader = context.ExecuteReader(cmd))
                {
                    while (dataReader.Read())
                    {
                        columnNameList.Add(Convert.ToString(dataReader["COLUMN_NAME"]).ToLower());
                    }
                }
                return new OperationResult<IList<string>>(OperationResultType.Success, null, columnNameList);
            }
            catch (Exception ex)
            {
                return new OperationResult<IList<string>>(OperationResultType.Error, ex.Message);
            }
        }

        public OperationResult<int> GetMaxID(string tableName, string primaryId)
        {
            int maxId = -1;
            try
            {
                string sql = " SELECT {0} FROM  {1} ORDER BY {2} DESC LIMIT 0,1";
                sql = string.Format(sql,primaryId,tableName,primaryId);
                DbCommand cmd = context.GetSqlStringCommand(sql);
                if (context.ExecuteScalar(cmd).IsNotNULL())
                {
                    maxId = Convert.ToInt32(context.ExecuteScalar(cmd));
                }
                else
                {
                    maxId = 0;
                }
                return new OperationResult<int>(OperationResultType.Success,null,maxId);
            }
            catch (Exception ex)
            {
                return new OperationResult<int>(OperationResultType.Error, ex.Message,-1);
            }
        }

        public OperationResult<DataTable> GetList(string tableName, string primaryId, string columnNames, int startCount, int size)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = " SELECT {0} FROM {1} order by  {2} DESC  LIMIT {3},{4} ";
                sql = string.Format(sql,columnNames, primaryId, tableName, startCount, size);
                DbCommand cmd = context.GetSqlStringCommand(sql);
                using (IDataReader dataReader = context.ExecuteReader(cmd))
                {
                    dt.Load(dataReader);
                }
                return new OperationResult<DataTable>(OperationResultType.Success, null, dt);
            }
            catch (Exception ex)
            {
                return new OperationResult<DataTable>(OperationResultType.Error, ex.Message, dt);
            }
        }

        public OperationResult<DataTable> GetList(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                DbCommand cmd = context.GetSqlStringCommand(sql);
                using (IDataReader dataReader = context.ExecuteReader(cmd))
                {
                    dt.Load(dataReader);
                }
                return new OperationResult<DataTable>(OperationResultType.Success, null, dt);
            }
            catch (Exception ex)
            {
                return new OperationResult<DataTable>(OperationResultType.Error, ex.Message, dt);
            }
        }

        public OperationResult<bool> InsertData(string tableName, string columnNames, string pColumnNames, DataTable dt,Dictionary<string,string> inserColumnsName)
        {
            DbConnection con = null;
            DbTransaction transcation = null;
            try
            {
                using (con = context.CreateConnection())
                {
                    con.Open();
                    transcation = con.BeginTransaction();
                    using (var command = con.CreateCommand())
                    {
                        command.Transaction = transcation;
                        string sql = " INSERT INTO {0}({1}) ";
                        sql += "VALUES";
                        sql += " ({2}) ";
                        sql = string.Format(sql, tableName, columnNames, pColumnNames);
                        command.Connection = con;
                        command.CommandText = sql;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            command.Parameters.Clear();
                            foreach (var item in inserColumnsName)
                            {
                                DbParameter parms = command.CreateParameter();
                                parms.DbType = ChangeType.MappingDbType(dt.Columns[item.Value].DataType);
                                parms.Value = dt.Rows[i][item.Value];
                                parms.ParameterName = item.Value;
                                command.Parameters.Add(parms);
                            }
                            command.ExecuteNonQuery();
                        }
                    }
                    transcation.Commit();
                }
                return new OperationResult<bool>(OperationResultType.Success, null, true);
            }
            catch (Exception ex)
            {
                if (transcation != null)
                {
                    transcation.Rollback();
                }
                return new OperationResult<bool>(OperationResultType.Error, ex.Message, false);
            }
            finally
            {
                con.Close();
            }
          
        }

        /// <summary>
        /// 同步更新数据 
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pKeyFlag"> ID=@ID </param>
        /// <param name="pValueFlag"> value=@value,... </param>
        /// <param name="dt">DataTable 数据源</param>
        /// <param name="updateColumnsName">比对列名</param>
        /// <returns></returns>
        public OperationResult<bool> UpdateData(string tableName, string pKeyFlag, string pValueFlag, DataTable dt, Dictionary<string, string> updateColumnsName)
        {
            //wait
            DbConnection con = null;
            DbTransaction transcation = null;
            try
            {
                using (con = context.CreateConnection())
                {      
                    con.Open();
                    transcation = con.BeginTransaction();
                    using (var command = con.CreateCommand())
                    {
                        command.Transaction = transcation;
                        string sql = " UPDATE {0} SET {1} ";
                        sql += " WHERE 1=1 ";
                        sql += " AND {2} ";
                        sql = string.Format(sql, tableName, pValueFlag, pKeyFlag);
                        command.Connection = con;
                        command.CommandText = sql;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            command.Parameters.Clear();
                            foreach (var item in updateColumnsName)
                            {
                                DbParameter parms = command.CreateParameter();
                                parms.DbType = ChangeType.MappingDbType(dt.Columns[item.Value].DataType);
                                parms.Value = dt.Rows[i][item.Value];
                                parms.ParameterName = item.Value;
                                command.Parameters.Add(parms);
                            }
                            command.ExecuteNonQuery();
                        }
                    }
                    transcation.Commit();
                }
                return new OperationResult<bool>(OperationResultType.Success, null, true);
            }
            catch (Exception ex)
            {
                 
                if (transcation != null)
                {
                    transcation.Rollback();
                }
                return new OperationResult<bool>(OperationResultType.Error, ex.Message, false);
            }
            finally
            {
                con.Close();
            }

        }
    }
}
