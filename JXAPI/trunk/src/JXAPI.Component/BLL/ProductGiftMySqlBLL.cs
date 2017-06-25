using JXAPI.Component.IBLL;
using JXAPI.Component.Model;
using JXAPI.Component.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JXAPI.Component.BLL
{
    public class ProductGiftMySqlBLL : IProductPlanMySqlBLL
    {
        #region 变量

        private static ProductGiftMySqlBLL _instance;
        private static readonly ProductGiftMySqlDAL dal = new ProductGiftMySqlDAL();
        private static readonly object _object = new object();

        #endregion

        private ProductGiftMySqlBLL() { }

        public static ProductGiftMySqlBLL Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock(_object)
                    {
                        if(_instance == null)
                        {
                            _instance = new ProductGiftMySqlBLL();
                        }
                    }
                }
                return _instance;
            }
        }

        #region CURD

        public int GetMaxID()
        {
            return dal.GetMaxProductGiftID();
        }

        public bool Add(DataTable productTable, out int errorCount)
        {
            return dal.AddProductGift(productTable, out errorCount);
        }

        public bool Update(DataTable productTable, out int errorCount)
        {
            return dal.UpdateProductGift(productTable, out errorCount);
        }

        #endregion
    }
}
