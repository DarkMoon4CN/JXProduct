﻿using JXAPI.Component.DataAccess;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using JXAPI.Common;

namespace JXAPI.Component.SQLServerDAL.MySqlDAL
{
    public class JXYXMySqlDAL
    {
        private static Database dbw_YX = JXYXMySqlData.Writer;
        private static Database dbr_YX = JXYXMySqlData.Reader;

        #region CURD

        public int GetMaxID(string tableName, string primaryId)
        {
            int maxId = 0;
            try
            {
                if (tableName == "JXYXProduct")
                {
                    tableName = "Product";
                }
                string sqlCommand = string.Format(@"select * from {0} order by {1} DESC limit 0, 1", tableName, primaryId);
                var cmd = dbr_YX.GetSqlStringCommand(sqlCommand);
                if (dbr_YX.ExecuteScalar(cmd).IsNotNULL())
                {
                    maxId = Convert.ToInt32(dbr_YX.ExecuteScalar(cmd));
                }
                else
                {
                    maxId = 0;
                }
            }
            catch (Exception ex)
            {
                maxId = -1;
            }
            return maxId;
        }

        private void GetDbParameter(Type colType, DbCommand cmd, string colName, object colValue)
        {
            if (colName == "SellingTime")
            {
                if (string.IsNullOrEmpty(colValue.ToString()))
                {
                    colValue = colValue.ToDateTime();
                }
            }
            else if (colName == "Source")
            {
                colValue = 0;
            }
            if (colType == Type.GetType("System.Int16") || colType == Type.GetType("System.Byte"))
            {
                DbParameter param = cmd.CreateParameter();
                param.DbType = DbType.Int16;
                param.Value = colValue;
                param.ParameterName = colName;
                cmd.Parameters.Add(param);
            }
            else if (colType == Type.GetType("System.Int32"))
            {
                DbParameter param = cmd.CreateParameter();
                param.DbType = DbType.Int32;
                param.Value = colValue;
                param.ParameterName = colName;
                cmd.Parameters.Add(param);
            }
            else if (colType == Type.GetType("System.Int64"))
            {
                DbParameter param = cmd.CreateParameter();
                param.DbType = DbType.Int64;
                param.Value = colValue;
                param.ParameterName = colName;
                cmd.Parameters.Add(param);
            }
            else if (colType == Type.GetType("System.Decimal") || colType == Type.GetType("System.Float"))
            {
                DbParameter param = cmd.CreateParameter();
                param.DbType = DbType.Decimal;
                param.Value = colValue;
                param.ParameterName = colName;
                cmd.Parameters.Add(param);
            }
            else if (colType == Type.GetType("System.String") || colType == Type.GetType("System.Text"))
            {
                DbParameter param = cmd.CreateParameter();
                param.DbType = DbType.String;
                param.Value = colValue;
                param.ParameterName = colName;
                cmd.Parameters.Add(param);
            }
            else if (colType == Type.GetType("System.DateTime"))
            {
                DbParameter param = cmd.CreateParameter();
                param.DbType = DbType.DateTime;
                param.Value = colValue;
                param.ParameterName = colName;
                cmd.Parameters.Add(param);
            }
            else if (colType == Type.GetType("System.Date"))
            {
                DbParameter param = cmd.CreateParameter();
                param.DbType = DbType.Date;
                param.Value = colValue;
                param.ParameterName = colName;
                cmd.Parameters.Add(param);
            }
            else if (colType == Type.GetType("System.DBNull"))
            {
                DbParameter param = cmd.CreateParameter();
                param.DbType = DbType.Object;
                param.Value = colValue;
                param.ParameterName = colName;
                cmd.Parameters.Add(param);
            }
        }

