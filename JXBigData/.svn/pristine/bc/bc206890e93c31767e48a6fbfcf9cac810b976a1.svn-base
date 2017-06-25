using JXBigData.Component.Model;
using JXBigData.Component.SqlServerDAL;
using JXUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXBigData.Component.BLL
{
    public class RegionDailyBLL
    {
        private RegionDailyBLL() { }
        private static RegionDailyBLL _instance;
        public static RegionDailyBLL Instance { get { return _instance ?? (_instance = new RegionDailyBLL()); } }
        private static readonly RegionDailyDAL dal = new RegionDailyDAL();

        public OperationResult<IList<RegionDailyInfo>> RegionDaily_GetProvince(DateTime startTime, DateTime endTime)
        {
            return dal.RegionDaily_GetProvince(startTime, endTime);
        }

        public OperationResult<IList<RegionDailyInfo>> RegionDaily_GetCity(string province, DateTime startTime, DateTime endTime)
        {
            return dal.RegionDaily_GetCity(province, startTime, endTime);
        }
    }
}
