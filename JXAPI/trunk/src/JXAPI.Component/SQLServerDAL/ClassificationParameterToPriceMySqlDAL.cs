using JXAPI.Component.DataAccess;
using log4net;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JXAPI.Component.SQLServerDAL
{
    public class ClassificationParameterToPriceMySqlDAL
    {
        private static Database dbw = JXProductMySqlData.Writer;
        private static Database dbr = JXProductMySqlData.Reader;
        private ILog myLog = log4net.LogManager.GetLogger(typeof(ClassificationParameterToPriceMySqlDAL));

        #region CURD

        /// <summary>
        /// 查询分类报价属性最大ID
        /// </summary>
        /// <returns></returns>
        public int GetMaxClassificationParameterToPriceID()
        {
            int maxId = 0;
            try
            {
                string sqlCommand = "select * from ClassificationParameterToPrice order by CFParaPriceID DESC limit 0, 1";
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
                myLog.ErrorFormat("GetMaxClassificationParameterToPriceID 获取分类报价属性最大ID失败,异常信息:{0}", ex.Message);
                maxId = -1;
            }
            return maxId;
        }

        /// <summary>
        /// 更新分类报价属性
        /// </summary>
        /// <param name="productTable"></param>
        /// <param name="errorCount"></param>
        /// <returns></returns>
        public bool UpdateClassificationParameterToPrice(DataTable productTable, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            try
            {
                string strPlaceholder = string.Empty;
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("replace into ClassificationParameterToPrice ( " + parmsKey + " ) values ");
                for (int i = 0; i < productTable.Rows.Count; i++)
                {
                    var dr = productTable.Rows[i];
                    var Placeholder = string.Format(@"({0},{1},{2},'{3}','{4}','{5}',{6})",
                                     dr["CFParaPriceID"].ToInt(), dr["CFID"].ToInt(), dr["FatherCFID"].ToInt()
                                     , dr["CFParaPriceName"].ToString().Replace("\'", "\""), dr["CFParaPriceValue"].ToString().Replace("\'", "\""), dr["CFParaPriceProp"].ToString().Replace("\'", "\"")
                                     , dr["Sort"].ToShort());
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
                myLog.ErrorFormat("UpdateClassificationParameterToPrice 更新分类报价属性表失败,分类报价属性ID{0}-{1},异常信息:{2}", productTable.Rows[0]["CFParaPriceID"], productTable.Rows[productTable.Rows.Count - 1]["CFParaPriceID"], ex.Message);
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// 添加分类报价属性
        /// </summary>
        /// <param name="productTable"></param>
        /// <param name="errorCount"></param>
        /// <returns></returns>
        public bool AddClassificationParameterToPrice(DataTable productTable, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            try
            {
                string strPlaceholder = string.Empty;
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("insert into ClassificationParameterToPrice ( " + parmsKey + " ) values ");
                for (int i = 0; i < productTable.Rows.Count; i++)
                {
                    var dr = productTable.Rows[i];
                    var Placeholder = string.Format(@"({0},{1},{2},'{3}','{4}','{5}',{6})",
                                     dr["CFParaPriceID"].ToInt(), dr["CFID"].ToInt(), dr["FatherCFID"].ToInt()
                                     ,dr["CFParaPriceName"].ToString().Replace("\'", "\""), dr["CFParaPriceValue"].ToString().Replace("\'", "\""), dr["CFParaPriceProp"].ToString().Replace("\'", "\"")
                                     ,dr["Sort"].ToShort());
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
                myLog.ErrorFormat("AddClassificationParameterToPrice 添加分类报价属性表失败,分类报价属性ID{0}-{1},异常信息:{2}", productTable.Rows[0]["CFParaPriceID"], productTable.Rows[productTable.Rows.Count - 1]["CFParaPriceID"], ex.Message);
                flag = false;
            }
            return flag;
        }

        #endregion

        private string parmsKey = string.Format(@"CFParaPriceID,CFID,FatherCFID,CFParaPriceName,CFParaPriceValue,CFParaPriceProp,Sort");
    }
}
