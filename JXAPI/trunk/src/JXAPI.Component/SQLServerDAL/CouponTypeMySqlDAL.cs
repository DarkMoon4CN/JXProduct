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
    public class CouponTypeMySqlDAL
    {
        private static Database dbw = JXCouponBaseMySqlData.Writer;
        private static Database dbr = JXCouponBaseMySqlData.Reader;
        private ILog myLog = log4net.LogManager.GetLogger(typeof(CouponTypeMySqlDAL));

        #region MySql 优惠券类型信息表相关操作


        /// <summary>
        /// 查询优惠券类型信息最大ID
        /// </summary>
        /// <returns></returns>
        public int GetMaxCouponTypeID()
        {
            int maxId = 0;
            try
            {
                string sqlCommand = "select * from coupontype order by TypeID DESC limit 0, 1";
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
                myLog.ErrorFormat("GetMaxCouponTypeID 获取优惠券类型信息最大ID失败,异常信息:{0}", ex.Message);
                maxId = -1;
            }
            return maxId;
        }

        /// <summary>
        /// 更新优惠券类型信息
        /// </summary>
        /// <param name="productTable"></param>
        /// <param name="errorCount"></param>
        /// <returns></returns>
        public bool UpdateCouponType(DataTable productTable, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            try
            {
                string strPlaceholder = string.Empty;
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("replace into coupontype ( " + parmsKey + " ) values ");
                for (int i = 0; i < productTable.Rows.Count; i++)
                {
                    var dr = productTable.Rows[i];
                    var Placeholder = string.Format(@"({0},{1},'{2}','{3}','{4}',{5},{6},{7},{8},{9},{10},{11},{12},'{13}','{14}',{15},'{16}')",
                                     dr["TypeID"].ToInt(), dr["TypeCategory"].ToShort(), dr["TypeName"].ToString().Replace("\'", "\""), dr["PaymentMethod"].ToString().Replace("\'", "\"")
                                     , dr["ProductInfo"].ToString().Replace("\'", "\""), dr["ProductPriceMin"].ToDecimal(), dr["OrderMin"].ToDecimal(), dr["IsToShipFee"].ToShort()
                                     , dr["CouponChannel"].ToShort(), dr["IsExceItem"].ToShort(), dr["NominalValue"].ToDecimal(), dr["Preferential"].ToDecimal()
                                     , 0, dr["CreateTime"].ToDateTime().ToString(), dr["Creator"].ToString().Replace("\'", "\""), dr["Status"].ToShort()
                                     , dr["Remarks"].ToString().Replace("\'", "\""));
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
                myLog.ErrorFormat("UpdateCouponType 更新优惠券类型信息表失败,优惠券类型信息ID：{0}-{1},异常信息:{2}", productTable.Rows[0]["TypeID"], productTable.Rows[productTable.Rows.Count - 1]["TypeID"], ex.Message);
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// 添加优惠券类型信息
        /// </summary>
        /// <param name="productTable"></param>
        /// <param name="errorCount"></param>
        /// <returns></returns>
        public bool AddCouponType(DataTable productTable, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            try
            {
                string strPlaceholder = string.Empty;
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("insert into coupontype ( " + parmsKey + " ) values ");
                for (int i = 0; i < productTable.Rows.Count; i++)
                {
                    var dr = productTable.Rows[i];
                    var Placeholder = string.Format(@"({0},{1},'{2}','{3}','{4}',{5},{6},{7},{8},{9},{10},{11},{12},'{13}','{14}',{15},'{16}')",
                                     dr["TypeID"].ToInt(), dr["TypeCategory"].ToShort(), dr["TypeName"].ToString().Replace("\'", "\""), dr["PaymentMethod"].ToString().Replace("\'", "\"")
                                     , dr["ProductInfo"].ToString().Replace("\'", "\""), dr["ProductPriceMin"].ToDecimal(), dr["OrderMin"].ToDecimal(), dr["IsToShipFee"].ToShort()
                                     , 0, dr["IsExceItem"].ToShort(), dr["NominalValue"].ToDecimal(), dr["Preferential"].ToDecimal()
                                     , 0, dr["CreateTime"].ToDateTime().ToString(), dr["Creator"].ToString().Replace("\'", "\""), dr["Status"].ToShort()
                                     , dr["Remarks"].ToString().Replace("\'", "\""));
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
                myLog.ErrorFormat("AddCouponType 添加优惠券类型信息失败,优惠券类型信息ID：{0}-{1},异常信息:{2}", productTable.Rows[0]["TypeID"], productTable.Rows[productTable.Rows.Count - 1]["TypeID"], ex.Message);
                flag = false;
            }
            return flag;
        }

        #endregion

        private string parmsKey = string.Format(@"TypeID,TypeCategory,TypeName,PaymentMethod,ProductInfo,ProductPriceMin,OrderMin,ToShipFee,CouponChannel,ToSpecial,NominalValue,Preferential,UseType,
                                                CreateTime,Creator,Status,Remarks");
    }
}
