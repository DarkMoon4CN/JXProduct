using JXProduct.Component.DataAccess;
using JXProduct.Component.Model;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
namespace JXProduct.Component.SQLServerDAL
{
    public class ProductEvaluationDAL
    {
        private static Database dbr = JXProductData.Writer;
        private static Database dbw = JXProductData.Reader;

        #region CURD

        /// <summary>
        /// ProductEvaluation_Insert Method		
        /// </summary>
        /// <param name="ProductEvaluationInfo">ProductEvaluation object</param>
        /// <returns></returns>
        internal int ProductEvaluation_Insert(ProductEvaluationInfo model)
        {
            string sqlCommand = "ProductEvaluation_Insert";
            DbCommand dbCommand = dbw.GetStoredProcCommand(sqlCommand);

            dbw.AddOutParameter(dbCommand, "EvaluationID", DbType.Int64, 4);
            dbw.AddInParameter(dbCommand, "ProductID", DbType.Int32, model.ProductID);
            dbw.AddInParameter(dbCommand, "ProductCode", DbType.String, model.ProductCode);
            dbw.AddInParameter(dbCommand, "UID", DbType.Int32, model.UID);
            dbw.AddInParameter(dbCommand, "UserName", DbType.String, model.UserName);
            dbw.AddInParameter(dbCommand, "EvalTime", DbType.DateTime, model.EvalTime);
            dbw.AddInParameter(dbCommand, "Title", DbType.String, model.Title);
            dbw.AddInParameter(dbCommand, "Content", DbType.String, model.Content);
            dbw.AddInParameter(dbCommand, "Score", DbType.Int16, model.Score);
            dbw.AddInParameter(dbCommand, "ScoreDes", DbType.Decimal, model.ScoreDes);
            dbw.AddInParameter(dbCommand, "ScoreService", DbType.Decimal, model.ScoreService);
            dbw.AddInParameter(dbCommand, "ScoreSend", DbType.Decimal, model.ScoreSend);
            dbw.AddInParameter(dbCommand, "ScoreLogistics", DbType.Decimal, model.ScoreLogistics);
            dbw.AddInParameter(dbCommand, "FlowerCount", DbType.Int32, model.FlowerCount);
            dbw.AddInParameter(dbCommand, "EggCount", DbType.Int32, model.EggCount);
            dbw.AddInParameter(dbCommand, "OrderID", DbType.String, model.OrderID);
            dbw.AddInParameter(dbCommand, "Status", DbType.Int16, model.Status);
            dbw.AddInParameter(dbCommand, "UpdateTime", DbType.DateTime, model.UpdateTime);
            dbw.AddInParameter(dbCommand, "Source", DbType.Int32, model.Source);
            dbw.ExecuteNonQuery(dbCommand);

            return int.Parse(dbw.GetParameterValue(dbCommand, "EvaluationID").ToString());
        }

        /// <summary>
        /// ProductEvaluation_Get Method
        /// </summary>
        /// <param name="evaluationID">ProductEvaluation Main ID</param>
        /// <returns>ProductEvaluationInfo get from ProductEvaluation table.</returns>	
        internal ProductEvaluationInfo ProductEvaluation_Get(long evaluationID)
        {
            ProductEvaluationInfo model = null;

            string sqlCommand = "ProductEvaluation_Get";
            DbCommand dbCommand = dbr.GetStoredProcCommand(sqlCommand);

            dbr.AddInParameter(dbCommand, "EvaluationID", DbType.Int64, evaluationID);

            using (IDataReader dataReader = dbr.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    model = RecoverModel(dataReader);
                }
            }

            return model;
        }

        /// <summary>
        /// ProductEvaluation_GetList Method
        /// </summary>
        /// <param name="pageIndex">起始页码</param>
        /// <param name="pageSize">每页数据数</param>
        /// <param name="orderType">设置排序，'':没有排序要求 0：主键升序 1：主键降序 字符串：用户自定义排序规则 如：‘SubmitTime DESC , ID DESC’</param>
        /// <param name="strWhere">查询条件(注意: 不要加 WHERE)</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>A Generic List of ProductEvaluationInfo</returns>
        internal IList<ProductEvaluationInfo> ProductEvaluation_GetList(int pageIndex, int pageSize, string orderType, string strWhere, out int recordCount)
        {
            IList<ProductEvaluationInfo> modelList = new List<ProductEvaluationInfo>();

            string sqlCommand = "ProductEvaluation_GetList";
            DbCommand dbCommand = dbr.GetStoredProcCommand(sqlCommand);

            dbr.AddInParameter(dbCommand, "PageIndex", DbType.Int32, pageIndex);
            dbr.AddInParameter(dbCommand, "PageSize", DbType.Int32, pageSize);
            dbr.AddInParameter(dbCommand, "OrderType", DbType.String, orderType);
            dbr.AddInParameter(dbCommand, "StrWhere", DbType.String, strWhere);
            dbr.AddOutParameter(dbCommand, "RecordCount", DbType.Int32, 4);

            using (IDataReader dataReader = dbr.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    modelList.Add(RecoverModel(dataReader));
                }
            }

