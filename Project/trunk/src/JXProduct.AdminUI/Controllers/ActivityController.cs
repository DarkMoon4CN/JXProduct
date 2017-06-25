using JXProduct.AdminUI.Models.Activity;
using JXProduct.Component.BLL;
using JXProduct.Component.Model;
using JXUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;

namespace JXProduct.AdminUI.Controllers
{
    public class ActivityController : JXAdminAuthBase.BaseController
    {
        #region 活动
        public ActionResult List(Models.Activity.SearchModel model)
        {
            int recordCount = 0;
            var list = ActivityBLL.Instance.Activity_GetList(model.pageindex, model.pagesize, "1", model.ToWhere(), out recordCount);
            var pagedlist = new PagedList<ActivityInfo>(list, model.pageindex, model.pagesize, recordCount);
            ViewBag.PagedList = pagedlist;
            if (Request.IsAjaxRequest() || Request["t"] == "1")
            {
                return PartialView("_PartialList");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int? actid)
        {
            var model = new ActivityModel();
            if (actid.HasValue)
            {
                var info = ActivityBLL.Instance.Activity_Get(actid.Value);
                if (info != null)
                {
                    model.ActID = info.ActID;
                    model.Name = info.Name;
                    model.BriefName = info.BriefName;
                    model.URL = info.URL;
                    model.Description = info.Description;
                    model.Type = info.Type;
                    model.Limit = info.Limit;
                    model.StartTime = info.StartTime;
                    model.EndTime = info.EndTime;
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ActivityModel model)
        {
            var result = new JsonResultObject();
            ActivityInfo info = null;
            if (model.ActID > 0)
            {
                info = ActivityBLL.Instance.Activity_Get(model.ActID);
            }
            if (info == null)
            {
                info = new ActivityInfo();
                info.ActID = 0;
            }
            if (ModelState.IsValid)
            {
                if (model.Type >= 1 && model.Type <= 6 && model.Limit == 0)
                {
                    result.msg = "当前活动类型必须选择限制";
                }
                else
                {
                    info.Name = model.Name;
                    info.BriefName = model.BriefName;
                    info.URL = model.URL;
                    info.Description = model.Description;
                    info.Type = model.Type;
                    info.Limit = model.Limit;
                    info.StartTime = model.StartTime;
                    info.EndTime = DateTime.Parse(model.EndTime.ToString("yyyy-MM-dd 23:59:59"));
                    info.Updater = base.UNICKNAME;

                    if (info.ActID > 0)
                    {
                        result.status = ActivityBLL.Instance.Activity_Update(info);
                    }
                    else
                    {
                        //添加全站活动时默认下线
                        info.Status = 1;
                        info.Creator = base.UNICKNAME;
                        info.ActID = ActivityBLL.Instance.Activity_Insert(info);
                        result.status = info.ActID > 0;
                    }
                    result.msg = info.ActID.ToString();
                }
            }
            else
            {
                var error = ModelState.Values.FirstOrDefault(t => t.Errors.Count > 0);
                result.msg = error.Errors[0].ErrorMessage;
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult SetStatus(int actID, int status)
        {
            var result = new JsonResultObject();
            //活动默认是0
            //如果改为上线,那么需要判断当前的规则是否存在
            if (status == 1)
            {
                var rules = ActivityRuleBLL.Instance.ActivityRule_GetList(actID);
                if (rules.Count == 0)
                {
                    result.msg = "需要添加规则才可以上线";
                    return Json(result);
                }
            }
            result.status = ActivityBLL.Instance.Activity_SetStatus(actID, status);
            return Json(result);
        }

        #endregion

        #region 规则

        [HttpGet]
        public ActionResult Rule(int actid)
        {
            var model = new RuleModel();
            model.ActID = actid;
            if (actid > 0)
            {
                var activity = ActivityBLL.Instance.Activity_Get(actid);
                var rules = ActivityRuleBLL.Instance.ActivityRule_GetList(actid);
                ViewBag.Activity = activity;
                ViewBag.Rules = rules;
                model.ActType = activity.Type;
            }

            return View(model);
        }
        [HttpPost]
        public ActionResult Rule(RuleModel model)
        {
            var result = new JsonResultObject();
            if (ModelState.IsValid)
            {
                ActivityRuleInfo rule = null;
                //目前没有修改。先删除再添加
                //if (model.RuleID > 0)
                //{
                //    rule = ActivityRuleBLL.Instance.ActivityRule_Get(model.RuleID.Value);
                //}
                if (rule == null)
                {
                    rule = new ActivityRuleInfo();
                    rule.ActID = model.ActID;
                }
                rule.ActType = model.ActType;
                rule.Amount = model.Amount ?? 0;
                rule.Quantity = model.Quantity ?? 0;
                rule.ProductID = model.ProductID;
                rule.DiscountAmount = model.DiscountAmount ?? 0;
                rule.Discount = model.Discount ?? 0;
                rule.CouponBatchNo = model.CouponBatchNo;

                if (rule.ActType == (int)JXProduct.Component.Enums.ProductActivity.满折 || rule.ActType == (int)JXProduct.Component.Enums.ProductActivity.折扣)
                {
                    rule.Discount = rule.Discount / 10;
                }
                if (rule.RuleID > 0)
                {
                    rule.Updater = base.UNICKNAME;
                    result.status = ActivityRuleBLL.Instance.ActivityRule_Update(rule);
                }
                else
                {
                    rule.Creator = base.UNICKNAME;
                    rule.RuleID = ActivityRuleBLL.Instance.ActivityRule_Insert(rule);
                    result.status = rule.RuleID > 0;
                }
            }
            else
            {
                result.msg = ModelState.Values.First(t => t.Errors.Count > 0).Errors[0].ErrorMessage;
            }
            return Json(result);
        }

        [HttpGet]
        public ActionResult RuleList(int actid)
        {
            var activity = ActivityBLL.Instance.Activity_Get(actid);
            var rules = ActivityRuleBLL.Instance.ActivityRule_GetList(actid);
            var str = new StringBuilder();
            int index = 1;
            foreach (var rule in rules)
            {
                str.Append("<tr>");
                str.Append("<td>" + (index++) + "</td>");
                str.Append("<td>" + JXProduct.Component.BLL.ActivityRuleBLL.Instance.ActivityRule_Description(activity, rule) + "</td>");
                str.Append("<td ruleid='" + rule.RuleID + "'>");
                //str.Append("<a href='javascript:void(0)' name='update'>编辑</a>");
                str.Append("<a href='javascript:void(0)' name='del'>删除</a>");
                str.Append("</td>");
                str.Append("</tr>");
            }
            return Content(str.ToString());
        }

        [HttpGet]
        public PartialViewResult RuleEdit(int actid, int ruleid)
        {
            return null;
            //var model = new RuleModel();
            //var activity = ActivityBLL.Instance.Activity_Get(actid);
            //var activityRule = ActivityRuleBLL.Instance.ActivityRule_Get(ruleid);

            //ViewBag.Activity = activity;

            //model.ActID = activityRule.ActID;
            //model.RuleID = activityRule.RuleID;
            //model.Amount = activityRule.Amount;
            //model.Quantity = activityRule.Quantity;
            //model.Discount = activityRule.Discount;
            //model.DiscountAmount = activityRule.DiscountAmount;
            //model.CouponBatchNo = activityRule.CouponBatchNo;
            //model.ProductID = activityRule.ProductID;

            //return PartialView("_PartialRuleEdit", model);
        }

        [HttpPost]
        public JsonResult RuleDel(int ruleid)
        {
            var result = new JsonResultObject();
            result.status = ActivityRuleBLL.Instance.ActivityRule_Delete(ruleid);
            return Json(result);
        }

        #endregion

        #region 活动绑定商品

        //显示
        public ActionResult Product(int actid)
        {
            var page = new RequestPagedBase();
            int recordCount = 0;
            var productlist = ProductBLL.Instance.Product_GetList(page.pageindex, page.pagesize, "0", "EXISTS(SELECT 1 FROM ProductActivity pa where pa.ProductID = p.ProductID and pa.ActID =" + actid + ")", out recordCount);
            var paged = new PagedList<ProductInfo>(productlist, page.pageindex, page.pagesize, recordCount);
            ViewBag.ProductList = paged;


            var searchlist = ProductBLL.Instance.Product_GetList(page.pageindex, page.pagesize, "0", "NOT EXISTS(SELECT * FROM ProductActivity pa where pa.ProductID = p.ProductID and pa.ActID =" + actid + ")", out recordCount);
            var searchpaged = new PagedList<ProductInfo>(searchlist, page.pageindex, page.pagesize, recordCount);
            ViewBag.SearchProductList = searchpaged;
            ViewBag.ActID = actid;
            return View();
        }
        public PartialViewResult SearchProduct(int? actid, SearchProductModel model)
        {
            var sindex = 0;
            int.TryParse(Request["sindex"], out sindex);
            model.pageindex = sindex > 0 ? sindex : 1;
            var str = new StringBuilder(" 1=1 ");
            str.AppendFormat(" AND NOT EXISTS(SELECT * FROM ProductActivity pa where pa.ProductID = p.ProductID and pa.ActID ={0})", actid);
            var cfid = 0;
            if (model.CF3 > 0)
                cfid = model.CF3;
            else if (model.CF2 > 0)
                cfid = model.CF2;
            else if (model.CF1 > 0)
                cfid = model.CF1;
            if (cfid > 0)
            {
                var cf = ClassificationBLL.Instance.Classification_Get(cfid);
                if (!string.IsNullOrWhiteSpace(cf.Path))
                    str.AppendFormat(" AND EXISTS (SELECT 1 FROM ProductClassification AS pc WHERE pc.ProductID =p.ProductID AND  pc.CFPath LIKE '{0}%')", cf.Path);
            }
            if (model.ProductID > 0)
            {
                str.AppendFormat(" AND p.ProductID={0} ", model.ProductID);
            }
            if (!string.IsNullOrWhiteSpace(model.ProductCode))
            {
                str.AppendFormat(" AND p.ProductCode='{0}'", model.ProductCode);
            }
            if (model.BrandID > 0)
            {
                str.AppendFormat(" AND p.BrandID={0} ", model.BrandID);
            }
            int recordCount = 0;
            var productlist = ProductBLL.Instance.Product_GetList(model.pageindex, model.pagesize, "0", str.ToString(), out recordCount);
            var paged = new PagedList<ProductInfo>(productlist, model.pageindex, model.pagesize, recordCount);
            ViewBag.SearchProductList = paged;
            ViewBag.ActID = actid;
            return PartialView("_PartialSearchProduct");
        }
        public PartialViewResult ActProduct(int actid, RequestPagedBase page)
        {
            int pindex = 1;
            int.TryParse(Request["pindex"], out pindex);
            page.pageindex = pindex > 0 ? pindex : 1;
            int recordCount = 0;
            var productList = ProductBLL.Instance.Product_GetList(page.pageindex, page.pagesize, "0", "  EXISTS (SELECT * FROM ProductActivity AS pa WHERE pa.ActID =" + actid + " AND pa.ProductID =p.ProductID)", out recordCount);
            var pagelist = new PagedList<ProductInfo>(productList, page.pageindex, page.pagesize, recordCount);
            ViewBag.ProductList = pagelist;
            ViewBag.ActID = actid;
            return PartialView("_PartialProduct");
        }

        //添加
        [HttpPost]
        public JsonResult AddProduct(SearchProductModel model)
        {
            var result = new JsonResultObject();
            var count = 0;
            if (model.ValidateProductIDs)
            {
                count = ActivityBLL.Instance.Activity_AddProduct(model.ActID, model.ProductIDs);
            }
            else
            {
                if (model.ValidateBatUpdate)
                {
                    count = ActivityBLL.Instance.Activity_AddProduct(model.ActID, model.GetCFPath, model.BrandID, model.ProductID, model.ProductCode);
                }
                else
                {
                    result.msg = "批量添加的时候,最少要一个条件";
                }
            }
            if (count > 0)
            {
                result.status = true;
                result.msg = "共操作" + count.ToString() + "条记录";
            }
            return Json(result);
        }

        //删除
        public JsonResult DelProduct(int actid, string productIDs)
        {
            var result = new JsonResultObject();
            result.status = ActivityBLL.Instance.Activity_DelProduct(actid, productIDs);
            return Json(result);
        }

        #endregion
    }
}