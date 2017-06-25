﻿using JXProduct.Component.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXProduct.Component.BLL
{
    public class ActivityBLL
    {
        private static readonly SQLServerDAL.ActivityDAL dal = new SQLServerDAL.ActivityDAL();
        private ActivityBLL() { }
        private static ActivityBLL _instance;
        public static ActivityBLL Instance { get { return _instance ?? (_instance = new ActivityBLL()); } }

        #region CURD

        public int Activity_Insert(ActivityInfo activityInfo)
        {
            return dal.Activity_Insert(activityInfo);
        }

        public bool Activity_Update(ActivityInfo activityInfo)
        {
            return dal.Activity_Update(activityInfo);
        }

        public ActivityInfo Activity_Get(int actID)
        {
            return dal.Activity_Get(actID);
        }

        public bool Activity_SetStatus(int actID, int status)
        {
            return dal.Activity_SetStatus(actID, status);
        }

        public IList<ActivityInfo> Activity_GetList(int pageIndex, int pageSize, string orderType, string strWhere, out int recordCount)
        {
            return dal.Activity_GetList(pageIndex, pageSize, orderType, strWhere, out recordCount);
        }

        #endregion

        #region 活动关联商品

        public int Activity_AddProduct(int actid, string productIDs)
        {
            return dal.Activity_AddProduct(actid, productIDs);
        }
        public int Activity_AddProduct(int actid, string cfPath, int brandID, int ProductID, string ProductCode)
        {
            return dal.Activity_AddProduct(actid, cfPath, brandID, ProductID, ProductCode);
        }
        public bool Activity_DelProduct(int actid, string productIDs)
        {
            return dal.Activity_DelProduct(actid, productIDs);
        }

        #endregion
    }
}