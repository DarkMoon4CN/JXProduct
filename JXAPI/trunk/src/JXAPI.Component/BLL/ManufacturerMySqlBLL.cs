using JXAPI.Component.IBLL;
using JXAPI.Component.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JXAPI.Component.BLL
{
    public class ManufacturerMySqlBLL : IProductPlanMySqlBLL
    {
        private ManufacturerMySqlBLL() { }
        private static ManufacturerMySqlBLL _instance;
        private static readonly ManufacturerMySqlDAL dal = new ManufacturerMySqlDAL();
        public static ManufacturerMySqlBLL Instance
        {
            get
            {
                return _instance ?? (_instance = new ManufacturerMySqlBLL());
            }
        }

        #region CURD
        
        public int GetMaxID()
        {
            return dal.GetMaxManufacturerID();
        }

        public bool Add(DataTable productTable, out int errorCount)
        {
            return dal.AddManufacturer(productTable,out errorCount);
        }

        public bool Update(DataTable productTable, out int errorCount)
        {
            return dal.UpdateManufacturer(productTable, out errorCount);
        }

        #endregion
    }
}
