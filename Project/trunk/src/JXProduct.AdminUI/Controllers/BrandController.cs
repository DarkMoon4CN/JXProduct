using JXProduct.AdminUI.Image;
using JXProduct.AdminUI.Models.Brand;
using JXProduct.Component.BLL;
using JXProduct.Component.Enums;
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
    public class BrandController : JXAdminAuthBase.BaseController
    {
        public ActionResult List(int cfid = -1, int pageIndex = 1, string orderType = "SellCount")
        {
            string brandIDs = string.Empty;
            IList<BrandInfo> brandInfoList = null;//品牌List对象
            ViewBag.ClassificationInfoList = null;//所有1级分类
            ViewBag.BrandInfo = null;//所有品牌
            ViewBag.SelectedBrands = null;//已选中的品牌
            ViewBag.SelectedBrandsCount = 0;
            ViewBag.SelectedClassId = -1;//默认选中的分类id
            int count = 0;
            //获取1级分类信息 
            IList<ClassificationInfo> classificationInfoList =
                                 JXProduct.Component.BLL.ClassificationBLL.Instance.Classification_GetList(0);
            #region 已选中的品牌
            if (cfid == -1 || cfid == -2)
            {
                //是热销品牌或者推荐品牌
                brandInfoList = JXProduct.Component.BLL.BrandBLL.Instance.Brand_GetList(pageIndex - 1, 36, orderType, "", out count);
                ViewBag.PageList = new PagedList<BrandInfo>(brandInfoList, pageIndex, 36, count);
                ViewBag.BrandInfo = brandInfoList;
            }
            else
            {
                //非热销和推荐的品牌
                brandInfoList = JXProduct.Component.BLL.BrandBLL.Instance.Brand_GetByCFID(cfid, pageIndex, 36, orderType, out count);
                ViewBag.PageList = new PagedList<BrandInfo>(brandInfoList, pageIndex, 36, count);
                ViewBag.BrandInfo = brandInfoList;
            }
            #endregion

            #region 等待选中的品牌
            IList<MarketBrandInfo> marketBrandInfo =
                                JXProduct.Component.BLL.MarketBrandBLL.Instance.Brand_GetByCFID(cfid);
            for (int i = 0; i < marketBrandInfo.Count; i++)
            {
                brandIDs += marketBrandInfo[i].BrandID + ",";
            }

            if (!string.IsNullOrEmpty(brandIDs))
            {
                brandIDs = brandIDs.Substring(0, brandIDs.Length - 1);
                brandInfoList = JXProduct.Component.BLL.BrandBLL.Instance.Brand_Get(brandIDs);
                ViewBag.SelectedBrandsCount = brandIDs.Split(',').Length;
                ViewBag.SelectedBrands = brandInfoList;
            }
            #endregion

            ViewBag.OrderType = orderType;
            ViewBag.ClassificationInfoList = classificationInfoList;
            ViewBag.SelectedClassId = cfid;
            return View();
        }

        public ActionResult brandList(BrandSearchModel model)
        {
            var recordCount = 0;
            var list = BrandBLL.Instance.Brand_GetList(model.pageindex, model.pagesize, model.OrderBy, model.ToWhere, out recordCount);

            var pagelist = new PagedList<BrandInfo>(list, model.pageindex, model.pagesize, recordCount);
            ViewBag.PagedList = pagelist;
            if (Request.IsAjaxRequest())
            {
                return PartialView("_PartialList");
            }
            return View("brandlist", model);
        }
        public ActionResult Edit(int brandid = 0)
        {
            var model = new BrandEditModel();
            if (brandid > 0)
            {
                var brand = BrandBLL.Instance.Brand_Get(brandid);
                if (brand != null)
                {
                    model.BrandID = brand.BrandID;
                    model.BrandType = brand.BrandType;
                    model.ChineseName = brand.ChineseName;
                    model.Description = brand.Description;
                    model.EnglishName = brand.EnglishName;
                    model.ImageBanner = brand.ImageBanner;
                    model.ImageLogo = brand.ImageLogo;
                    model.Keywords = brand.Keywords;
                    model.MetaDescription = brand.MetaDescription;
                    model.RegTrademark = brand.RegTrademark;
                    model.Title = brand.Title;
                    model.UnregTrademark = brand.UnregTrademark;
                }
            }
            return View(model);
        }
        public ActionResult EditModel(BrandEditModel model)
        {
            model.Error = string.Empty;
            BrandInfo brand = null;
            if (model.BrandID > 0)
            {
                brand = BrandBLL.Instance.Brand_Get(model.BrandID);
            }
            if (brand == null)
            {
                brand = new BrandInfo();
            }
            brand.ChineseName = model.ChineseName;
            if (string.IsNullOrWhiteSpace(brand.ChineseName))
            {
                model.Error = "请输入品牌名称!";
                return View("Edit", model);
            }
            if (model.BrandType < 0)
            {
                model.Error = "请选择品牌类别!";
                return View("Edit", model);
            }
            //判断当前名称是否存在
            if (brand.ChineseName != model.ChineseName)
            {
                var recordCount = 0;
                var list = BrandBLL.Instance.Brand_GetList(1, 1, "", string.Format("ChineseName='{0}'", brand.ChineseName), out recordCount);
                if (list.Count > 0)
                {
                    model.Error = "该品牌名称已存在!";
                    return View("Edit", model);
                }
            }
            brand.BrandType = model.BrandType;
            brand.Description = model.Description;
            brand.PinyinName = JXUtil.PinyinUtil.ConvertToPinyin(model.ChineseName);
            brand.EnglishName = model.EnglishName;
            brand.Keywords = model.Keywords;
            brand.MetaDescription = model.MetaDescription;


            if (model.BrandID > 0)
            {
                BrandBLL.Instance.Brand_Update(brand);
            }
            else
            {
                brand.BrandID = BrandBLL.Instance.Brand_Insert(brand);
            }
            var img = Request.Files[0];
            if (img != null && img.ContentLength > 0)
            {
                var result = ProductImage.Instance.BrandUpload(brand.BrandID + "_" + brand.PinyinName, 1, img);
                var path = JXAPI.JXSdk.Utils.JsonHelper.ConvertToObj<List<JXAPI.JXSdk.Request.ImagePathRequest>>(result.data.ToString());
                brand.ImageLogo = path[0].ImagePath.TrimStart('/');
                model.ImageLogo = brand.ImageLogo;
                BrandBLL.Instance.Brand_Update(brand);
            }
            model.BrandID = brand.BrandID;
            //return View("Edit", model);
            return RedirectToAction("brandlist");

        }
        public JsonResult ModifyBrand(int cfId, int brandId)
        {
            var result = new JsonResultObject();
            IList<MarketBrandInfo> brandInfoList = JXProduct.Component.BLL.MarketBrandBLL.Instance.Brand_GetByCFID(cfId, brandId);
            bool state = false;//执行状态
            MarketBrandInfo brandInfo = new MarketBrandInfo();
            brandInfo.BrandID = brandId;
            brandInfo.CFID = cfId;

            if (brandInfoList == null || brandInfoList.Count == 0)
            {
                state = JXProduct.Component.BLL.MarketBrandBLL.Instance.Brand_Insert(brandInfo);
                result.msg = "添加品牌";
            }
            else
            {
                state = JXProduct.Component.BLL.MarketBrandBLL.Instance.Brand_Delete(brandInfo);
                result.msg = "取消添加品牌";
            }

            result.data = JXProduct.Component.BLL.MarketBrandBLL.Instance.Brand_GetByCFID(cfId).Count;
            result.status = state;
            return Json(result);
        }
    }
}