        public OperationResult<bool> Update(string tableName, string[] colName, string[] sqlServerColName, DataRow dr)
        {
            try
            {
                if (colName.Length <= 0 || sqlServerColName.Length <= 0 || dr == null)
                {
                    return new OperationResult<bool>(OperationResultType.Error, "列名或列名对应的值为空", false);
                }
                if (tableName == "JXYXProduct")
                {
                    tableName = "Product";
                }
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.AppendFormat("update {0} set ", tableName);
                StringBuilder sqlStr = new StringBuilder();
                //拼接列名并建立对应关系
                for (int i = 0; i < colName.Length; i++)
                {
                    if (!string.IsNullOrEmpty(colName[i]))
                    {
                        if (i == 0)
                        {
                            continue;
                        }
                        if (i == 1)
                        {
                            sqlStr.AppendFormat(@"{0} = {1}{2}", colName[i].Trim(), "@", colName[i].Trim());
                        }
                        else
                        {
                            sqlStr.AppendFormat(@",{0} = {1}{2}", colName[i].Trim(), "@", colName[i].Trim());
                        }
                    }
                }
                sqlCommand.Append(sqlStr.ToString());
                sqlCommand.AppendFormat(@" where {0} = @{1}", colName[0].Trim(), colName[0].Trim());
                var cmd = dbw_YX.GetSqlStringCommand(sqlCommand.ToString());
                cmd.Parameters.Clear();
                for (int i = 0; i < sqlServerColName.Length; i++)
                {
                    GetDbParameter(dr[sqlServerColName[i].Trim()].GetType(), cmd, colName[i].Trim(), dr[sqlServerColName[i].Trim()]);
                }
                dbw_YX.ExecuteNonQuery(cmd);
                return new OperationResult<bool>(OperationResultType.Success, null, true);
            }
            catch (Exception ex)
            {
                return new OperationResult<bool>(OperationResultType.Error, ex.Message, false);
            }
        }

        public OperationResult<bool> Add(string tableName, string[] colName, string[] sqlServerColName, DataRow dr)
        {
            try
            {
                if (colName.Length <= 0 || sqlServerColName.Length <= 0 || dr == null)
                {
                    return new OperationResult<bool>(OperationResultType.Error, "列名或列名对应的值为空", false);
                }
                if (tableName == "JXYXProduct")
                {
                    tableName = "Product";
                }
                //防止已存在数据重复插入的异常
                StringBuilder sqlExistCommand = new StringBuilder();
                sqlExistCommand.AppendFormat("select COUNT(1) from {0} where {1} = {2}", tableName, colName[0], dr[0]);
                var cmdExist = dbr_YX.GetSqlStringCommand(sqlExistCommand.ToString());
                if (Convert.ToInt32(dbr_YX.ExecuteScalar(cmdExist)) > 0)
                {
                    return new OperationResult<bool>(OperationResultType.Success, null, true);
                }

                StringBuilder sqlKey = new StringBuilder();
                StringBuilder sqlValue = new StringBuilder();
                //拼接列名并建立对应关系
                for (int i = 0; i < colName.Length; i++)
                {
                    if (!string.IsNullOrEmpty(colName[i]))
                    {
                        if (i == 0)
                        {
                            sqlKey.AppendFormat(@"{0}", colName[i].Trim());
                            sqlValue.AppendFormat(@"@{0}", colName[i].Trim());
                        }
                        else
                        {
                            sqlKey.AppendFormat(@",{0}", colName[i].Trim());
                            sqlValue.AppendFormat(@",@{0}", colName[i].Trim());
                        }
                    }
                }
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.AppendFormat("insert into {0} ( " + sqlKey.ToString() + " ) values ", tableName);
                sqlCommand.AppendFormat("({0})", sqlValue);
                var cmd = dbw_YX.GetSqlStringCommand(sqlCommand.ToString());
                cmd.Parameters.Clear();
                for (int i = 0; i < sqlServerColName.Length; i++)
                {
                    GetDbParameter(dr[sqlServerColName[i].Trim()].GetType(), cmd, colName[i].Trim(), dr[sqlServerColName[i].Trim()]);
                }
                dbw_YX.ExecuteNonQuery(cmd);
                return new OperationResult<bool>(OperationResultType.Success, null, true);
            }
            catch (Exception ex)
            {
                return new OperationResult<bool>(OperationResultType.Error, ex.Message, false);
            }
        }

        public OperationResult<bool> Delete(DataRow dr)
        {
            try
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.AppendFormat(@"delete from {0} where {1} = {2}",
                    dr["TableName"].ToString().Trim(), dr["KeyName"].ToString().Trim(), dr["KeyValue"]);
                var cmd = dbw_YX.GetSqlStringCommand(sqlCommand.ToString());
                dbw_YX.ExecuteNonQuery(cmd);
                return new OperationResult<bool>(OperationResultType.Success, null, true);
            }
            catch (Exception ex)
            {
                return new OperationResult<bool>(OperationResultType.Error, ex.Message, false);
            }
        }

        #endregion

    }
}
