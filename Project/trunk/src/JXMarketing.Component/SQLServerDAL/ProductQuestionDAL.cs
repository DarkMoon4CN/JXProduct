using JXProduct.Component.DataAccess;
using JXProduct.Component.Model;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace JXProduct.Component.SQLServerDAL
{
    public class ProductQuestionDAL
    {
        private static Database dbr = JXProductData.Writer;
        private static Database dbw = JXProductData.Reader;


        #region CURD

        //回复/审核
        internal bool ProductQuestion_Answer(int questionid, string answer, short status)
        {
            var sql = "ProductQuestion_Answer";
            var cmd = dbw.GetSqlStringCommand(sql);
            dbw.AddInParameter(cmd, "questionid", DbType.Int32, questionid);
            dbw.AddInParameter(cmd, "answer", DbType.String, answer);
            dbw.AddInParameter(cmd, "status", DbType.Int16, status);
            var result = dbw.ExecuteNonQuery(cmd);
            return result > 0;
        }

        internal IList<ProductQuestionInfo> ProductQuestion_GetList(int pageIndex, int pageSize, string orderType, string strWhere, out int recordCount)
        {
            IList<ProductQuestionInfo> modelList = new List<ProductQuestionInfo>();

            string sqlCommand = "ProductQuestion_GetList";
            DbCommand dbCommand = dbr.GetStoredProcCommand(sqlCommand);

            dbr.AddInParameter(dbCommand, "PageIndex", DbType.Int32, pageIndex);
            dbr.AddInParameter(dbCommand, "PageSize", DbType.Int32, pageSize);
            dbr.AddInParameter(dbCommand, "OrderType", DbType.String, orderType);
            dbr.AddInParameter(dbCommand, "StrWhere", DbType.String, strWhere);
            dbr.AddOutParameter(dbCommand, "RecordCount", DbType.Int32, 4);

            using (IDataReader read = dbr.ExecuteReader(dbCommand))
            {
                while (read.Read())
                {
                    modelList.Add(readerModel(read));
                }
            }

            recordCount = int.Parse(dbr.GetParameterValue(dbCommand, "RecordCount").ToString());

            return modelList;
        }

        private ProductQuestionInfo readerModel(IDataReader read)
        {
            ProductQuestionInfo model = new ProductQuestionInfo();

            model.QuestionID = int.Parse(read["QuestionID"].ToString());
            model.ProductID = int.Parse(read["ProductID"].ToString());
            model.UID = int.Parse(read["UID"].ToString());
            model.UserName = read["UserName"].ToString();
            model.Question = read["Question"].ToString();
            model.Answer = read["Answer"].ToString();
            model.Anonymous = bool.Parse(read["Anonymous"].ToString());
            model.AskTime = DateTime.Parse(read["AskTime"].ToString());
            model.AnswerTime = DateTime.Parse(read["AnswerTime"].ToString());
            model.FlowerCount = int.Parse(read["FlowerCount"].ToString());
            model.EggCount = int.Parse(read["EggCount"].ToString());
            model.Status = short.Parse(read["Status"].ToString());

            return model;
        }

        #endregion
    }
}
