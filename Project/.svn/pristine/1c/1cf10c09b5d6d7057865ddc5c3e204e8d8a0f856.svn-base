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
    public class BelowLinePromotionDAL
    {
        private static Database dbr = JXUsersMySqlData.Writer;
        private static Database dbw = JXUsersMySqlData.Reader;


        /// <summary>
        /// 添加 BelowLinePromotion
        /// </summary>
        /// <param name="belowLinePromotionInfo">实体</param>
        /// <returns></returns>
        public bool BelowLinePromotion_Insert(BelowLinePromotionInfo belowLinePromotionInfo) 
        {
            string sql = string.Empty;
            sql = " INSERT INTO belowlinepromotion ";
            sql += " (InvitationCode,status,TrueName,CreateTime ) ";
            sql += " VALUES ";
            sql += " ('{0}',{1},'{2}','{3}') ";
            sql = string.Format(sql, belowLinePromotionInfo.InvitationCode, belowLinePromotionInfo.Status, belowLinePromotionInfo.TrueName,belowLinePromotionInfo.CreateTime);
            DbCommand cmd =dbr.GetSqlStringCommand(sql);
            var result = dbw.ExecuteNonQuery(cmd);
            return result > 0;
        }


        /// <summary>
        /// 查询 InvitationCode
        /// </summary>
        /// <param name="InvitationCode">主键</param>
        /// <returns></returns>
        public bool BelowLinePromotion_IsExist(string invitationCode)
        {
            try
            {
                string sql = string.Empty;
                sql = " SELECT COUNT(*) FROM belowlinepromotion ";
                sql += " WHERE InvitationCode='{0}'";
                sql = string.Format(sql, invitationCode);
                DbCommand dbCommand = dbr.GetSqlStringCommand(sql);
                int count = Convert.ToInt32(dbr.ExecuteScalar(dbCommand));
                if (count == 0)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return true;
            }
        }

        /// <summary>
        ///  分页查询
        /// </summary>
        /// <param name="startCount">起始页</param>
        /// <param name="size">页面大小</param>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public IList<BelowLinePromotionInfo> BelowLinePromotion_GetList(int startCount, int size, string strWhere) 
        {
            IList<BelowLinePromotionInfo> blpInfoList = new List<BelowLinePromotionInfo>();
            string sql = string.Empty;
            sql = " SELECT * FROM belowlinepromotion";
            sql +=" WHERE 1=1 ";
            sql += strWhere;
            sql += " ORDER BY CreateTime DESC LIMIT {0},{1} ";
            sql = string.Format(sql,startCount,size);
            DbCommand cmd = dbr.GetSqlStringCommand(sql);
            using (IDataReader dataReader = dbr.ExecuteReader(cmd))
            {
                while (dataReader.Read())
                {
                    blpInfoList.Add(RecoverModel(dataReader));
                }
            }
            return blpInfoList;
        }

        public int  BelowLinePromotion_GetCount(string strWhere)
        {
            int count = 0;
            string sql = string.Empty;
            sql = " SELECT COUNT(*) FROM belowlinepromotion";
            sql += " WHERE 1=1 ";
            sql += strWhere;
            DbCommand dbCommand = dbr.GetSqlStringCommand(sql);
            count = Convert.ToInt32(dbr.ExecuteScalar(dbCommand));
            return count;
        }

        private BelowLinePromotionInfo RecoverModel(IDataReader dataReader) 
        {
            BelowLinePromotionInfo blpInfo = new BelowLinePromotionInfo();
            blpInfo.InvitationCode = dataReader["InvitationCode"].ToString();
            blpInfo.Status = dataReader["Status"].ToInt();
            blpInfo.CreateTime = dataReader["CreateTime"].ToDateTime();
            blpInfo.TrueName = dataReader["TrueName"].ToString();
            return blpInfo;
        }
    }
}
