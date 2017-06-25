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
    public class ProductQuestionMySqlDAL
    {
        private static Database dbw = JXProductMySqlData.Writer;
        private static Database dbr = JXProductMySqlData.Reader;
        private ILog myLog = log4net.LogManager.GetLogger(typeof(ProductQuestionMySqlDAL));

        #region CURD

        /// <summary>
        /// 查询商品咨询最大ID
        /// </summary>
        /// <returns></returns>
        public int GetMaxProductQuestionID()
        {
            int maxId = 0;
            try
            {
                string sqlCommand = "select * from ProductQuestion order by QuestionID DESC limit 0, 1";
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
                myLog.ErrorFormat("GetMaxProductQuestionID 获取商品咨询最大ID失败,异常信息:{0}", ex.Message);
                maxId = -1;
            }
            return maxId;
        }

        /// <summary>
        /// 更新商品咨询
        /// </summary>
        /// <param name="productTable"></param>
        /// <param name="errorCount"></param>
        /// <returns></returns>
        public bool UpdateProductQuestion(DataTable productTable, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            try
            {
                string strPlaceholder = string.Empty;
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("replace into ProductQuestion ( " + parmsKey + " ) values ");
                for (int i = 0; i < productTable.Rows.Count; i++)
                {
                    var dr = productTable.Rows[i];
                    var Placeholder = string.Format(@"({0},{1},{2},'{3}','{4}','{5}',{6},'{7}','{8}',{9},{10},{11})",
                                     dr["QuestionID"].ToInt(), dr["ProductID"].ToInt(), dr["UID"].ToInt(), dr["UserName"].ToString().Replace("\'", "\""), dr["Question"].ToString().Replace("\'", "\""), dr["Answer"].ToString().Replace("\'", "\"")
                                     , Convert.ToBoolean(dr["Anonymous"]) ? 1 : 0, dr["AskTime"].ToDateTime().ToString(), dr["AnswerTime"].ToDateTime().ToString(), dr["FlowerCount"].ToInt(), dr["EggCount"].ToInt(), dr["Status"].ToShort());
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
                myLog.ErrorFormat("UpdateProductQuestion 更新商品咨询表失败,商品咨询ID{0}-{1},异常信息:{2}", productTable.Rows[0]["QuestionID"], productTable.Rows[productTable.Rows.Count - 1]["QuestionID"], ex.Message);
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// 添加商品咨询
        /// </summary>
        /// <param name="productTable"></param>
        /// <param name="errorCount"></param>
        /// <returns></returns>
        public bool AddProductQuestion(DataTable productTable, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            try
            {
                string strPlaceholder = string.Empty;
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("insert into ProductQuestion ( " + parmsKey + " ) values ");
                for (int i = 0; i < productTable.Rows.Count; i++)
                {
                    var dr = productTable.Rows[i];
                    var Placeholder = string.Format(@"({0},{1},{2},'{3}','{4}','{5}',{6},'{7}','{8}',{9},{10},{11})",
                                     dr["QuestionID"].ToInt(), dr["ProductID"].ToInt(), dr["UID"].ToInt(), dr["UserName"].ToString().Replace("\'", "\""), dr["Question"].ToString().Replace("\'", "\""), dr["Answer"].ToString().Replace("\'", "\"")
                                     , Convert.ToBoolean(dr["Anonymous"]) ? 1 : 0, dr["AskTime"].ToDateTime().ToString(), dr["AnswerTime"].ToDateTime().ToString(), dr["FlowerCount"].ToInt(), dr["EggCount"].ToInt(), dr["Status"].ToShort());
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
                myLog.ErrorFormat("AddProductQuestion 添加商品咨询表失败,商品咨询ID{0}-{1},异常信息:{2}", productTable.Rows[0]["QuestionID"], productTable.Rows[productTable.Rows.Count - 1]["QuestionID"], ex.Message);
                flag = false;
            }
            return flag;
        }

        #endregion

        private string parmsKey = string.Format(@"QuestionID,ProductID,UID,UserName,Question,Answer,Anonymous,AskTime,AnswerTime,FlowerCount,
                                                EggCount,Status");
    }
}
