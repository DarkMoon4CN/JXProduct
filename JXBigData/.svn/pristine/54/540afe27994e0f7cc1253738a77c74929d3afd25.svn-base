using JXBigData.Component.Model;
using JXProduct.Component.DataAccess;
using JXUtil;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace JXBigData.Component.SqlServerDAL
{
    public class RegionDailyDAL
    {
        private static Database dbr = JXProductData.Writer;
        private static Database dbw = JXProductData.Reader;
        public OperationResult<IList<RegionDailyInfo>> RegionDaily_GetProvince(DateTime startTime, DateTime endTime)
        {
            IList<RegionDailyInfo> rdInfoList = new List<RegionDailyInfo>();
            try
            {
                string sqlCommand = "JXMarketing.dbo.RegionDaily_GetProvince";
                DbCommand cmd = dbw.GetStoredProcCommand(sqlCommand);
                dbw.AddInParameter(cmd, "StartTime", DbType.DateTime, startTime);
                dbw.AddInParameter(cmd, "EndTime", DbType.DateTime, endTime);
                using (var read = dbr.ExecuteReader(cmd))
                {
                    while (read.Read())
                    {
                        RegionDailyInfo rdInfo = new RegionDailyInfo();
                        rdInfo.Province = read["Province"].ToString();
                        rdInfo.TotalQuan = read["TotalQuan"].ToInt();
                        rdInfoList.Add(rdInfo);
                    }
                }
                return new OperationResult<IList<RegionDailyInfo>>(OperationResultType.Success, null, rdInfoList);
            }
            catch (Exception ex)
            {
                return new OperationResult<IList<RegionDailyInfo>>(OperationResultType.Error, ex.Message);
            }
        }

        public OperationResult<IList<RegionDailyInfo>> RegionDaily_GetCity(string province,DateTime startTime, DateTime endTime)
        {
            IList<RegionDailyInfo> rdInfoList = new List<RegionDailyInfo>();
            try
            {
                string sqlCommand = "JXMarketing.dbo.RegionDaily_GetCity";
                DbCommand cmd = dbw.GetStoredProcCommand(sqlCommand);
                dbw.AddInParameter(cmd, "Province", DbType.String, province);
                dbw.AddInParameter(cmd, "StartTime", DbType.DateTime, startTime);
                dbw.AddInParameter(cmd, "EndTime", DbType.DateTime, endTime);
                using (var read = dbr.ExecuteReader(cmd))
                {
                    while (read.Read())
                    {
                        RegionDailyInfo rdInfo = new RegionDailyInfo();
                        rdInfo.City = read["City"].ToString();
                        rdInfo.TotalQuan = read["TotalQuan"].ToInt();
                        rdInfoList.Add(rdInfo);
                    }
                }
                return new OperationResult<IList<RegionDailyInfo>>(OperationResultType.Success, null, rdInfoList);
            }
            catch (Exception ex)
            {
                return new OperationResult<IList<RegionDailyInfo>>(OperationResultType.Error, ex.Message);
            }
        }


    }
}
