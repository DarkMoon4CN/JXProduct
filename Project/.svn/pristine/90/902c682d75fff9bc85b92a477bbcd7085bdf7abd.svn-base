using JXProduct.Component.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXProduct.Component.BLL
{
    public class BelowLinePromotionBLL
    {
        private static readonly SQLServerDAL.BelowLinePromotionDAL dal = new SQLServerDAL.BelowLinePromotionDAL();
        private BelowLinePromotionBLL() { }
        private static BelowLinePromotionBLL _instance;
        public static BelowLinePromotionBLL Instance { get { return _instance ?? (_instance = new BelowLinePromotionBLL()); } }

        public bool BelowLinePromotion_Insert(BelowLinePromotionInfo belowLinePromotionInfo)
        {
            return dal.BelowLinePromotion_Insert(belowLinePromotionInfo);
        }

        public int BelowLinePromotion_GetCount(string strWhere) 
        {
            return dal.BelowLinePromotion_GetCount(strWhere);
        }
        public IList<BelowLinePromotionInfo> BelowLinePromotion_GetList(int startCount, int size, string strWhere)
        {
            return dal.BelowLinePromotion_GetList(startCount, size, strWhere);
        }
        public bool BelowLinePromotion_IsExist(string invitationCode) 
        {
            return dal.BelowLinePromotion_IsExist(invitationCode);
        }
    }
}
