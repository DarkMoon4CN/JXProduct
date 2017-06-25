using JXAPI.Component.DataAccess;
using log4net;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace JXAPI.Component.SQLServerDAL
{
    public class JXYXBrandMySqlDAL
    {
        private static Database dbw = JXYXMySqlData.Writer;
        private ILog myLog = log4net.LogManager.GetLogger(typeof(JXYXBrandMySqlDAL));

        #region MySql 品牌表相关操作

        public int GetMaxBrandID()
        {
            int maxId = 0;
            try
            {
                string sqlCommand = "select * from brand order by BrandID DESC limit 0, 1";
                var cmd = dbw.GetSqlStringCommand(sqlCommand);
                if (dbw.ExecuteScalar(cmd).IsNotNULL())
                {
                    maxId = Convert.ToInt32(dbw.ExecuteScalar(cmd));
                }
                else
                {
                    maxId = 0;
                }
            }
            catch (Exception ex)
            {
                myLog.ErrorFormat("GetMaxBrandID 获取品牌最大ID失败,异常信息:{0}", ex.Message);
                maxId = -1;
            }
            return maxId;
        }

        public bool DeleteBrand()
        {
            bool flag = true;
            try
            {
                string sql = "delete from brand";
                var cmd = dbw.GetSqlStringCommand(sql);
                dbw.ExecuteNonQuery(cmd);
            }
            catch(Exception ex)
            {
                myLog.ErrorFormat("DeleteBrand 删除品牌信息失败失败,异常信息:{0}", ex.Message);
                flag = false;
            }
            return flag;
        }

        public bool AddBrand(DataTable productTable, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            if(!DeleteBrand())
            {
                return false;
            }
            try
            {
                string strPlaceholder = string.Empty;
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("insert into brand ( " + parmsKey + " ) values ");
                for (int i = 0; i < productTable.Rows.Count; i++)
                {
                    var dr = productTable.Rows[i];
                    var Placeholder = string.Format(@"({0},'{1}','{2}','{3}','{4}')",
                                     dr["BrandID"].ToInt(), dr["CFID"].ToInt(), dr["ChineseName"].ToString().Replace("\'", "\""), dr["ImageLogo"].ToString(), dr["Sort"].ToInt());
                    
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
                myLog.ErrorFormat("AddBrand 添加品牌失败,品牌ID：{0}-{1},异常信息:{2}", productTable.Rows[0]["BrandID"], productTable.Rows[productTable.Rows.Count - 1]["BrandID"], ex.Message);
                flag = false;
            }
            return flag;
        }

        #endregion

        private string parmsKey = string.Format(@"BrandID,CFID,BrandName,Image,Sort");
    }
}
