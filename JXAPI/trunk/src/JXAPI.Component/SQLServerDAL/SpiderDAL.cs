using JXAPI.Common;
using JXAPI.Component.DataAccess;
using JXAPI.Component.Model;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace JXAPI.Component.SQLServerDAL
{
    public class SpiderDAL
    {
        private static Database dbw = JXProductData.Writer;
        private static Database dbr = JXProductData.Reader;
        /// <summary>
        ///  写入数据表事务提交
        /// </summary>
        /// <param name="spInfolist"></param>
        /// <returns></returns>
        public OperationResult<bool> InsertSpider(IList<SpiderProductInfo> spInfolist)
        {
             DbConnection con = null;
             DbTransaction transcation = null;
             try
             {
                 using (con = dbw.CreateConnection())
                 {
                     con.Open();
                     transcation = con.BeginTransaction();
                     using (var command = con.CreateCommand())
                     {
                        command.Transaction = transcation;
                        var sql = string.Empty;
                        sql += " INSERT INTO Spider ";
                        sql += " (SpiderPID,ProductName,Url,KeyName,KeyValue ) ";
                        sql += " VALUES ";
                        sql += " (@SpiderPID,@ProductName,@Url,@KeyName,@KeyValue) ";
                        command.Connection = con;
                        command.CommandText = sql;
                       
                        for (int i = 0; i < spInfolist.Count; i++)
                        {
                            command.Parameters.Clear();
                            SpiderProductInfo spInfo = spInfolist[i];
                            DbParameter spiderPID=command.CreateParameter();
                            spiderPID.DbType = DbType.Int32;
                            spiderPID.Value = spInfo.SpiderPID;
                            spiderPID.ParameterName = "SpiderPID";
                            command.Parameters.Add(spiderPID);

                            DbParameter productName = command.CreateParameter();
                            productName.DbType = DbType.String;
                            productName.Value = spInfo.ProductName;
                            productName.ParameterName = "ProductName";
                            command.Parameters.Add(productName);

                            DbParameter url = command.CreateParameter();
                            url.DbType = DbType.String;
                            url.Value = spInfo.Url;
                            url.ParameterName = "Url";
                            command.Parameters.Add(url);

                            DbParameter keyName = command.CreateParameter();
                            keyName.DbType = DbType.String;
                            keyName.Value = spInfo.KeyName;
                            keyName.ParameterName = "KeyName";
                            command.Parameters.Add(keyName);

                            DbParameter keyValue = command.CreateParameter();
                            keyValue.DbType = DbType.String;
                            keyValue.Value = spInfo.KeyValue;
                            keyValue.ParameterName = "KeyValue";
                            command.Parameters.Add(keyValue);

                            command.ExecuteNonQuery();
                        }
                     }
                     transcation.Commit();
                 }
                 return new OperationResult<bool>(OperationResultType.Success, null, true);
             }
             catch (Exception e)
             {
                 if (transcation != null)
                 {
                     transcation.Rollback();
                 }
                 return new OperationResult<bool>(OperationResultType.Error, e.Message, false);
             }
             finally 
             {
                 con.Close();
             }
        }

        /// <summary>
        /// 查询分页  productID
        /// </summary>
        /// <param name="startCount">起始数</param>
        /// <param name="endCount">结束数</param>
        /// <returns>product 商品ID 集合</returns>
        public OperationResult<IList<int>> ProductID_GetList(int startCount,int endCount) 
        {
            IList<int> productIDList = new List<int>();
            try
            {
                var sql = " SELECT p.rowid,ProductID FROM ";
                sql += " (SELECT ROW_NUMBER() OVER(ORDER BY ProductID) AS rowid,ProductID,Selling,Status FROM  product ";
                sql += " WHERE  Selling=1 AND Status=0 ) ";
                sql += " AS p WHERE 1=1 ";
                sql += " AND p.rowid BETWEEN {0} AND {1}  ";
                sql = string.Format(sql, startCount, endCount);
                DbCommand cmd = dbr.GetSqlStringCommand(sql);
                using (IDataReader dataReader = dbr.ExecuteReader(cmd))
                {
                    while (dataReader.Read())
                    {
                        productIDList.Add(dataReader["productID"].ToInt());
                    }
                }
                return new OperationResult<IList<int>>(OperationResultType.Success, null, productIDList);
            }
            catch (Exception e) 
            {
                return new OperationResult<IList<int>>(OperationResultType.Error, e.Message, productIDList);
            }
        }
    }
}
