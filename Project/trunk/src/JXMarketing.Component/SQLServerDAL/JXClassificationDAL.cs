namespace JXProduct.Component.SQLServerDAL
{
    using JXProduct.Component.DataAccess;
    using JXProduct.Component.Model;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Text;

    public class JXClassificationDAL
    {
        private static Database dbr = JXProductData.Writer;
        private static Database dbw = JXProductData.Reader;

        #region JX分类

        //获取分类
        internal List<JXClassificationInfo> JXClassification_GetList()
        {
            var sql = "SELECT * FROM JXClassification AS j";
            var cmd = dbr.GetStoredProcCommand(sql);
            var list = new List<JXClassificationInfo>();
            using (var read = dbr.ExecuteReader(cmd))
            {
                while (read.Read())
                {
                    list.Add(this.ReaderJXCFModel(read));
                }
            }
            return list;
        }

        #endregion

        #region JX分类属性  JXClassification + ProductJXParameterValue

        //获取分类->说明书字段
        internal List<JXClassificationParameterInfo> JXClassificationParameter_GetByJXCFID(int jxcfid)
        {
            var sql = "SELECT * FROM JXClassificationParameter AS jp WHERE jp.JXCFID = " + jxcfid;
            var cmd = dbr.GetStoredProcCommand(sql);
            var list = new List<JXClassificationParameterInfo>();
            using (var read = dbr.ExecuteReader(cmd))
            {
                while (read.Read())
                {
                    list.Add(this.ReaderJXParaModel(read));
                }
            }
            return list;

        }

        //插入特定值 ->ProductJXParameterValue
        internal bool ProductJXParameterValue_Add(int productid, Dictionary<int, string> dic)
        {
            var s = new StringBuilder();
            if (productid > 0 && dic != null && dic.Count > 0)
            {
                s.AppendFormat("DELETE FROM ProductJXParameterValue WHERE ProductID ={0};", productid);
                s.Append("INSERT into ProductJXParameterValue(JXParaID,ProductID,paravalue)");

                var f = false;
                foreach (var d in dic)
                {
                    if (f || !(f = true))
                    {
                        s.Append(" UNION ALL ");
                    }
                    s.AppendFormat("SELECT {0},{1},'{2}' ", d.Key, productid, d.Value);
                }
                s.Append(";");
            }
            DbCommand dbcommand = dbw.GetSqlStringCommand(s.ToString());
            return dbw.ExecuteNonQuery(dbcommand) > 0;
        }

        #endregion

        #region 商品ID -> 说明书value

        //获取当前分类下的value值
        internal List<JXProductParameterValueInfo> ProductJXParameterValue_GetList(int productID)
        {
            string sql = "ProductJXParameterValue_GetList";
            DbCommand dbcommand = dbr.GetStoredProcCommand(sql);
            dbr.AddInParameter(dbcommand, "ProductID", DbType.Int32, productID);
            using (var read = dbr.ExecuteReader(dbcommand))
            {
                var list = new List<JXProductParameterValueInfo>();
                while (read.Read())
                {
                    list.Add(new JXProductParameterValueInfo
                    {
                        ProductID = read.GetInt32(0),
                        JXParaID = read.GetInt32(1),
                        ParaValue = read.GetString(2)

                    });
                }
                return list;
            }
        }

        #endregion

        #region 转换数据模型 ReaderModel

        //金象分类
        /// <summary>
        /// Reader->JXClassification对象
        /// </summary>
        private JXClassificationInfo ReaderJXCFModel(IDataReader dataReader)
        {
            var jxinfo = new JXClassificationInfo();

            jxinfo.JXCFID = int.Parse(dataReader["JXCFID"].ToString());
            jxinfo.ChineseName = dataReader["ChineseName"].ToString();
            jxinfo.PinyinName = dataReader["PinyinName"].ToString();
            jxinfo.ImagesLogo = dataReader["ImagesLogo"].ToString();
            jxinfo.Level = short.Parse(dataReader["Level"].ToString());
            jxinfo.Path = dataReader["Path"].ToString();
            jxinfo.ParentID = int.Parse(dataReader["ParentID"].ToString());
            jxinfo.PathCount = short.Parse(dataReader["PathCount"].ToString());
            jxinfo.Title = dataReader["Title"].ToString();
            jxinfo.Keywords = dataReader["Keywords"].ToString();
            jxinfo.Description = dataReader["Description"].ToString();
            jxinfo.Sort = int.Parse(dataReader["Sort"].ToString());
            jxinfo.Status = short.Parse(dataReader["Status"].ToString());

            return jxinfo;
        }

        //金象分类属性
        /// <summary>
        /// Reader->JXClassificationParameter对象
        /// </summary>
        private JXClassificationParameterInfo ReaderJXParaModel(IDataReader dataReader)
        {
            var jxparaInfo = new JXClassificationParameterInfo();
            jxparaInfo.JXParaID = int.Parse(dataReader["JXParaID"].ToString());
            jxparaInfo.JXCFID = int.Parse(dataReader["JXCFID"].ToString());
            jxparaInfo.ParaName = dataReader["ParaName"].ToString();
            jxparaInfo.ParaType = short.Parse(dataReader["ParaType"].ToString());
            jxparaInfo.ParaLength = int.Parse(dataReader["ParaLength"].ToString());
            jxparaInfo.ParaValueList = dataReader["ParaValueList"].ToString();
            jxparaInfo.Sort = int.Parse(dataReader["Sort"].ToString());
            jxparaInfo.IsSearch = short.Parse(dataReader["IsSearch"].ToString());
            jxparaInfo.IsNull = short.Parse(dataReader["IsNull"].ToString());
            jxparaInfo.ParaLabel = short.Parse(dataReader["ParaLabel"].ToString());
            return jxparaInfo;
        }

        #endregion
    }
}