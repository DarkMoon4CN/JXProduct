using JXAPI.Common;
using JXAPI.Component.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.Component.BLL
{
    public  class SpiderBLL
    {
        private SpiderBLL() { }
        private static SpiderBLL _instance;
        private static readonly JXAPI.Component.SQLServerDAL.SpiderDAL dal = new JXAPI.Component.SQLServerDAL.SpiderDAL();

        public static SpiderBLL Instance
        {
            get
            {
                return _instance ?? (_instance = new SpiderBLL());
            }
        }

        public OperationResult<bool> InsertSpider(IList<SpiderProductInfo> spInfoList)
        {
            return dal.InsertSpider(spInfoList);
        }

        public OperationResult<IList<int>> ProductID_GetList(int startCount, int endCount)
        {
            return dal.ProductID_GetList(startCount, endCount);
        }
    }
}
