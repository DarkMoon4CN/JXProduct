using JXAPI.Component.DataAccess;
using JXAPI.Component.Model;
using log4net;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JXAPI.Component.SQLServerDAL
{
    public class MobilePushDAL
    {
        private static Database dbr = JXMarketingData.Writer;
        private static Database dbw = JXMarketingData.Reader;

        public bool InsertMobilePush(PushMessageInfo pushMessageInfo)
        {
            string parmsKey = string.Empty;
            string strPlaceholder = string.Empty;
            parmsKey = "ChannelID,Section,DataID,Contents,Creator,CreateTime,Status,Updater,UpdateTime,PushType,Template,TargetList,PushCount,TypeID,Url ";
            strPlaceholder = "@ChannelID,@Section,@DataID,@Contents,@Creator,@CreateTime,@Status,@Updater,@UpdateTime,@PushType,@Template,@TargetList,@PushCount,@TypeID,@Url";
            var sql = " INSERT INTO MobilePush ";
            sql += " ( " + parmsKey + ") ";
            sql += " VALUES ";
            sql += string.Format(" (" + strPlaceholder + ") ");
            var cmd = dbw.GetSqlStringCommand(sql);
            dbw.AddInParameter(cmd, "ChannelID", DbType.Int16, pushMessageInfo.ChannelID);
            dbw.AddInParameter(cmd, "Section", DbType.String, pushMessageInfo.Section);
            dbw.AddInParameter(cmd, "DataID", DbType.String, pushMessageInfo.DataID);
            dbw.AddInParameter(cmd, "Contents", DbType.String, pushMessageInfo.Contents);
            dbw.AddInParameter(cmd, "Creator", DbType.String, pushMessageInfo.Creator);
            dbw.AddInParameter(cmd, "CreateTime", DbType.DateTime, pushMessageInfo.CreateTime);
            dbw.AddInParameter(cmd, "Status", DbType.Int16, pushMessageInfo.Status);
            dbw.AddInParameter(cmd, "Updater", DbType.String, pushMessageInfo.Updater);
            dbw.AddInParameter(cmd, "UpdateTime", DbType.DateTime, pushMessageInfo.UpdateTime);
            dbw.AddInParameter(cmd, "PushType", DbType.String, pushMessageInfo.PushType);
            dbw.AddInParameter(cmd, "Template", DbType.Int16, pushMessageInfo.Template);
            dbw.AddInParameter(cmd, "TargetList", DbType.String, pushMessageInfo.TargetList);
            dbw.AddInParameter(cmd, "PushCount", DbType.Int32, pushMessageInfo.PushCount);
            dbw.AddInParameter(cmd, "TypeID", DbType.Int32, pushMessageInfo.TypeID);
            dbw.AddInParameter(cmd, "Url", DbType.String, pushMessageInfo.Url);
            try
            {
                var result = dbw.ExecuteNonQuery(cmd);
                return result > 0;
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="minute">推送间隔,与定时计划时间相同</param>
        /// <returns></returns>
        public List<PushMessageInfo> MobilePush_GetList(int minute, ref string Msg)
        {
            string startTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string endTime = DateTime.Parse(startTime).AddMinutes(30).ToString();
            List<PushMessageInfo> list = new List<PushMessageInfo>();
            try
            {
                var sql = " SELECT * FROM  MobilePush   ";
                sql += " WHERE 1=1 ";
                sql += " AND  Status  IN (0,2) ";
                sql += " AND  ( ISNULL(ExecutionTime,'')='' ";
                sql += " OR ExecutionTime >= '{0}'   ";
                sql += " AND ExecutionTime <= '{1}' ) ";
                sql = string.Format(sql, startTime, endTime);

                var cmd = dbr.GetSqlStringCommand(sql);
                using (var read = dbr.ExecuteReader(cmd))
                {
                    while (read.Read())
                    {
                        list.Add(RecoverModel(read));
                    }
                }
            }
            catch (Exception ex)
            {
                Msg = ex.Message;
            }
            return list;
        }

        public PushMessageInfo RecoverModel(IDataReader dataReader)
        {
            PushMessageInfo pushMessageInfo = new PushMessageInfo();
            pushMessageInfo.PushID = dataReader["PushID"].ToInt();
            pushMessageInfo.ChannelID = dataReader["ChannelID"].ToInt();
            pushMessageInfo.TypeID = dataReader["TypeID"].ToInt();
            pushMessageInfo.Section = dataReader["Section"].ToInt();
            pushMessageInfo.Contents = dataReader["Contents"].ToString();
            pushMessageInfo.Creator = dataReader["Creator"].ToString();
            pushMessageInfo.Updater = dataReader["Updater"].ToString();
            pushMessageInfo.Status = dataReader["Status"].ToInt();
            pushMessageInfo.PushType = dataReader["PushType"].ToString();
            pushMessageInfo.Template = dataReader["Template"].ToInt();
            pushMessageInfo.TargetList = dataReader["TargetList"].ToString();
            pushMessageInfo.DataID = dataReader["DataID"].ToString();
            pushMessageInfo.Url = dataReader["Url"].ToString();
            pushMessageInfo.PushCount = dataReader["PushCount"].ToInt();
            pushMessageInfo.CreateTime = dataReader["CreateTime"].ToDateTime();
            pushMessageInfo.UpdateTime = dataReader["UpdateTime"].ToDateTime();
            return pushMessageInfo;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="pushMessageInfo">pushId,pushCount,Status</param>
        /// <returns></returns>
        public bool UpdateMobilePush(PushMessageInfo pushMessageInfo)
        {
            string parmsKey = string.Empty;
            string strPlaceholder = string.Empty;

            var sql = " UPDATE  MobilePush SET ";
            sql += " Status=@Status,PushCount=@PushCount ";
            sql += " WHERE  PushID=@PushID";
            var cmd = dbw.GetSqlStringCommand(sql);
            dbw.AddInParameter(cmd, "Status", DbType.Int16, pushMessageInfo.Status);
            dbw.AddInParameter(cmd, "PushCount", DbType.Int32, pushMessageInfo.PushCount);
            dbw.AddInParameter(cmd, "PushID", DbType.Int32, pushMessageInfo.PushID);
            try
            {
                var result = dbw.ExecuteNonQuery(cmd);
                return result > 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///  验证 推送表内是否有相同的数据
        /// </summary>
        /// <param name="uid">用户ID  别名</param>
        /// <param name="typeId">一级目录</param>
        /// <param name="section">二级目录</param>
        /// <param name="dataID">参数</param>
        /// <returns>true 存在  false 不存在</returns>
        public bool MobilePush_IsExist(int uid,int typeId,int section,string dataID) 
        {
            var sql = " SELECT * FROM  MobilePush ";
            sql += " WHERE  TargetList ={0} AND TypeID={1} AND Section={2} AND DataID='{3}' ";
            sql = string.Format(sql, uid, typeId,section,dataID);
            var cmd = dbr.GetSqlStringCommand(sql);
            var read = dbr.ExecuteReader(cmd);
            if (read != null && read.Read() == true)
            {
                return true;
            }
            else 
            {
                return false;
            }
           
        }

        /// <summary>
        /// 用于清空 90天前的推送数据
        /// </summary>
        /// <param name="numberHour">小时计算单位</param>
        /// <returns></returns>
        public bool MobilePush_CleanExpired(int numberHour) 
        {
            string endTime = DateTime.Now.AddHours(-numberHour).ToString("yyyy-MM-dd");
            var sql = " DELETE FROM MobilePush ";
            sql += " WHERE  CreateTime < '{0}' ";
            sql = string.Format(sql, endTime);
            try
            {
                var cmd = dbr.GetSqlStringCommand(sql);
                var result = dbw.ExecuteNonQuery(cmd);
                return result > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}