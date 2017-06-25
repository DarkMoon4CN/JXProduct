﻿using JXAPI.Component.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.Component.BLL
{
    public class MobilePushBLL
    {
        private MobilePushBLL() { }
        private static MobilePushBLL _instance;
        private static readonly JXAPI.Component.SQLServerDAL.MobilePushDAL dal = new JXAPI.Component.SQLServerDAL.MobilePushDAL();

        public static MobilePushBLL Instance
        {
            get
            {
                return _instance ?? (_instance = new MobilePushBLL());
            }
        }
        #region CURD
        public bool InsertMobilePush(PushMessageInfo pushMessageInfo)
        {
            return dal.InsertMobilePush(pushMessageInfo);
        }

        public List<PushMessageInfo> MobilePush_GetList(ref string Msg, int minute = 30)
        {
            return dal.MobilePush_GetList(minute, ref Msg);
        }

        public bool UpdateMobilePush(PushMessageInfo pushMessageInfo)
        {
            return dal.UpdateMobilePush(pushMessageInfo);
        }

        public bool MobilePush_IsExist(int uid,int typeId,int section,string dataID) 
        {
                return dal.MobilePush_IsExist(uid, typeId, section, dataID);
        }

        public bool MobilePush_CleanExpired(int numberHour)
        {
            return dal.MobilePush_CleanExpired(numberHour);
        }
        
        #endregion

    }
}
