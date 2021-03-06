﻿using JXAPI.Component.DataAccess;
using log4net;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JXAPI.Component.SQLServerDAL
{
    public class JxorderProductRelatedMySqlDAL
    {
        private static Database dbw = JXOrderMySqlData.Writer;
        private static Database dbr = JXOrderMySqlData.Reader;
        private ILog myLog = log4net.LogManager.GetLogger(typeof(JxorderProductRelatedMySqlDAL));

        #region CURD

        /// <summary>
        /// 查询产品关联最大ID
        /// </summary>
        /// <returns></returns>
        public int GetMaxJxorderProductRelatedID()
        {
            int maxId = 0;
            try
            {
                string sqlCommand = "select * from productrelated order by ID DESC limit 0, 1";
                var cmd = dbr.GetSqlStringCommand(sqlCommand);
                if (dbr.ExecuteScalar(cmd).IsNotNULL())
                {
                    maxId = Convert.ToInt32(dbr.ExecuteScalar(cmd));
                }
                else
                {
                    maxId = 0;
                }
            }
            catch (Exception ex)
            {
                myLog.ErrorFormat("GetMaxJxorderProductRelatedID 获取产品关联最大ID失败,异常信息:{0}", ex.Message);
                maxId = -1;
            }
            return maxId;
        }

        /// <summary>
        /// 更新产品关联
        /// </summary>
        /// <param name="productTable"></param>
        /// <param name="errorCount"></param>
        /// <returns></returns>
        public bool UpdateJxorderProductRelated(DataTable productTable, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            try
            {
                string strPlaceholder = string.Empty;
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("replace into productrelated ( " + parmsKey + " ) values ");
                for (int i = 0; i < productTable.Rows.Count; i++)
                {
                    var dr = productTable.Rows[i];
                    var Placeholder = string.Format(@"({0},{1},{2},'{3}',{4},'{5}','{6}','{7}','{8}')",
                                     dr["RelatedID"].ToInt(), dr["ProductID"].ToInt(), dr["ChildProductID"].ToInt(), dr["Name"].ToString().Replace("\'", "\""), dr["Type"].ToShort()
                                     , dr["Creator"].ToString().Replace("\'", "\""), dr["CreateTime"].ToDateTime().ToString(), dr["Updater"].ToString().Replace("\'", "\""), dr["UpdateTime"].ToDateTime().ToString());
                    if (i == 0)
                    {
                        strPlaceholder = Placeholder;
                    }
                    else
                    {
                        strPlaceholder += "," + Placeholder;
                    }
                }
                if (!string.IsNullOrEmpty(strPlaceholder))
                {
                    sqlCommand.Append(strPlaceholder);
                    var cmd = dbw.GetSqlStringCommand(sqlCommand.ToString());
                    var result = dbw.ExecuteNonQuery(cmd);
                    if (result <= 0)
                    {
                        errorCount = productTable.Rows.Count;
                        flag = false;
                    }
                    else
                    {
                        errorCount = (productTable.Rows.Count - result > 0) ? productTable.Rows.Count - result : 0;
                        if (errorCount == 0)
                        {
                            flag = true;
                        }
                        else
                        {
                            flag = false;
                        }  
                    }
                }
            }
            catch (Exception ex)
            {
                myLog.ErrorFormat("UpdateJxorderProductRelated 更新产品关联表失败,产品关联ID{0}-{1},异常信息:{2}", productTable.Rows[0]["RelatedID"], productTable.Rows[productTable.Rows.Count - 1]["RelatedID"], ex.Message);
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// 更新产品关联
        /// </summary>
        /// <param name="productTable"></param>
        /// <param name="errorCount"></param>
        /// <returns></returns>
        public bool UpdateJxorderProductRelatedEx(DataTable productTable, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            for (int i = 0; i < productTable.Rows.Count; i++)
            {
                var dr = productTable.Rows[i];
                try
                {
                    StringBuilder sqlCommand = new StringBuilder();
                    sqlCommand.Append("update productrelated set ");
                    var Placeholder = string.Format(@"ProductID = '{0}',ChildProductID = '{1}',Name = '{2}',Type = '{3}',Creator = '{4}',CreateTime = '{5}',
                                                      Updater = '{6}',UpdateTime = '{7}'",
                                         dr["ProductID"].ToInt(), dr["ChildProductID"].ToInt(), dr["Name"].ToString().Replace("\'", "\""), dr["Type"].ToShort()
                                        , dr["Creator"].ToString().Replace("\'", "\""), dr["CreateTime"].ToDateTime().ToString(), dr["Updater"].ToString().Replace("\'", "\""), dr["UpdateTime"].ToDateTime().ToString());
                    sqlCommand.Append(Placeholder);
                    sqlCommand.AppendFormat(@" where ID = {0}", dr["RelatedID"]);
                    var cmd = dbw.GetSqlStringCommand(sqlCommand.ToString().Replace("\'null\'", "null").Replace("\\", "\\\\"));
                    var result = dbw.ExecuteNonQuery(cmd);
                    if (result <= 0)
                    {
                        errorCount++;
                        flag = false;
                        myLog.ErrorFormat("UpdateJxorderProductRelated 更新产品关联表失败,产品关联ID：{0},受影响行为0", dr["RelatedID"]);
                    }
                }
                catch (Exception ex)
                {
                    myLog.ErrorFormat("UpdateJxorderProductRelated 更新产品关联表失败,产品关联ID:{0},异常信息:{1}", dr["RelatedID"], ex.Message);
                    flag = false;
                    errorCount++;
                }
            }
            return flag;
        }

        /// <summary>
        /// 添加产品关联
        /// </summary>
        /// <param name="productTable"></param>
        /// <param name="errorCount"></param>
        /// <returns></returns>
        public bool AddJxorderProductRelated(DataTable productTable, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            try
            {
                string strPlaceholder = string.Empty;
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("insert into productrelated ( " + parmsKey + " ) values ");
                for (int i = 0; i < productTable.Rows.Count; i++)
                {
                    var dr = productTable.Rows[i];
                    var Placeholder = string.Format(@"({0},{1},{2},'{3}',{4},'{5}','{6}','{7}','{8}')",
                                     dr["RelatedID"].ToInt(), dr["ProductID"].ToInt(), dr["ChildProductID"].ToInt(), dr["Name"].ToString().Replace("\'", "\""), dr["Type"].ToShort()
                                     , dr["Creator"].ToString().Replace("\'", "\""), dr["CreateTime"].ToDateTime().ToString(), dr["Updater"].ToString().Replace("\'", "\""), dr["UpdateTime"].ToDateTime().ToString());
                    if (i == 0)
                    {
                        strPlaceholder = Placeholder;
                    }
                    else
                    {
                        strPlaceholder += "," + Placeholder;
                    }
                }
                if (!string.IsNullOrEmpty(strPlaceholder))
                {
                    sqlCommand.Append(strPlaceholder);
                    var cmd = dbw.GetSqlStringCommand(sqlCommand.ToString());
                    var result = dbw.ExecuteNonQuery(cmd);
                    if (result <= 0)
                    {
                        errorCount = productTable.Rows.Count;
                        flag = false;
                    }
                    else
                    {
                        errorCount = (productTable.Rows.Count - result > 0) ? productTable.Rows.Count - result : 0;
                        if (errorCount == 0)
                        {
                            flag = true;
                        }
                        else
                        {
                            flag = false;
                        }  
                    }
                }
            }
            catch (Exception ex)
            {
                myLog.ErrorFormat("AddJxorderProductRelated 添加产品关联表失败,产品关联ID{0}-{1},异常信息:{2}", productTable.Rows[0]["RelatedID"], productTable.Rows[productTable.Rows.Count - 1]["RelatedID"], ex.Message);
                flag = false;
            }
            return flag;
        }

        #endregion

        private string parmsKey = string.Format(@"ID,ProductID,ChildProductID,Name,Type,Creator,CreateTime,
                                           Updater,UpdateTime");
    }
}
