﻿using JXProduct.Component.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXProduct.Component.BLL
{
    public class OperateLogBLL
    {
        private OperateLogBLL() { }
        private static OperateLogBLL _instance;
        private static readonly SQLServerDAL.OperateLogDAL dal = new SQLServerDAL.OperateLogDAL();
        public static OperateLogBLL Instance
        {
            get
            {
                return _instance ?? (_instance = new OperateLogBLL());
            }
        }

        public OperateLogInfo Save(int uid, string username, string title, string content)
        {
            return dal.OperateLog_Insert(new OperateLogInfo()
            {
                UID = uid,
                UserType = 0,
                UserName = username,
                Title = title,
                Content = content
            });
        }
    }
}