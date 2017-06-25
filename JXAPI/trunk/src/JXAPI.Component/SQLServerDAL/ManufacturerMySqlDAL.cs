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
    public class ManufacturerMySqlDAL
    {
        private static Database dbw = JXProductMySqlData.Writer;
        private static Database dbr = JXProductMySqlData.Reader;
        private ILog myLog = log4net.LogManager.GetLogger(typeof(ManufacturerMySqlDAL));

        #region CURD

        /// <summary>
        /// 查询生产厂家最大ID
        /// </summary>
        /// <returns></returns>
        public int GetMaxManufacturerID()
        {
            int maxId = 0;
            try
            {
                string sqlCommand = "select * from manufacter order by ManuID DESC limit 0, 1";
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
                myLog.ErrorFormat("GetMaxManufacturerID 获取生产厂家最大ID失败,异常信息:{0}", ex.Message);
                maxId = -1;
            }
            return maxId;
        }

        /// <summary>
        /// 更新生产厂家
        /// </summary>
        /// <param name="productTable"></param>
        /// <param name="errorCount"></param>
        /// <returns></returns>
        public bool UpdateManufacturer(DataTable productTable, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            try
            {
                string strPlaceholder = string.Empty;
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("replace into manufacter ( " + parmsKey + " ) values ");
                for (int i = 0; i < productTable.Rows.Count; i++)
                {
                    var dr = productTable.Rows[i];
                    var Placeholder = string.Format(@"({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}')",
                                     dr["ManuID"].ToInt(), dr["Manufacturer"].ToString().Replace("\'", "\""), dr["Address"].ToString().Replace("\'", "\""), dr["Postalcode"].ToString().Replace("\'", "\""), dr["Phone"].ToString().Replace("\'", "\""), dr["ConsultPhone"].ToString().Replace("\'", "\"")
                                     , dr["ServicePhone"].ToString().Replace("\'", "\""), dr["Office"].ToString().Replace("\'", "\""), dr["Fax"].ToString().Replace("\'", "\""), dr["RegAddress"].ToString().Replace("\'", "\""), dr["Site"].ToString().Replace("\'", "\""), dr["Creator"].ToString().Replace("\'", "\"")
                                     , dr["CreateTime"].ToDateTime().ToString(), dr["LastUpdate"].ToString().Replace("\'", "\""), dr["LastUpdateTime"].ToDateTime().ToString());
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
                myLog.ErrorFormat("UpdateManufacturer 更新生产厂家表失败,生产厂家ID{0}-{1},异常信息:{2}", productTable.Rows[0]["ManuID"], productTable.Rows[productTable.Rows.Count - 1]["ManuID"], ex.Message);
                flag = false;
            }
            return flag;
        }


        /// <summary>
        /// 更新生产厂家
        /// </summary>
        /// <param name="productTable"></param>
        /// <param name="errorCount"></param>
        /// <returns></returns>
        public bool UpdateManufacturerEx(DataTable productTable, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            for (int i = 0; i < productTable.Rows.Count; i++)
            {
                var dr = productTable.Rows[i];
                try
                {
                    StringBuilder sqlCommand = new StringBuilder();
                    sqlCommand.Append("update manufacter set ");
                    var Placeholder = string.Format(@"Manufacturer = '{0}',Address = '{1}',Postalcode = '{2}',Phone = '{3}',ConsultPhone = '{4}',ServicePhone = '{5}',Office = '{6}',Fax = '{7}',RegAddress = '{8}',Site = '{9}'
                                                         ,Creator = '{10}',CreateTime = '{11}',LastUpdate = '{12}',LastUpdateTime = '{13}'",
                                      dr["Manufacturer"].ToString().Replace("\'", "\""), dr["Address"].ToString().Replace("\'", "\""), dr["Postalcode"].ToString().Replace("\'", "\""), dr["Phone"].ToString().Replace("\'", "\""), dr["ConsultPhone"].ToString().Replace("\'", "\"")
                                     , dr["ServicePhone"].ToString().Replace("\'", "\""), dr["Office"].ToString().Replace("\'", "\""), dr["Fax"].ToString().Replace("\'", "\""), dr["RegAddress"].ToString().Replace("\'", "\""), dr["Site"].ToString().Replace("\'", "\""), dr["Creator"].ToString().Replace("\'", "\"")
                                     , dr["CreateTime"].ToDateTime().ToString(), dr["LastUpdate"].ToString().Replace("\'", "\""), dr["LastUpdateTime"].ToDateTime().ToString());
                    sqlCommand.Append(Placeholder);
                    sqlCommand.AppendFormat(@" where ManuID = {0}", dr["ManuID"]);
                    var cmd = dbw.GetSqlStringCommand(sqlCommand.ToString().Replace("\'null\'", "null").Replace("\\", "\\\\"));
                    var result = dbw.ExecuteNonQuery(cmd);
                    if (result <= 0)
                    {
                        errorCount++;
                        flag = false;
                        myLog.ErrorFormat("UpdateManufacturer 更新生产厂家表失败,生产厂家ID：{0},受影响行为0", dr["ManuID"]);
                    }
                }
                catch (Exception ex)
                {
                    myLog.ErrorFormat("UpdateManufacturer 更新生产厂家表失败,生产厂家ID:{0},异常信息:{1}", dr["ManuID"], ex.Message);
                    flag = false;
                    errorCount++;
                }
            }

            return flag;
        }

        /// <summary>
        /// 添加生产厂家
        /// </summary>
        /// <param name="productTable"></param>
        /// <param name="errorCount"></param>
        /// <returns></returns>
        public bool AddManufacturer(DataTable productTable, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            try
            {
                string strPlaceholder = string.Empty;
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("insert into manufacter ( " + parmsKey + " ) values ");
                for (int i = 0; i < productTable.Rows.Count; i++)
                {
                    var dr = productTable.Rows[i];
                    var Placeholder = string.Format(@"({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}')",
                                     dr["ManuID"].ToInt(), dr["Manufacturer"].ToString().Replace("\'", "\""), dr["Address"].ToString().Replace("\'", "\""), dr["Postalcode"].ToString().Replace("\'", "\""), dr["Phone"].ToString().Replace("\'", "\""), dr["ConsultPhone"].ToString().Replace("\'", "\"")
                                     , dr["ServicePhone"].ToString().Replace("\'", "\""), dr["Office"].ToString().Replace("\'", "\""), dr["Fax"].ToString().Replace("\'", "\""), dr["RegAddress"].ToString().Replace("\'", "\""), dr["Site"].ToString().Replace("\'", "\""), dr["Creator"].ToString().Replace("\'", "\"")
                                     , dr["CreateTime"].ToDateTime().ToString(), dr["LastUpdate"].ToString().Replace("\'", "\""), dr["LastUpdateTime"].ToDateTime().ToString());
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
                myLog.ErrorFormat("AddManufacturer 添加生产厂家表失败,生产厂家ID{0}-{1},异常信息:{2}", productTable.Rows[0]["ManuID"], productTable.Rows[productTable.Rows.Count - 1]["ManuID"], ex.Message);
                flag = false;
            }
            return flag;
        }

        #endregion

        private string parmsKey = string.Format(@"ManuID,Manufacturer,Address,Postalcode,Phone,ConsultPhone,ServicePhone,Office,Fax,RegAddress,Site,Creator,CreateTime 
                           ,LastUpdate,LastUpdateTime");
    }
}
