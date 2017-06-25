using JXProduct.Component.Enums;
using JXProduct.Component.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXProduct.Component.BLL
{
    public class ActivityRuleBLL
    {
        private ActivityRuleBLL() { }
        private static ActivityRuleBLL _instance;
        private static readonly SQLServerDAL.ActivityRuleDAL dal = new SQLServerDAL.ActivityRuleDAL();

        public static ActivityRuleBLL Instance { get { return _instance ?? (_instance = new ActivityRuleBLL()); } }




        #region CURD

        public int ActivityRule_Insert(ActivityRuleInfo activityRuleInfo)
        {
            return dal.ActivityRule_Insert(activityRuleInfo);
        }

        public bool ActivityRule_Update(ActivityRuleInfo activityRuleInfo)
        {
            return dal.ActivityRule_Update(activityRuleInfo);
        }

        public bool ActivityRule_Delete(int ruleID)
        {
            return dal.ActivityRule_Delete(ruleID);
        }

        //public ActivityRuleInfo ActivityRule_Get(int ruleID)
        //{
        //    return dal.ActivityRule_Get(ruleID);
        //}

        public IList<ActivityRuleInfo> ActivityRule_GetList(int actid)
        {
            return dal.ActivityRule_GetList(actid);
        }

        #endregion

        #region Get 规则说明

        /// <summary>
        /// 包邮 = 1,满赠 = 2,满减 = 3,满返 = 4,换购 = 5,满折 = 6,直降= 7,折扣  = 8
        /// </summary>
        public string ActivityRule_Description(ActivityInfo act, ActivityRuleInfo actrule)
        {
            var rule = new StringBuilder();
            switch ((ProductActivity)act.Type)
            {
                case ProductActivity.包邮:
                    {
                        if (act.Limit == 1)
                        {
                            rule.AppendFormat("满￥{0}包邮", actrule.Amount);
                        }
                        else if (act.Limit == 2)
                        {
                            rule.AppendFormat("满{0}件包邮", actrule.Quantity);
                        }
                    } break;
                case ProductActivity.满赠:
                    {
                        if (act.Limit == 1)
                        {
                            rule.AppendFormat("满￥{0}送<a href='http://www.jxdyf.com/product/{2}.html' target='_blank'>{1}</a>", actrule.Amount, actrule.ProductGiftName, actrule.ProductID);
                        }
                        else if (act.Limit == 2)
                        {
                            rule.AppendFormat("满{0}件送<a href='http://www.jxdyf.com/product/{2}.html' target='_blank'>{1}</a>", actrule.Quantity, actrule.ProductGiftName, actrule.ProductID);
                        }

                    } break;
                case ProductActivity.满减:
                    {
                        if (act.Limit == 1)
                        {
                            rule.AppendFormat("满￥{0}减￥{1}", actrule.Amount, actrule.DiscountAmount);
                        }
                        else if (act.Limit == 2)
                        {
                            rule.AppendFormat("满{0}件减￥{1}", actrule.Quantity, actrule.DiscountAmount);
                        }
                    } break;
                case ProductActivity.满返:
                    {
                        if (act.Limit == 1)
                        {
                            rule.AppendFormat("满￥{0}送优惠劵{1}", actrule.Amount, actrule.CouponBatchNo);
                        }
                        else if (act.Limit == 2)
                        {
                            rule.AppendFormat("满{0}件送优惠劵{1}", actrule.Quantity, actrule.CouponBatchNo);
                        }
                    } break;

                case ProductActivity.换购:
                    {
                        if (act.Limit == 1)
                        {
                            rule.AppendFormat("满￥{0}加{1}换购{2}", actrule.Amount, actrule.DiscountAmount, actrule.ProductGiftName);
                        }
                        else if (act.Limit == 2)
                        {
                            rule.AppendFormat("满{0}件加{1}换购{2}", actrule.Quantity, actrule.DiscountAmount, actrule.ProductGiftName);
                        }
                    } break;
                case ProductActivity.满折:
                    {
                        if (act.Limit == 1)
                        {
                            rule.AppendFormat("满￥{0}打{1}折", actrule.Amount, actrule.Discount.ToString("F2").TrimEnd('0').TrimEnd('.'));
                        }
                        else if (act.Limit == 2)
                        {
                            rule.AppendFormat("满{0}件打{1}折", actrule.Quantity, actrule.Discount.ToString("F0").TrimEnd('0').TrimEnd('.'));
                        }
                    } break;
                case ProductActivity.直降:
                    {
                        rule.AppendFormat("全场直降￥{0}", actrule.DiscountAmount);
                    } break;
                case ProductActivity.折扣:
                    {
                        rule.AppendFormat("全场{0}折优惠", actrule.Discount.ToString("F2").TrimEnd('0').TrimEnd('.'));
                    }
                    break;
                default: break;
            }

            return rule.ToString();
        }
        #endregion
    }
}