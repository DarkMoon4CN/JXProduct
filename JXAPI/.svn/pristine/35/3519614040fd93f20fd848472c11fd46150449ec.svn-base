using JXAPI.Common;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JXAPI.ConsoleProduct
{
    public class MySqlProductPlanManager
    {
        #region Update

        /// <summary>
        /// 商品库的数据更新
        /// </summary>
        /// <param name="tbName"></param>
        public static void JXProductUpdate(string tbName, bool updateSqlFlag)
        {
            ColNameManager.GetColNameByTbName(tbName, out colName, out sqlServerColName);
            MySqlProductPlanUpdate(JXAPI.Component.BLL.SqlServerBLL.ProductBLL.Instance, JXAPI.Component.BLL.MySqlBLL.ProductMySqlBLL.Instance, tbName, colName, sqlServerColName, updateSqlFlag);
        }

        /// <summary>
        /// 活动库的数据更新
        /// </summary>
        /// <param name="tbName"></param>
        public static void JXActivityUpdate(string tbName, bool updateSqlFlag)
        {
            ColNameManager.GetColNameByTbName(tbName, out colName, out sqlServerColName);
            MySqlProductPlanUpdate(JXAPI.Component.BLL.SqlServerBLL.ProductBLL.Instance, JXAPI.Component.BLL.MySqlBLL.ActivityMySqlBLL.Instance, tbName, colName, sqlServerColName, updateSqlFlag);
        }

        /// <summary>
        /// 订单库的数据更新
        /// </summary>
        /// <param name="tbName"></param>
        public static void JXOrderUpdate(string tbName, bool updateSqlFlag)
        {
            ColNameManager.GetColNameByTbName(tbName, out colName, out sqlServerColName);
            MySqlProductPlanUpdate(JXAPI.Component.BLL.SqlServerBLL.ProductBLL.Instance, JXAPI.Component.BLL.MySqlBLL.OrderMySqlBLL.Instance, tbName, colName, sqlServerColName, updateSqlFlag);
        }

        /// <summary>
        /// 营销库的数据更新
        /// </summary>
        /// <param name="tbName"></param>
        public static void JXYXUpdate(string tbName, bool updateSqlFlag)
        {
            ColNameManager.GetColNameByTbName(tbName, out colName, out sqlServerColName);
            MySqlProductPlanUpdate(JXAPI.Component.BLL.SqlServerBLL.ProductBLL.Instance, JXAPI.Component.BLL.MySqlBLL.YXMySqlBLL.Instance, tbName, colName, sqlServerColName, updateSqlFlag);
        }

        #endregion

        #region Delete

        /// <summary>
        /// 商品库的数据更新
        /// </summary>
        /// <param name="tbName"></param>
        public static bool JXProductDelete(DataRow dr)
        {
           return MySqlProductPlanDelete(JXAPI.Component.BLL.MySqlBLL.ProductMySqlBLL.Instance,dr);
        }

        /// <summary>
        /// 活动库的数据更新
        /// </summary>
        /// <param name="tbName"></param>
        public static bool JXActivityDelete(DataRow dr)
        {
            return MySqlProductPlanDelete(JXAPI.Component.BLL.MySqlBLL.ActivityMySqlBLL.Instance, dr);
        }

        /// <summary>
        /// 订单库的数据更新
        /// </summary>
        /// <param name="tbName"></param>
        public static bool JXOrderDelete(DataRow dr)
        {
            return MySqlProductPlanDelete(JXAPI.Component.BLL.MySqlBLL.OrderMySqlBLL.Instance, dr);
        }

        /// <summary>
        /// 营销库的数据更新
        /// </summary>
        /// <param name="tbName"></param>
        public static bool JXYXDelete(DataRow dr)
        {
            return MySqlProductPlanDelete(JXAPI.Component.BLL.MySqlBLL.YXMySqlBLL.Instance, dr);
        }

        #endregion
        

        /// <summary>
        /// 更新mysql 相关表的公共方法
        /// </summary>
        /// <param name="iBLL">sql对象</param>
        /// <param name="iMysqlBLL">mysql对象</param>
        /// <param name="tbName">表名</param>
        /// <param name="colName">mysql表对应的列名</param>
        /// <param name="sqlServerColName">sqlServer表对应的列名</param>
        /// <param name="updateSqlFlag">更新sql数据库更新标记</param>
        private static void MySqlProductPlanUpdate(JXAPI.Component.IBLL.ISqlServerBLL.IProductPlanBLL iBLL, JXAPI.Component.IBLL.IMysqlBLL.IProductPlanMySqlBLL iMysqlBLL, string tbName
                                                     , string[] colName, string[] sqlServerColName, bool updateSqlFlag)
        {
            int errorSum = 0;
            Console.WriteLine("===============Mysql {0} BEGIN===================", tbName);
            try
            {
                int maxID = iMysqlBLL.GetMaxID(tbName, colName[0]);
                string errorMsg = string.Empty;
                DataTable dTable = iBLL.GetList(tbName, out errorMsg);
                if (dTable != null && dTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dTable.Rows.Count; i++)
                    {
                        OperationResult<bool> result = null;
                        if (dTable.Rows[i][0].ToInt() > maxID)
                        {
                            result = iMysqlBLL.Add(tbName, colName, sqlServerColName, dTable.Rows[i]);
                        }
                        else
                        {
                            result = iMysqlBLL.Update(tbName, colName, sqlServerColName, dTable.Rows[i]);
                        }
                        if (result.ResultType != OperationResultType.Success)
                        {
                            errorSum++;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(string.Format("{0}表更新数据失败,{1}：{2},失败信息:{3}", tbName, colName[0], dTable.Rows[i][0], result.Message));
                            myLog.ErrorFormat("{0}表更新数据失败,{1}：{2},失败信息:{3}", tbName, colName[0], dTable.Rows[i][0], result.Message);
                        }
                        else
                        {
                            //由于在同步mysql时需要向不同库的相同表中同步更新数据，而在sqlserver中对应的是同一张表；
                            //因此在修改sqlserver的同步标记时，要等到mysql多张表更新完成之后才能去更新；
                            //当mysql对应数据更新成功之后，去更新sql相应的更新标记
                            if (updateSqlFlag)
                            {
                                SqlProductPlanUpdate(tbName, sqlServerColName[0], dTable.Rows[i][0].ToInt());
                            }
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("{0}表已更新{1}条数据", tbName, i + 1);
                        }
                    }
                    if (errorSum != 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(string.Format("{0} 表总共更新完 {1} 条数据,失败数:{2}", tbName, dTable.Rows.Count, errorSum));
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(string.Format("{0} 表总共更新完 {1} 条数据", tbName, dTable.Rows.Count));
                    }
                }
                if (!string.IsNullOrEmpty(errorMsg))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(errorMsg);
                    myLog.ErrorFormat(errorMsg);
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Mysql {0} ERROR：{1}", tbName, ex.Message);
                myLog.ErrorFormat("Mysql {0} ERROR：{1}", tbName, ex.Message);
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("===============Mysql {0}  END===================", tbName);
        }

        /// <summary>
        /// 当mysql数据库相应数据更新完成之后，同时更新sqlserver对应数据的IsPush( 1=需要 0=否)并置为0
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="primaryKey">主键名字</param>
        /// <param name="primaryValue">主键的值</param>
        private static void SqlProductPlanUpdate(string tableName, string primaryKey, int primaryValue)
        {
            try
            {
                OperationResult<bool> result = JXAPI.Component.BLL.SqlServerBLL.ProductBLL.Instance.UpdateProduct(tableName, primaryKey, primaryValue);
                if (result.ResultType != OperationResultType.Success)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(string.Format("{0}表更新IsPush状态失败,主键ID:{1},失败信息:{2}", tableName, primaryValue, result.Message));
                    myLog.ErrorFormat("{0}表更新IsPush状态失败,主键ID:{1},失败信息:{2}", tableName, primaryValue, result.Message);
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("sql {0} Update IsPush ERROR：{1}", tableName, ex.Message);
                myLog.ErrorFormat("sql {0} Update IsPush ERROR：{1}", tableName, ex.Message);
            }
        }


        //更新商品表中的评论数，好评率字段
        public static void UpdateEvaluationField()
        {
            try
            {
                JXAPI.Component.BLL.MySqlBLL.ProductMySqlBLL maSqlBll = JXAPI.Component.BLL.MySqlBLL.ProductMySqlBLL.Instance;
                OperationResult<bool> result = maSqlBll.UpdateEvaluationCount();
                if (result.ResultType != OperationResultType.Success)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(string.Format("商品评论数更新失败"));
                    myLog.ErrorFormat("商品评论数更新失败");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(string.Format("商品评论数更新成功"));
                }
                result = maSqlBll.UpdateEvaluationPraiseRate();
                if (result.ResultType != OperationResultType.Success)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(string.Format("商品好评率更新失败"));
                    myLog.ErrorFormat("商品好评率更新失败");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(string.Format("商品好评率更新成功"));
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(string.Format("商品评论数，好评率更新失败：{0}", ex.Message));
                myLog.ErrorFormat("商品评论数，好评率更新失败：{0}", ex.Message);
            }
        }

        //更新JXHealth库中的Activity表中的LikeUserID，LikeFavor字段
        public static void UpdateHealthActivityField()
        {
            try
            {
                string createTime = System.DateTime.Now.AddMonths(-1).ToString();
                JXAPI.Component.BLL.MySqlBLL.HealthActivityMySqlBLL mySqlBll = JXAPI.Component.BLL.MySqlBLL.HealthActivityMySqlBLL.Instance;
                OperationResult<bool> result = mySqlBll.UpdateHealthActivity(createTime);
                if (result.ResultType != OperationResultType.Success)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(string.Format("更新JXHealth库中的Activity表中的LikeUserID，LikeFavor字段成功"));
                    myLog.ErrorFormat("更新JXHealth库中的Activity表中的LikeUserID，LikeFavor字段成功");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(string.Format("更新JXHealth库中的Activity表中的LikeUserID，LikeFavor字段失败"));
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(string.Format("更新JXHealth库中的Activity表中的LikeUserID，LikeFavor字段失败：{0}", ex.Message));
                myLog.ErrorFormat("更新JXHealth库中的Activity表中的LikeUserID，LikeFavor字段失败：{0}", ex.Message);
            }
        }

        #region 当更新数据完成之后，需要情况mysql相应库的数据

        private static bool MySqlProductPlanDelete(JXAPI.Component.IBLL.IMysqlBLL.IProductPlanMySqlBLL iMysqlBLL,DataRow dr)
        {
            bool flag = true;
            try
            {
                OperationResult<bool> result = iMysqlBLL.Delete(dr);
                if (result.ResultType != OperationResultType.Success)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("mysql {0}{1}表删除数据失败,主键ID:{2},失败信息:{3}", dr["DbName"], dr["TableName"], dr["KeyValue"], result.Message);
                    myLog.ErrorFormat("mysql {0}{1}表删除数据失败,主键ID:{2},失败信息:{3}", dr["DbName"], dr["TableName"], dr["KeyValue"], result.Message);
                    flag = false;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("mysql {0}{1}表删除数据成功,主键ID:{2}", dr["DbName"], dr["TableName"], dr["KeyValue"]);
                }
            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("MYSQL DELETE ERROR：{0}", ex.Message);
                myLog.ErrorFormat("MYSQL DELETE ERROR：{0}", ex.Message);
                flag = false;
            }
            return flag;
        }

        private static void SqlProductPlanDelete(int id)
        {
            try
            {
                OperationResult<bool> result = JXAPI.Component.BLL.SqlServerBLL.ProductBLL.Instance.DeleteProductDeleteItemRecord(id);
                if (result.ResultType != OperationResultType.Success)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("sqlserver DeleteItemRecord表删除数据失败,主键ID:{0},失败信息:{1}", id, result.Message);
                    myLog.ErrorFormat("sqlserver DeleteItemRecord表删除数据失败,主键ID:{0},失败信息:{1}", id, result.Message);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("sqlserver DeleteItemRecord表删除数据成功,主键ID:{0}", id);
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("SQLSERVER DELETE ERROR：{0}", ex.Message);
                myLog.ErrorFormat("SQLSERVER DELETE ERROR：{0}", ex.Message);
            }
        }

        public static void ProductPlanDelete()
        {
            try
            {
                string errorMsg = string.Empty;
                DataTable dt = JXAPI.Component.BLL.SqlServerBLL.ProductBLL.Instance.GetProductDeleteItemRecord(out errorMsg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    var flag = true;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        var dr = dt.Rows[i];
                        var dbName = dr["DbName"].ToString().ToLower();
                        switch (dbName)
                        {
                            case "jxproduct":
                                if (dr["TableName"].ToString().ToLower() == "product")
                                {
                                    continue;
                                }
                                if (dr["TableName"].ToString().ToLower() == "productgift"
                                    || dr["TableName"].ToString().ToLower() == "productrelated")
                                {
                                    flag = JXProductDelete(dr);
                                    flag = JXOrderDelete(dr);
                                }
                                else
                                {
                                    flag = JXProductDelete(dr);
                                }
                                break;
                            case "jxactivity":
                                if (dr["TableName"].ToString().ToLower() == "productactivity")
                                {
                                    continue;
                                }
                                flag = JXActivityDelete(dr);
                                break;
                            case "jxyx":
                                flag = JXYXDelete(dr);
                                break;
                            default:
                                break;
                        }
                        if (flag)
                        {
                            SqlProductPlanDelete(Convert.ToInt32(dr["ID"]));
                        }
                    }
                }
                if (!string.IsNullOrEmpty(errorMsg))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("sql DeleteItemRecord GetProductDeleteItemRecord ERROR：{0}", errorMsg);
                    myLog.ErrorFormat("sql DeleteItemRecord GetProductDeleteItemRecord ERROR：{0}", errorMsg);
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("sql DeleteItemRecord GetProductDeleteItemRecord ERROR：{0}", ex.Message);
                myLog.ErrorFormat("sql DeleteItemRecord GetProductDeleteItemRecord ERROR：{0}", ex.Message);
            }
        }

        #endregion

        private static ILog myLog = log4net.LogManager.GetLogger(typeof(MySqlProductPlanManager));
        private static string[] colName;
        private static string[] sqlServerColName;
    }
}
