using System;
using System.Collections.Generic;
using JXProduct.Component.Model;
using JXProduct.Component.SQLServerDAL;
using JXProduct.Component.Enums;
using System.Text;
namespace JXProduct.Component.BLL
{
    /// <summary>
    /// ProductActivity 
    /// </summary>
    public class ProductActivityBLL
    {
        private ProductActivityBLL() { }
        private static ProductActivityBLL _instance;
        public static ProductActivityBLL Instance
        {
            get
            {
                return _instance ?? (_instance = new ProductActivityBLL());
            }
        }

        private static readonly ProductActivityDAL dal = new ProductActivityDAL();

        #region CURD


        //更行单品活动
        public bool ProductActivity_Update(ProductActivityInfo model)
        {
            if (model.Discount > 1 && model.Type == (int)Enums.ProductActivity.满折 || model.Type == (int)Enums.ProductActivity.折扣)
            {
                model.Discount = model.Discount / 10;
            }
            return dal.ProductActivity_Update(model);
        }


        //获取单品活动
        public ProductActivityInfo ProductActivity_Get(Int32 productid)
        {
            var act = dal.ProductActivity_Get(productid);
            if (act == null)
            {
                act = new ProductActivityInfo();
                act.ProductID = productid;
            }
            return act;
        }

        /// <summary>
        /// 获取多个商品活动
        /// </summary>
        /// <param name="productIDs"></param>
        public List<ProductActivityInfo> ProductActivity_GetList(List<int> list)
        {
            if (list != null && list.Count > 0)
                return dal.ProductActivity_GetList(string.Join(",", list));
            return new List<ProductActivityInfo>();
        }

        #endregion


        #region 验证ActivityInfo 数据完整


        /// <summary>
        /// 验证数据是否符合活动标准
        /// </summary>
        /// <param name="act"></param>
        /// <returns></returns>
        public bool ValidateActivity(ProductActivityInfo act)
        {
            var result = false;

            //类型为0 没有活动
            if (act.Type == 0) return true;
            if (string.IsNullOrEmpty(act.ActDesc)) return false;

            //验证时间
            if (act.EndDate > DateTime.Now && act.EndDate > act.StartDate)
            {
                switch (act.Type)
                {
                    case 1: //包邮
                        result = act.ActPrice > 0 || act.ActQuantity > 0;
                        break;
                    case 2: //满赠
                        result = (act.ActPrice > 0 || act.ActQuantity > 0) && act.ProductGiftID > 0;
                        break;
                    case 3: //满减
                        result = (act.ActPrice > 0 || act.ActQuantity > 0) && act.Discount >= 0;
                        break;
                    case 4: //满返(满额返)
                        result = (act.ActPrice > 0 || act.ActQuantity > 0) && !string.IsNullOrEmpty(act.CouponBatchNo) && !string.IsNullOrEmpty(act.CouponName);
                        break;
                    case 5: //换购
                        result = (act.ActPrice > 0 || act.ActQuantity > 0) && act.Discount >= 0 && act.ProductGiftID > 0;
                        break;
                    case 6: //满折(满额打折)
                        result = (act.ActPrice > 0 || act.ActQuantity > 0) && act.Discount >= 0;
                        break;
                    case 7: //8.直降
                    case 8: //7.折扣                    
                        result = act.Discount > 0; break;
                    default: break;
                }
            }
            return result;
        }

        public Dictionary<string, string> GetActivity
        {
            get
            {
                return new Dictionary<string, string>{
                    {"0","无"},
                    //{"1_1","包邮(满额包邮)"},
                    //{"1_2","包邮(满件包邮)"},
                    {"2_1","满赠(满额赠)"},
                    {"2_2","满赠(满件赠)"},
                    {"3_1","满减(满额减)"},
                    {"3_2","满减(满件减)"},
                    {"4_1","满返(满额返)"},
                    {"4_2","满返(满件返)"},
                    {"5_1","换购(满额换购)"},
                    {"5_2","换购(满件换购)"},
                    {"6_1","满折(满额打折)"},
                    {"6_2","满折(满件打折)"},
                    {"7","直降"},
                    {"8","折扣"}                    
                };
            }
        }

        public string ProductActivity_Get(ProductActivityInfo model)
        {

            var sb = new StringBuilder();

            if (model != null && model.Type > 0)
            {
                if (model.ActivityType > 0)
                {
                    sb.AppendFormat("[{0}] {1}<br/>", JXUtil.EnumHelper.GetText<ProductActivity>(model.ActivityType), model.ActivityName);
                }
                sb.AppendFormat("[{0}] {1}", JXUtil.EnumHelper.GetText<ProductActivity>(model.Type), model.ActDesc);
            }

            return sb.ToString();
        }

        #endregion
    }
}