            recordCount = int.Parse(dbr.GetParameterValue(dbCommand, "RecordCount").ToString());

            return modelList;
        }


        /// <summary>
        /// ProductEvaluation_GetByPName Method
        /// </summary>
        /// <param name="pageIndex">起始页码</param>
        /// <param name="pageSize">每页数据数</param>
        /// <param name="orderType">字符串：用户自定义排序规则 如：‘SubmitTime DESC , ID DESC’</param>
        /// <param name="pName">需要查询的产品名称</param>
        /// <param name="strWhere">查询条件(注意: 不要加 WHERE)</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>A Generic List of ProductEvaluationInfo</returns>
        internal IList<ProductEvaluationInfo> ProductEvaluation_GetByPName(int pageIndex, int pageSize, string orderType,string pName ,string strWhere, out int recordCount)
        {
            IList<ProductEvaluationInfo> modelList = new List<ProductEvaluationInfo>();

            string sqlCommand = "ProductEvaluation_GetByPName";
            DbCommand dbCommand = dbr.GetStoredProcCommand(sqlCommand);

            dbr.AddInParameter(dbCommand, "PageIndex", DbType.Int32, pageIndex);
            dbr.AddInParameter(dbCommand, "PageSize", DbType.Int32, pageSize);
            dbr.AddInParameter(dbCommand, "OrderType", DbType.String, orderType);
            dbr.AddInParameter(dbCommand, "PName", DbType.String, pName);
            dbr.AddInParameter(dbCommand, "StrWhere", DbType.String, strWhere);
            dbr.AddOutParameter(dbCommand, "RecordCount", DbType.Int32, 4);

            using (IDataReader dataReader = dbr.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    modelList.Add(RecoverModel(dataReader));
                }
            }
            recordCount = int.Parse(dbr.GetParameterValue(dbCommand, "RecordCount").ToString());
            return modelList;
        }

        /// <summary>
        /// 从 IDataReader 中恢复ProductEvaluation对象
        /// </summary>
        /// <param name="IDataReader"></param>
        /// <returns></returns>
        private ProductEvaluationInfo RecoverModel(IDataReader dataReader)
        {
            ProductEvaluationInfo info = new ProductEvaluationInfo();

            info.EvaluationID = long.Parse(dataReader["EvaluationID"].ToString());
            info.ProductID = int.Parse(dataReader["ProductID"].ToString());
            info.ProductCode = dataReader["ProductCode"].ToString();
            info.UID = int.Parse(dataReader["UID"].ToString());
            info.UserName = dataReader["UserName"].ToString();
            info.EvalTime = dataReader["EvalTime"].ToDateTime();
            info.Title = dataReader["Title"].ToString();
            info.Content = dataReader["Content"].ToString();
            info.Score = short.Parse(dataReader["Score"].ToString());
            info.ScoreDes = decimal.Parse(dataReader["ScoreDes"].ToString());
            info.ScoreService = decimal.Parse(dataReader["ScoreService"].ToString());
            info.ScoreSend = decimal.Parse(dataReader["ScoreSend"].ToString());
            info.ScoreLogistics = decimal.Parse(dataReader["ScoreLogistics"].ToString());
            info.FlowerCount = int.Parse(dataReader["FlowerCount"].ToString());
            info.EggCount = int.Parse(dataReader["EggCount"].ToString());
            info.OrderID = dataReader["OrderID"].ToString();
            info.Status = short.Parse(dataReader["Status"].ToString());
            info.UpdateTime = dataReader["UpdateTime"].ToDateTime();
            info.Source = dataReader["Source"].ToString();
            return info;
        }

        /// <summary>
        /// 以评价流水号更新评价状态
        /// </summary>
        /// <param name="evalID">评价流水号</param>
        /// <param name="statusType">需要更新的状态</param>
        /// <returns></returns>
        public bool ProductEvaluation_Update(int evalID, int statusType)
        {
            var sql = "UPDATE [ProductEvaluation] SET Status ='" + statusType + "' WHERE EvaluationID='" + evalID + "' ";
            var cmd = dbw.GetSqlStringCommand(sql);
            return dbw.ExecuteNonQuery(cmd) > 0;
        }

        #endregion
    }
}