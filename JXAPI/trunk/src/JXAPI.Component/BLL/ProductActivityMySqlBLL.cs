﻿using JXAPI.Component.IBLL;
using JXAPI.Component.Model;
using JXAPI.Component.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JXAPI.Component.BLL
{
    public class ProductActivityMySqlBLL : IProductPlanMySqlBLL
    {
        #region 变量

        private static ProductActivityMySqlBLL _instance;
        private static readonly ProductActivityMySqlDAL dal = new ProductActivityMySqlDAL();
        private static readonly object _object = new object();

        #endregion

        private ProductActivityMySqlBLL() { }

        public static ProductActivityMySqlBLL Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock(_object)
                    {
                        if(_instance == null)
                        {
                            _instance = new ProductActivityMySqlBLL();
                        }
                    }
                }
                return _instance;
            }
        }

        #region CURD

        public int GetMaxID()
        {
            return dal.GetMaxProductActivityID();
        }

        public bool Add(DataTable productTable, out int errorCount)
        {
            return dal.AddProductActivity(productTable, out errorCount);
        }

        public bool Update(DataTable productTable, out int errorCount)
        {
            return dal.UpdateProductActivity(productTable, out errorCount);
        }

        #endregion
    }
}