using AutoMapper;
using JXAdminAuthBase;
using JXAPI.JXSdk.Request;
using JXAPI.JXSdk.Response;
using JXAPI.JXSdk.Service;
using JXProduct.AdminUI.App_Start.AutoMapper;
using JXProduct.AdminUI.Image;
using JXProduct.AdminUI.Models.Product;
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
using System.Xml.Linq;
using Webdiyer.WebControls.Mvc;

namespace JXProduct.AdminUI.Controllers
{
    public class ProductController : BaseController
    {
        public static bool IsExecuteMapper = RegisterAutomapper.Execute();

        //列表页
        public ActionResult List(ProductSearchModel model)
        {
            int recordCount = 0;
            var where = model.ToSqlWhere();
            //strWhere = "ProductID =28959";
            var orderby = model.ToOrderBy();
            var productlist = ProductBLL.Instance.Product_GetList(model.pageindex, model.pagesize, orderby, where, out recordCount);

            //如有套餐商品 加载数据
            if (productlist.Any(t => t.Type == 2))
            {
                var comboIDs = productlist.Where(t => t.Type == 2).Select(t => t.ProductID).ToList();
                var comboProductList = ProductComboBLL.Instance.ProductCombo_GetByProductIDs(comboIDs);
                ViewBag.comboProductList = comboProductList;
            }
            List<ProductActivityInfo> activitylist = null;
            List<ProductGiftInfo> giftlist = null;
            List<ProductAuditInfo> auditlist = null;
            Dictionary<int, int> expirelist = null;
            if (productlist.Count > 0)
            {
                var productids = productlist.Select(t => t.ProductID).ToList();
                activitylist = ProductActivityBLL.Instance.ProductActivity_GetList(productids);
                giftlist = ProductGiftBLL.Instance.ProductGift_GetList(productids);
                auditlist = ProductBLL.Instance.ProductAudit_Get(productids.ToArray());
                expirelist = QualificationBLL.Instance.Qualification_GetExpire(productids);
            }
            else
            {
                activitylist = new List<ProductActivityInfo>();
                giftlist = new List<ProductGiftInfo>();
                auditlist = new List<ProductAuditInfo>();
                expirelist = new Dictionary<int, int>();
            }
            var pagedlist = new PagedList<ProductInfo>(productlist, model.pageindex, model.pagesize, recordCount);
            ViewBag.ActivityList = activitylist;
            ViewBag.PagedList = pagedlist;
            ViewBag.GiftList = giftlist;
            ViewBag.AuditList = auditlist;
            ViewBag.ExpireList = expirelist;
            if (Request.IsAjaxRequest())
            {
                return PartialView("_PartialList", ViewBag.PagedList);
            }
            return View();
        }

        [HttpPost]
        public JsonResult Get(int productid)
        {
            var result = new JsonResultObject();
            if (productid > 0)
            {
                var productinfo = ProductBLL.Instance.Product_Get(productid);
                if (null != productinfo)
                {
                    result.status = true;
                    result.data = productinfo;
                }
            }
            return Json(result);
        }

        public JsonResult CheckProductCode(string productcode)
        {
            var result = new JsonResultObject();
            result.status = ProductBLL.Instance.CheckProductCode(productcode);
            return Json(result);
        }

        #region 提示信息列表

        public ActionResult PendingInfo(PendingListModel p)
        {
            p.PendingType = (int)Product.Process.产品信息维护;
            if (Request.IsAjaxRequest())
                return PartialView("_PartialPendingList", p);
            return View("PendingList", p);
        }
        public ActionResult PendingQualification(PendingListModel p)
        {
            p.PendingType = (int)Product.Process.质管部信息维护;
            if (Request.IsAjaxRequest())
                return PartialView("_PartialPendingList", p);
            return View("PendingList", p);
        }
        public ActionResult PendingAudit(PendingListModel p)
        {
            p.PendingType = (int)Product.Process.质管部信息审核;
            if (Request.IsAjaxRequest())
                return PartialView("_PartialPendingList", p);
            return View("PendingList", p);
        }
        public ActionResult PendingEditPrice(PendingListModel p)
        {
            p.PendingType = (int)Product.Process.运营部价格维护;
            if (Request.IsAjaxRequest())
                return PartialView("_PartialPendingList", p);
            return View("PendingList", p);
        }

        #endregion

        #region 编辑商品

        public JsonResult GetStock(string productIDs)
        {
            var result = new JsonResultObject(true);
            result.data = ProductBLL.Instance.Product_GetComboStock(productIDs).Select(t => new { productid = t.Key, stock = t.Value });
            return Json(result);
        }

        #endregion

        #region 编辑 Edit

        //编辑商品
        [HttpGet]
        public ActionResult Edit(int productid)
        {
            var model = new ProductEditModel();
            model.Roles = base.ROLENAME;

            if (productid > 0)
            {
                model.Product = ProductBLL.Instance.Product_Get(productid);
            }
            if (model.Product == null)
            {
                return new EmptyResult();
            }
            if (model.Product.Type == 0)
            {
                int recordCount = 0;
                model.Brands = BrandBLL.Instance.Brand_GetList(1, 0, "pinyinname asc", "", out recordCount).
                    Select(t => new SelectListItem()
                        {
                            Value = t.BrandID.ToString(),
                            Text = t.ChineseName,
                            Selected = t.BrandID == model.Product.BrandID
                        });
                model.Audit = ProductBLL.Instance.ProductAudit_Get(model.Product.ProductID);

                model.Instructions = ProductBLL.Instance.Product_GetInstructions(productid);
                model.keywords = KeywordBLL.Instance.KeywordProduct_ByProductID(productid);
                model.relateds = ProductParameterPriceBLL.Instance.ProductPropValue_GetGroupList(productid);
                if (model.relateds.Count == 0)
                {
                    model.relateds.Add(
                        new ProductPropValueInfo()
                        {
                            ProductID = model.Product.ProductID,
                            ProductCode = model.Product.ProductCode,
                            ChineseName = model.Product.ChineseName,
                            TradeName = model.Product.TradeName,
                            CADN = model.Product.CADN,
                            BarCode = model.Product.BarCode,
                            Specifications = model.Product.Specifications,
                            Spec = model.Product.Specifications
                        }
                    );
                }
                var supplierid = ProductBLL.Instance.Product_GetSupplierID(productid);
                var suppliers = ProductBLL.Instance.Product_GetSupplier()
                    .Select(t => new SelectListItem()
                    {
                        Text = t.Value,
                        Value = t.Key.ToString(),
                        Selected = t.Key == supplierid
                    }).ToList();
                suppliers.Insert(0, new SelectListItem() { Text = "请选择", Value = "" });
                model.Suppliers = suppliers;
                return View(model);
            }
            else
            {
                model.Audit = ProductBLL.Instance.ProductAudit_Get(model.Product.ProductID);
                return View("EditImage", model);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Edit(ProductModel model, ProductInstructionsInfo ins)
        {
            var result = new JsonResultObject();
            var groupid = 0;

            ProductInfo info = null;
            if (model.ProductID > 0)
            {
                info = ProductBLL.Instance.Product_Get(model.ProductID);
            }
            if (info == null)
                return Json(result);
            var productlist = new List<ProductInfo>();
            //处理商品信息
            var requestIDs = Request["ProductIDs"];
            if (!string.IsNullOrWhiteSpace(requestIDs))
            {
                var ids = requestIDs.Split(',');
                var edit = new Dictionary<int, string>();
                foreach (var id in ids)
                {
                    var val = Request["editproduct" + id];
                    if (!string.IsNullOrEmpty(val))
                    {
                        edit.Add(int.Parse(id), val);
                    }
                }
                foreach (var pro in edit)
                {
                    //修改,不需要修改金象码 同步商品信息即可
                    var propvalue = pro.Value.Split('|');
                    var product = ProductBLL.Instance.Product_Get(pro.Key);

                    product = Mapper.Map(model, product);  //同步的时候 会覆盖ProductID,ProductCode 需要重新赋值
                    product.ProductID = pro.Key;
                    product.ProductCode = propvalue[1];

                    //保存组ID
                    if (groupid == 0)
                        groupid = product.ProductID;

                    //条码
                    product.BarCode = propvalue[2];
                    //规格
                    product.Specifications = propvalue[3];

                    //图片同步
                    product.Images = info.Images;
                    product.Description = info.Description;
                    product.Promotion = info.Promotion;
                    product.Instructions = info.Instructions;

                    var prop = new ProductPropValueInfo();
                    prop.Prop1 = propvalue[4];
                    prop.Prop2 = propvalue[5];
                    prop.Prop3 = propvalue[6];

                    productlist.Add(this.EditProduct(product, ins, prop, groupid));
                }
                result.status = true;
            }

            //处理添加商品  
            //获取当前商品.肯定是编辑过的。提出来做模板
            //同时处理掉ID 和金象码,根据提交的数据 重新生成
            var count = 0;
            info = ProductBLL.Instance.Product_Get(model.ProductID);
            int.TryParse(Request["hidden_count"], out count);
            for (int i = 1; i <= count; i++)
            {
                var val = Request["newproduct" + i];
                if (string.IsNullOrWhiteSpace(val))
                    continue;

                var propvalue = val.Split('|');
                info.ProductID = 0;
                info.ProductCode = propvalue[1];
                info.BarCode = propvalue[2];
                //规格
                info.Specifications = propvalue[3];

                var prop = new ProductPropValueInfo();
                prop.Prop1 = propvalue[4];
                prop.Prop2 = propvalue[5];
                prop.Prop3 = propvalue[6];

                productlist.Add(this.EditProduct(info, ins, prop, groupid));
            }

            if (!result.status)
            {
                result.msg = "商品信息编辑失败!";
            }

            var speclist = productlist.Select(t => t.Specifications).Distinct().ToList();
            if (speclist.Count > 1)
            {
                //提取规格,合并
                var productIDs = productlist.Where(t => !string.IsNullOrWhiteSpace(t.Specifications)).Select(t => t.ProductID).ToList();
                if (productIDs.Count > 1)
                    ProductRelatedBLL.Instance.ProductRelated_SaveDifferentSpec(productIDs, base.UNICKNAME);
            }
            return Json(result);
        }

        [NonAction]
        public ProductInfo EditProduct(ProductInfo p, ProductInstructionsInfo ins, ProductPropValueInfo prop, int groupid)
        {
            var flag = false;
            var brand = BrandBLL.Instance.Brand_Get(p.BrandID);
            p.BrandName = brand.ChineseName;

            var value = new ValueListInfo(p.ValueList);
            value.ParameterValue = ins.ToProductParameter(p);
            value.Instructions = ins.ToString();
            p.ValueList = value.ToString();
            p.ManufacturerName = ins.Manufacturer;
            p.ReckonType();
            if (p.ProductID == 0)
            {
                p.Creator = base.UNICKNAME;
                p.ProductID = ProductBLL.Instance.Product_AddNew(p);
            }
            else
            {
                p.Updater = base.UNICKNAME;
                p.UpdateTime = DateTime.Now;
                flag = ProductBLL.Instance.Product_Update(p);
            }
            ins.ProductID = p.ProductID;
            prop.ProductID = p.ProductID;
            prop.GroupID = groupid;
            prop.Spec = p.Specifications;

            flag = ProductBLL.Instance.Product_SaveInstructions(ins);
            ProductParameterPriceBLL.Instance.ProductPropValue_Save(prop);
            ProductBLL.Instance.Product_SaveSupplier(p.ProductID, p.SupplierID);
            ProductBLL.Instance.ProductAudit_Save(p.ProductID, Product.AuditType.商品信息, base.UNICKNAME);
            ProductProcessBLL.Instance.Update(p.ProductID, Product.Process.产品信息维护, base.UNICKNAME);
            ProductProcessBLL.Instance.Product_RoleMessage_Save(RoleType.商品信息, p.ProductID, base.UNICKNAME);
            return p;
        }

        [HttpPost]
        public ActionResult EditImage(int productid)
        {
            var result = new JsonResultObject(true);
            var product = ProductBLL.Instance.Product_Get(productid);
            //操作商品信息

            product.Updater = base.UNICKNAME;
            product.UpdateTime = DateTime.Now;
            product.Status = 3;
            ProductBLL.Instance.Product_Update(product);
            ProductBLL.Instance.ProductAudit_Save(product.ProductID, Product.AuditType.商品信息, base.UNICKNAME);
            ProductProcessBLL.Instance.Update(product.ProductID, Product.Process.产品信息维护, base.UNICKNAME);
            ProductProcessBLL.Instance.Product_RoleMessage_Save(RoleType.商品信息, product.ProductID, base.UNICKNAME);
            var productlist = new List<ProductInfo>();
            productlist.Add(product);
            return Json(result);
        }

        //添加商品
        [HttpGet]
        public ActionResult AddProduct()
        {
            int recordCount = 0;
            var brands = BrandBLL.Instance.Brand_GetList(1, 0, "pinyinname asc", "", out recordCount).OrderBy(t => t.ChineseName).Select(t => new SelectListItem() { Value = t.BrandID.ToString(), Text = t.ChineseName }).ToList();
            var suppliers = ProductBLL.Instance.Product_GetSupplier().Select(t => new SelectListItem() { Text = t.Value, Value = t.Key.ToString() }).ToList();
            brands.Insert(0, new SelectListItem() { Text = "请选择", Value = "" });
            suppliers.Insert(0, new SelectListItem() { Text = "请选择", Value = "" });
            ViewBag.Brands = brands;
            ViewBag.Suppliers = suppliers;
            ViewBag.IsEdit = JXProduct.AdminUI.App_Start.Auth.UserAuth.hasAddInfo(base.ROLENAME);

            return View();
        }

        [HttpPost]
        public JsonResult AddProduct(ProductModel model)
        {
            var result = new JsonResultObject();
            if (ModelState.IsValid)
            {
                result.msg = "请填写相关数据!";
                return Json(result);
            }

            ProductInfo p = null;
            int count = 0;
            int.TryParse(Request["hidden_count"], out count);
            if (count > 0)
            {

                List<ProductInfo> list = new List<ProductInfo>();
                var paras = new List<string>();
                var codes = new List<string>();
                for (int i = 1; i <= count; i++)
                {
                    var data = Request["newproduct" + i] + "";
                    if (string.IsNullOrWhiteSpace(data))
                        continue;
                    paras.Add(data);
                    codes.Add(data.Split('|')[1]);
                }

                //判断是否有金象码存在
                if (codes.Count > 0)
                {
                    var f = ProductBLL.Instance.CheckProductCode(string.Join(",", codes));
                    if (f)
                    {
                        result.msg = "已经有金象码存在!";
                        return Json(result);
                    }
                }
                else
                {
                    result.msg = "没有获取到数据!";
                    return Json(result);
                }

                foreach (var para in paras)
                {
                    //var temp = 'id|code|barcode|guige|color|size|dushu';
                    p = new ProductInfo();
                    p = Mapper.Map(model, p);
                    var arr = para.Split('|');
                    p.ProductCode = arr[1];
                    p.BarCode = arr[2];
                    p.Specifications = arr[3];

                    p.ReckonType();

                    p.ProductID = ProductBLL.Instance.Product_AddNew(p);
                    if (p.ProductID > 0)
                    {

                        var prop = new ProductPropValueInfo();
                        prop.ProductID = p.ProductID;
                        prop.Prop1 = arr[4];
                        prop.Prop2 = arr[5];
                        prop.Prop3 = arr[6];

                        //属性,供应商
                        ProductParameterPriceBLL.Instance.ProductPropValue_Save(prop);
                        ProductBLL.Instance.Product_SaveSupplier(p.ProductID, p.SupplierID);
                        ProductProcessBLL.Instance.Product_RoleMessage_Save(RoleType.商品信息, p.ProductID, base.UNICKNAME);
                        var ins = new ProductInstructionsInfo()
                        {
                            ProductID = p.ProductID,
                            TradeName = model.TradeName,
                            Manufacturer = model.Manufacturer,
                            ManufacturerAddress = model.ManufacturerAddress,
                            ApprovalNumber = model.ApprovalNumber,
                            CADN = model.CADN,
                            InsDosage = model.Dosage,
                            PinyinName = model.PinyinName,
                            Specifications = p.Specifications
                        };
                        ProductBLL.Instance.Product_SaveInstructions(ins);
                    }
                    list.Add(p);
                }

                var tablelist = list.Select(t =>
                    new
                    {
                        t.ProductID,
                        t.ProductCode,
                        TradeName = t.TradeName + "",
                        t.CADN,
                        t.TypeName,
                        t.Specifications,
                        DrugType = JXUtil.EnumHelper.GetText<Product.DrugType>(t.DrugType),
                        Manufacter = model.Manufacturer,
                        SupplierName = model.SupplierName
                    });


                var speclist = list.Select(t => t.Specifications).Distinct().ToList();
                if (speclist.Count > 1)
                {
                    //提取规格,并且
                    var productIDs = list.Where(t => !string.IsNullOrWhiteSpace(t.Specifications)).Select(t => t.ProductID).ToList();
                    if (productIDs.Count > 1)
                        ProductRelatedBLL.Instance.ProductRelated_SaveDifferentSpec(productIDs, base.UNICKNAME);
                }

                result.status = true;
                result.data = tablelist.ToList();
            }
            else
            {
                result.msg = "请填写金象码!";
            }
            return Json(result);
        }

        public JsonResult GetManufacturer()
        {
            int recordCount = 0;
            var result = new JsonResultObject();
            result.data = ManufacturerBLL.Instance.Manufacturer_GetList(1, 0, "0", "", out recordCount).Select(t => new
            {
                mid = t.ManuID,
                mname = t.Manufacturer,
                maddress = t.Address,
                zipcode = t.Postalcode,
                tel = t.Phone,
                fax = t.Fax,
                url = t.Site
            });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //提供套餐跳转到主商品
        public ActionResult EditCombo(int productid)
        {
            var list = ProductComboBLL.Instance.ProductCombo_GetByProductIDs(new List<int> { productid });
            if (list.Count == 0)
            {
                return Content("当前商品类型错误！请检查!");
            }
            var product = list.FirstOrDefault(t => t.IsMain);
            if (product == null)
            {
                product = list.First();
            }
            return RedirectToAction("Related", new { productid = product.ComboProductID });
        }
        //提供大包装跳转到单品的
        public ActionResult EditPacking(int productid)
        {
            var product = ProductRelatedBLL.Instance.ProductRelated_Get(productid, RelatedType.大包装);
            if (null == product)
                return Content("当前商品类型错误！请检查!");
            return RedirectToAction("related", new { productid = product.ProductID });
        }

        #endregion

        #region 区域编辑

        //商品信息
        public JsonResult EditPrice(int productid, string LongName, decimal tradePrice, decimal marketPrice, decimal mobilePrice)
        {
            var result = new JsonResultObject();
            var product = ProductBLL.Instance.Product_Get(productid);

            if (product.CostPrice > tradePrice)
            {
                result.msg = "成交价应大于成本价";
            }
            else if (tradePrice > marketPrice)
            {
                result.msg = "成交价应小于市场价";
            }
            else if (product.CostPrice > mobilePrice)
            {
                result.msg = "手机端价格应大于成本价";
            }
            else if (mobilePrice > marketPrice)
            {
                result.msg = "手机端价格应小于市场价";
            }
            if (!string.IsNullOrEmpty(result.msg))
            {
                return Json(result);
            }

            product.TradePrice = tradePrice;
            product.MarketPrice = marketPrice;
            product.MobilePrice = mobilePrice;

            product.LongName = JXUtil.SqlUtil.ReplaceInjection2(Request["LongName"] + "");
            result.status = ProductBLL.Instance.Product_Update(product);
            if (result.status)
            {
                ProductProcessBLL.Instance.Update(product.ProductID, Product.Process.运营部价格维护, base.UNICKNAME);
            }
            return Json(result);
        }

        public JsonResult EditSEO(int productid)
        {
            var result = new JsonResultObject();
            var product = ProductBLL.Instance.Product_Get(productid);
            product.Title = JXUtil.SqlUtil.ReplaceInjection2(Request["Title"] + "");
            product.MetaDescription = JXUtil.SqlUtil.ReplaceInjection2(Request["MetaDescription"] + "");
            product.Keywords = JXUtil.SqlUtil.ReplaceInjection2(Request["Keywords"] + "");
            product.MetaKeywords = JXUtil.SqlUtil.ReplaceInjection2(Request["MetaKeywords"] + "");
            result.status = ProductBLL.Instance.Product_Update(product);
            return Json(result);
        }

        public JsonResult EditPromotion(int productid)
        {
            var result = new JsonResultObject();
            var product = ProductBLL.Instance.Product_Get(productid);
            product.Promotion = Server.UrlDecode(Request["Promotion"] + "");
            result.status = ProductBLL.Instance.Product_Update(product);
            ProductBLL.Instance.ProductAudit_Save(product.ProductID, Product.AuditType.详情图片, base.UNICKNAME);
            return Json(result);
        }

        #endregion

        #region 编辑说明书

        [HttpGet]
        public ActionResult ProductQuickEdit()
        {
            var cf = ClassificationBLL.Instance.Classification_GetList(0);
            ViewBag.CF = cf;
            return View("ProductQuickEdit");
        }
        [HttpGet]
        public PartialViewResult EditInstructions(int productid)
        {

            var m = new ProductEditModel();
            m.Product = ProductBLL.Instance.Product_Get(productid);
            m.Instructions = ProductBLL.Instance.Product_GetInstructions(productid);
            m.keywords = KeywordBLL.Instance.KeywordProduct_ByProductID(productid);
            m.Manufacturer = ManufacturerBLL.Instance.Manufacturer_Get(m.Product.ManufacturerID);

            return PartialView("editinstructions", m);
        }

        [HttpPost]
        public JsonResult EditInstructions(ProductModel p, ProductInstructionsInfo ins, ManufacturerModel m)
        {
            var result = new JsonResultObject();
            var product = ProductBLL.Instance.Product_Get(p.ProductID);
            var inst = ProductBLL.Instance.Product_GetInstructions(p.ProductID);

            //商品信息
            product.ChineseName = p.CADN;
            product.CADN = p.CADN;
            product.PinyinName = p.PinyinName;
            product.EnglishName = p.EnglishName;

            //product.ProductType = p.ProductType;
            product.ParameterType = p.ParameterType;
            if (product.ParameterType == 1)
            {
                product.DrugType = p.DrugType;
                product.DrugClassification = p.DrugClassification;
                product.Dosage = p.Dosage;
                product.Usage = p.Usage;
            }
            product.Unit = p.Unit;
            product.IsFragile = (short)(p.IsFragile ? 1 : 0);
            product.IsOdour = (short)(p.IsOdour ? 1 : 0);
            product.IsProtection = (short)(p.IsProtection ? 1 : 0);
            product.ContainMHJ = (short)(p.ContainMHJ ? 1 : 0);
            product.ReckonType();
            //企业
            ManufacturerInfo manu = null;
            if (p.ManufacturerID > 0)
            {
                manu = ManufacturerBLL.Instance.Manufacturer_Get(p.ManufacturerID);
            }
            if (null == manu)
            {
                manu = new ManufacturerInfo();
                p.ManufacturerID = 0;
            }

            manu.Manufacturer = m.Manufacturer;
            manu.Phone = m.Tel;
            manu.Fax = m.Fax;
            manu.Address = m.ManufacturerAddress;
            manu.Site = m.Url;
            manu.Postalcode = m.ZipCode;
            if (manu.ManuID > 0)
            {
                manu.LastUpdate = base.UNICKNAME;
                manu.LastUpdateTime = DateTime.Now;
                ManufacturerBLL.Instance.Manufacturer_Update(manu);
            }
            else
            {
                manu.Creator = base.UNICKNAME;
                manu.CreateTime = DateTime.Now;
                manu.ManuID = ManufacturerBLL.Instance.Manufacturer_Insert(manu);
            }

            //保存说明书
            var bakIns = ProductBLL.Instance.Product_GetInstructions(product.ProductID);

            //企业信息
            ins.Manufacturer = m.Manufacturer;
            ins.ManufacturerAddress = m.ManufacturerAddress;

            ins.Weight = bakIns.Weight;
            ins.RevisionDate = bakIns.RevisionDate;
            ins.ApprovedDate = bakIns.ApprovedDate;

            ProductBLL.Instance.Product_SaveInstructions(ins);

            //保存商品信息
            product.ManufacturerID = manu.ManuID;
            product.Updater = base.UNICKNAME;
            product.UpdateTime = DateTime.Now;
            result.status = ProductBLL.Instance.Product_Update(product);

            //记录
            ProductBLL.Instance.Product_EditRecord(product.ProductID);

            return Json(result);
        }

        public JsonResult Search(int cfid, bool selling, int groupid)
        {
            var sb = new StringBuilder("p.Type=0 AND p.CreateTime < '2015-08-20' AND NOT EXISTS(SELECT 1 FROM ProductEditRecord as per where per.ProductID = p.ProductID)");
            if (cfid > 0)
            {
                sb.AppendFormat(" AND EXISTS(SELECT * FROM Classification as c where p.CFID = C.CFID AND  c.[Path] LIKE '{0}/%' ) ", cfid);
            }
            if (selling)
            {
                sb.Append(" AND p.Status=0 AND p.Selling=1");
            }
            if (groupid > 0)
            {
                int start = 0, end = 0;
                switch (groupid)
                {
                    case 1:
                        start = 0; end = 8606;
                        break;
                    case 2:
                        start = 8607; end = 14703;
                        break;
                    case 3:
                        start = 14704; end = 21145;
                        break;
                    case 4:
                        start = 21145; end = 100000;
                        break;
                    default: break;
                }
                sb.AppendFormat(" AND ProductID Between {0} and {1} ", start, end);
            }
            int recordCount = 0;
            var product = ProductBLL.Instance.Product_GetList(1, 200, "sellcount desc", sb.ToString(), out recordCount);
            var result = new JsonResultObject(true);
            result.data = product;
            result.msg = recordCount.ToString();
            return Json(result);
        }

        #endregion

        #region 价格 Price

        //价格
        [HttpGet]
        public ViewResult Price(int productid)
        {
            var product = ProductBLL.Instance.Product_Get(productid);
            var related = ProductRelatedBLL.Instance.ProductRelated_GetList(productid, RelatedType.大包装);
            var comboIDs = ProductComboBLL.Instance.ProductCombo_Get(productid);
            var productids = related.Select(t => t.ChildProductID).ToList();
            productids.AddRange(comboIDs);

            var productlist = ProductBLL.Instance.Product_GetList(productids);

            var dic = new Dictionary<string, ProductInfo>();
            if (related != null && related.Count > 0)
            {
                for (int i = 0; i < related.Count; i++)
                {
                    var productinfo = productlist.FirstOrDefault(t => t.ProductID == related[i].ChildProductID);
                    if (null != productinfo)
                        dic.Add("大包装" + (i + 1), productinfo);
                }
            }
            if (comboIDs.Count > 0)
            {
                for (int i = 0; i < comboIDs.Count; i++)
                {
                    var productinfo = productlist.FirstOrDefault(t => t.ProductID == comboIDs[i]);
                    if (null != productinfo)
                        dic.Add("套餐" + (i + 1), productinfo);
                }
            }
            ViewBag.ProductInfo = product;
            ViewBag.ProductList = dic;
            return View();
        }
        [HttpPost]
        public JsonResult Price(PriceModel model)
        {
            var result = new JsonResultObject();
            if (ModelState.IsValid)
            {
                var product = ProductBLL.Instance.Product_Get(model.ProductID);
                if (product.CheckPrice > 0)
                {
                    if (model.TradePrice < product.CheckPrice)
                    {
                        model.TradePrice = product.CheckPrice;
                    }
                    if (model.MobilePrice < product.CheckPrice)
                    {
                        model.MobilePrice = product.CheckPrice;
                    }
                }
                if (model.ValidatePrice(product.CostPrice))
                {
                    result.status = ProductBLL.Instance.Product_PriceEdit(model.ProductID, model.MarketPrice, model.TradePrice, model.MobilePrice, base.UNICKNAME);
                }
                else
                {
                    result.msg = "价格错误!检查数据完整";
                }
            }
            else
            {
                result.msg = ModelState.Values.First().Errors[0].ErrorMessage;
            }
            return Json(result);
        }

        #endregion

        #region 快速编辑

        public JsonResult QuickEdit(QuickEditModel model)
        {
            var result = new JsonResultObject();
            model.Creator = base.UNICKNAME;
            result.status = ProductBLL.Instance.Product_QuickEdit(model);
            OperateLogBLL.Instance.OperateLog_Insert(base.UID, base.UNICKNAME, "快速编辑商品", "商品ID:" + model.ProductID);
            return Json(result);
        }

        #endregion

        #region 单品活动 Activity

        [HttpGet]
        public ActionResult Activity(int productid)
        {
            ActivityModel model = null;
            ProductInfo productinfo = null;
            ProductActivityInfo activityinfo = null;
            if (productid > 0)
            {
                productinfo = ProductBLL.Instance.Product_Get(productid);
            }
            if (null == productinfo)
                return new EmptyResult();

            model = new ActivityModel();
            model.Product = productinfo;

            string ex = string.Empty;

            activityinfo = ProductActivityBLL.Instance.ProductActivity_Get(productid);

            if (activityinfo != null)
            {

                model.ActDesc = activityinfo.ActDesc;
                model.ActPrice = activityinfo.ActPrice;
                model.ActQuantity = activityinfo.ActQuantity;
                model.CouponBatchNo = activityinfo.CouponBatchNo;
                model.CouponName = activityinfo.CouponName;
                model.Discount = activityinfo.Discount;
                model.ProductGiftID = activityinfo.ProductGiftID;
                model.ProductGiftName = activityinfo.ProductGiftName;
                model.ProductID = activityinfo.ProductID;

                if (!string.IsNullOrEmpty(activityinfo.ExtType))
                {
                    model.isBuyCard = activityinfo.ExtType.Contains("shoppingcard");
                    model.isCoupon = activityinfo.ExtType.Contains("coupon");
                }
                model.StartDate = activityinfo.StartDate.ToString("yyyy-MM-dd HH:mm");
                model.EndDate = activityinfo.EndDate.ToString("yyyy-MM-dd HH:mm");

                //构造Type
                if (activityinfo.TypeLimit > 0)
                {
                    model.Type = activityinfo.Type.ToString() + activityinfo.TypeLimit.ToString();
                }
                else
                {
                    model.Type = activityinfo.Type.ToString();
                }
            }
            else
            {
                model.Type = "0";
                model.ProductID = productid;
                model.StartDate = DateTime.Now.ToString("yyyy-MM-dd HH:00");
                model.EndDate = DateTime.Now.AddDays(2).ToString("yyyy-MM-dd HH:00");
            }

            return View("Activity", model);
        }

        [HttpPost]
        public JsonResult ActivityEdit(ActivityModel model)
        {
            var result = new JsonResultObject();
            var activityinfo = ProductActivityBLL.Instance.ProductActivity_Get(model.ProductID);

            //合并数据
            //var newinfo = Mapper.Map(model, model);
            short type = 0;
            short.TryParse(model.Type.Length >= 1 ? model.Type.Substring(0, 1) : "0", out type);

            activityinfo.Type = type;
            if (type > 0)
            {
                activityinfo.ActDesc = model.ActDesc;
                activityinfo.ActPrice = model.ActPrice;
                activityinfo.ActQuantity = model.ActQuantity;
                activityinfo.CouponBatchNo = model.CouponBatchNo;
                activityinfo.CouponName = model.CouponName;
                activityinfo.Discount = model.Discount;
                activityinfo.ProductGiftID = model.ProductGiftID;
                activityinfo.ProductGiftName = model.ProductGiftName;
                activityinfo.ProductID = model.ProductID;
                var start = DateTime.Now;
                var end = DateTime.Now;
                DateTime.TryParse(model.StartDate, out start);
                DateTime.TryParse(model.EndDate, out end);
                activityinfo.StartDate = start;
                activityinfo.EndDate = end;
                activityinfo.ExtType = model.isBuyCard ? "shoppingcard," : "";
                activityinfo.ExtType = activityinfo.ExtType + (model.isCoupon ? "coupon," : "");
            }
            else
            {
                activityinfo = new ProductActivityInfo();
                activityinfo.ProductID = model.ProductID;
            }
            if (ProductActivityBLL.Instance.ValidateActivity(activityinfo))
            {

                activityinfo.Updater = base.UNICKNAME;
                result.status = ProductActivityBLL.Instance.ProductActivity_Update(activityinfo);
                OperateLogBLL.Instance.OperateLog_Insert(base.UID, base.UNICKNAME, "活动编辑", model.ProductID.ToString() + "/" + model.Type + "/" + model.ActDesc);
            }
            else
            {
                result.msg = "数据验证不通过!";
            }
            return Json(result);
        }

        #endregion

        #region 图片上传

        public JsonResult UploadImage(int productid, int index)
        {
            var file = HttpContext.Request.Files[0];
            var result = ProductImage.Instance.ProductUpload(productid, index, file);
            if (result.status)
            {
                var path = JXAPI.JXSdk.Utils.JsonHelper.ConvertToObj<List<JXAPI.JXSdk.Request.ImagePathRequest>>(result.data.ToString());
                var product = ProductBLL.Instance.Product_Get(productid);
                result.msg = path[0].ImagePath.Replace("\\", "/").TrimStart('/');
                result.data = path[0].Url + result.msg;

                product.Images = ProductBLL.Instance.Product_UpdateImage(product.Images, index, path[0].ImagePath.Replace("\\", "/").TrimStart('/').Replace("_L", "_S"));
                product.UpdateTime = DateTime.Now;
                result.status = ProductBLL.Instance.Product_Update(product);
                if (!result.status)
                {
                    result.msg = "更新信息失败!";
                    result.data = "";
                }
            }
            return Json(result);
        }
        public JsonResult UploadOtherImage(int productid, int index)
        {
            var file = HttpContext.Request.Files[0];
            var result = ProductImage.Instance.OtherUpload("desc_" + productid, index, file);
            if (result.status)
            {
                var path = JXAPI.JXSdk.Utils.JsonHelper.ConvertToObj<List<JXAPI.JXSdk.Request.ImagePathRequest>>(result.data.ToString());

                result.data = path[0].Url + path[0].ImagePath.Replace("\\", "/").TrimStart('/');
                //var product = ProductBLL.Instance.Product_Get(productid);
                //ProductBLL.Instance.Product_UpdateOtherImage(productid, index, result.data.ToString());
                result.status = ProductBLL.Instance.Product_UpdateOtherImage(productid, index, result.data.ToString());
                if (!result.status)
                {
                    result.msg = "更新信息失败!";
                    result.data = "";
                }
            }
            return Json(result);
        }
        public JsonResult UploadShuoming(int productid)
        {
            var file = HttpContext.Request.Files[0];
            var result = ProductImage.Instance.OtherUpload("sms_" + productid, 0, file);
            if (result.status)
            {
                var path = JXAPI.JXSdk.Utils.JsonHelper.ConvertToObj<List<JXAPI.JXSdk.Request.ImagePathRequest>>(result.data.ToString());
                var product = ProductBLL.Instance.Product_Get(productid);
                result.data = path[0].Url + path[0].ImagePath.Replace("\\", "/").TrimStart('/');
                product.Instructions = path[0].ImagePath.TrimStart('/');
                product.UpdateTime = DateTime.Now;
                result.status = ProductBLL.Instance.Product_Update(product);
                if (!result.status)
                {
                    result.msg = "更新信息失败!";
                    result.data = "";
                }
            }
            return Json(result);
        }
        public JsonResult DelImage(int productid, int t, int index)
        {
            var result = new JsonResultObject();
            var product = ProductBLL.Instance.Product_Get(productid);
            switch (t)
            {
                case 1: //商品图片
                    {
                        var dic = ProductBLL.Instance.GetImages(product.Images);
                        dic.Remove(index);
                        product.Images = string.Join("|", dic.OrderBy(d => d.Key).Where(d => d.Value != "").Select(d => d.Value));
                        result.status = ProductBLL.Instance.Product_Update(product);
                    }
                    break;
                case 2: //商品说明
                    {
                        // var dic = ProductBLL.Instance.GetDescImages(product.Description);
                        //product.Description = string.Join("|", dic.OrderBy(d => d.Key).Where(d => d.Value != "").Select(d => d.Value));
                        result.status = ProductBLL.Instance.Product_UpdateOtherImage(productid, index, "");
                    }
                    break;
                case 3: //说明书
                    {
                        product.Instructions = "";
                        result.status = ProductBLL.Instance.Product_Update(product);
                    }
                    break;
                default:
                    t = 0;
                    break;
            }
            return Json(result);
        }

        #endregion

        #region keywords

        public JsonResult AddKeyword(int productid, int keywordid, string chinesename)
        {
            var result = new JsonResultObject();
            if (!string.IsNullOrWhiteSpace(chinesename))
            {
                var keywordlist = KeywordBLL.Instance.Keyword_GetByName(chinesename);
                if (keywordlist != null && keywordlist.Count > 0)
                {
                    keywordid = keywordlist[0].KeywordID;
                }
                else
                {
                    var k = KeywordBLL.Instance.Keyword_Insert(
                        new KeywordInfo
                        {
                            ChineseName = chinesename,
                            PinyinName = JXUtil.PinyinUtil.ConvertToPinyin(chinesename),
                            FirstLetter = PinyinUtil.ConvertToPinyin(chinesename).SubStr(1),
                            Status = 0,
                            TypeID = 0,
                            Creator = base.UNICKNAME,
                            Updater = base.UNICKNAME
                        });
                    if (k.KeywordID == 0)
                    {
                        result.msg = "添加失败!";
                    }
                    else
                    {
                        keywordid = k.KeywordID;
                    }
                }
            }
            if (productid > 0 && keywordid > 0)
            {
                result.msg = KeywordBLL.Instance.KeywordProduct_Save(keywordid, productid, 100, 0);
                if (result.msg == "0")
                {
                    result.status = true;
                    result.data = keywordid;
                }
            }
            return Json(result);
        }
        public JsonResult DelKeyword(int productid, int keywordid)
        {
            var result = new JsonResultObject();
            result.msg = KeywordBLL.Instance.KeywordProduct_Save(keywordid, productid, 0, 1);
            result.status = result.msg == "0";
            return Json(result);
        }
        public JsonResult GetKeywords()
        {
            var result = new JsonResultObject();
            var name = Request["q"];
            if (!string.IsNullOrWhiteSpace(name))
            {
                var list = KeywordBLL.Instance.Keyword_GetByName(name, true);
                if (list.Count > 0)
                {
                    result.data = list;
                    result.status = true;
                }
            }
            return Json(result);
        }
        #endregion

        #region  组合商品

        [HttpPost]
        public JsonResult Combo(int? productid, int? mainproductid, string comboname, string data)
        {
            var pid = productid ?? 0;
            var mpid = mainproductid ?? 0;

            var result = new JsonResultObject();
            if ((pid == 0 && mpid == 0) || string.IsNullOrEmpty(data))
            {
                result.msg = "数据错误!";
            }
            else
            {
                List<ProductComboInfo> list = new List<ProductComboInfo>();
                data = Server.UrlDecode(data);
                XDocument doc = XDocument.Parse(data);
                list = (from t in doc.Descendants("item")
                        select new ProductComboInfo
                        {
                            ComboProductID = Convert.ToInt32(t.Element("comboproductid").Value),
                            Name = t.Element("name").Value,
                            Price = Convert.ToDecimal(t.Element("price").Value),
                            Quantity = Convert.ToInt32(t.Element("quantity").Value),
                            IsMain = (Convert.ToInt32(t.Element("comboproductid").Value) == mpid || Convert.ToInt32(t.Element("comboproductid").Value) == pid)
                        }).ToList();

                if (list.Count < 2)
                {
                    result.msg = "参数不正确!";
                    return Json(result);
                }

                if (mpid > 0)
                {
                    pid = ProductComboBLL.Instance.ProductCombo_Insert(mpid, comboname, base.UNICKNAME, list);
                    result.status = pid > 0;
                }
                else if (pid > 0)
                {
                    result.status = ProductComboBLL.Instance.ProductCombo_Update(pid, comboname, base.UNICKNAME, list);
                }
                else
                {
                    result.status = productid > 0;
                }
                //更新图片操作
                //已在存储过程里面做处理
            }
            return Json(result);
        }

        [HttpGet]
        public PartialViewResult Combo(int productid)
        {
            return PartialView("_PartialCombo", productid);
        }

        [HttpPost]
        public JsonResult ComboDel(int productid)
        {
            var result = new JsonResultObject();
            result.status = ProductComboBLL.Instance.ProductCombo_Delete(productid);
            OperateLogBLL.Instance.OperateLog_Insert(base.UID, base.UNICKNAME, "组合商品删除", productid.ToString());
            return Json(result);
        }

        public JsonResult ComboDetail(int productid)
        {
            var result = new JsonResultObject(true);
            result.data = ProductComboBLL.Instance.ProductCombo_GetByProductIDs(new List<int> { productid }).OrderByDescending(t => t.IsMain);
            return Json(result);
        }
        #endregion

        #region 大包装/推荐/规格 ProductRelated

        [HttpGet]
        public ActionResult Related(int productid)
        {
            var p = ProductBLL.Instance.Product_Get(productid);
            if (p == null)
                return new EmptyResult();

            int type = 0;
            int.TryParse(Request["type"], out type);

            var model = new RelatedModel();
            model.ProductID = productid;
            model.RelatedType = type;
            if (Request.IsAjaxRequest() || Request["isajax"] == "1")
            {
                return PartialView("_PartialRelated", model);
            }

            if (p.Type == 1)
            {
                int recordCount = 0;
                var plist = ProductBLL.Instance.Product_GetList(1, 0, "1", " ProductCode='" + p.ProductCode + "' and Type=0", out recordCount);
                model.BaseProduct = plist.FirstOrDefault();
            }

            model.Product = p;
            return View(model);
        }

        [HttpPost]
        public JsonResult Related_LargePacking(int? relatedid, int productid, int quantity, decimal price, string viewname)
        {
            var rid = relatedid ?? 0;
            var result = new JsonResultObject();
            if (rid > 0 || quantity > 1)
            {

                if ((rid > 0 || productid > 0) && price > 0 && !string.IsNullOrEmpty(viewname))
                {
                    result.status = ProductRelatedBLL.Instance.ProductRelated_SavePackaging(
                        rid,
                        productid,
                        quantity,
                        price,
                        viewname,
                        base.UNICKNAME
                        );
                    OperateLogBLL.Instance.OperateLog_Insert(base.UID, base.UNICKNAME, "大包装添加", productid.ToString());
                }
            }
            else
            {
                result.msg = "输入的数量应该大于1";
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult Related_BestRecommend(int productid, string productIDs)
        {
            var result = new JsonResultObject();
            if (productid > 0 && productIDs.Length > 0)
            {
                result.status = ProductRelatedBLL.Instance.ProductRelated_SaveBestRecommend(productid, productIDs, base.UNICKNAME);
                OperateLogBLL.Instance.OperateLog_Insert(base.UID, base.UNICKNAME, "最佳推荐", productid.ToString());
            }
            return Json(result);
        }

        //[HttpPost]
        //public JsonResult Related_DifferentSpec(ProductRelatedInfo model)
        //{
        //    var result = new JsonResultObject();
        //    model.Creator = base.UNICKNAME;
        //    var r = ProductRelatedBLL.Instance.ProductRelated_SaveDifferentSpec(model);
        //    switch (r)
        //    {
        //        case 2:
        //        case 1:
        //            OperateLogBLL.Instance.OperateLog_Insert(base.UID, base.UNICKNAME, "规格编辑", model.ChildProductID.ToString());
        //            result.status = true;
        //            break;
        //        case -1:
        //            result.msg = "添加重复!";
        //            break;
        //        default: break;

        //    }
        //    return Json(result);
        //}

        [HttpPost]
        public JsonResult Related_Del(int relatedID)
        {
            var result = new JsonResultObject();
            if (relatedID > 0)
            {
                var r = ProductRelatedBLL.Instance.ProductRelated_Delete(relatedID);
                result.status = r;
                if (!r)
                {
                    result.msg = "参数错误!或者还有子商品";
                }
                OperateLogBLL.Instance.OperateLog_Insert(base.UID, base.UNICKNAME, "商品关联删除", relatedID.ToString());
            }
            else
            {
                result.msg = "参数错误!";
            }
            return Json(result);
        }

        #endregion

        #region lm For商品评价管理
        public ActionResult EvalList(int statusType = -1, string sourceType = "-1", string sTime = "null", string eTime = "null", string pName = "null", int pageIndex = 1)
        {
            ProductEvaluationListRequest evalRequest = new ProductEvaluationListRequest();
            IList<ProductInfo> productInfoList = new List<ProductInfo>();//商品信息结果集
            bool isExist = false;
            int pageSize = 10;
            if (statusType != -1)
            {
                evalRequest.status = statusType;

            }
            if (sourceType != "-1")
            {
                evalRequest.source = int.Parse(sourceType);
            }
            if (sTime != "null" && eTime != "null")
            {

                DateTime endTime = DateTime.Parse(eTime);
                endTime = endTime.AddDays(1).AddMinutes(-1);
                evalRequest.ceilTime = JXAPI.JXSdk.Utils.ConvertDataTimeHelper.ConvertDataTimeLong(endTime);

                DateTime startTime = DateTime.Parse(sTime);
                evalRequest.floorTime = JXAPI.JXSdk.Utils.ConvertDataTimeHelper.ConvertDataTimeLong(startTime);
            }
            if (pName != "null")
            {
                evalRequest.productName = pName;
            }
            else
            {
                pName = "";
            }
            JXAPI.JXSdk.Domain.PageFormInfo pageInfo = new JXAPI.JXSdk.Domain.PageFormInfo() { page = pageIndex, size = pageSize };
            evalRequest.pageForm = pageInfo;
            ProductEvaluationListResponse evalResposne = ProductEvaluationService.Instance.List(evalRequest);

            IList<JXAPI.JXSdk.Domain.ProductEvaluationInfo> productEvaluationInfoList = evalResposne.evaluationList;

            #region 产品ID与产品信息关联
            if (productEvaluationInfoList != null)
            {
                for (int i = 0; i < productEvaluationInfoList.Count; i++)
                {
                    try
                    {
                        ProductInfo productInfo = ProductBLL.Instance.Product_Get(productEvaluationInfoList[i].productID);
                        if (productInfo != null)
                        {
                            foreach (var item in productInfoList)
                            {
                                if (productInfo.ProductID == item.ProductID)
                                {
                                    isExist = true;
                                    continue;
                                }
                                isExist = false;
                            }
                            if (!isExist)
                            {
                                productInfoList.Add(productInfo);
                            }
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            #endregion

            if (productEvaluationInfoList != null)
            {

                ViewBag.PageList = new PagedList<JXAPI.JXSdk.Domain.ProductEvaluationInfo>(productEvaluationInfoList, pageIndex, pageSize, evalResposne.total);
            }
            ViewBag.ProductEvaluationInfoList = productEvaluationInfoList;
            ViewBag.ProductInfoList = productInfoList;
            ViewBag.StatusType = statusType;
            ViewBag.SourceType = sourceType;
            ViewBag.STime = sTime;
            ViewBag.ETime = eTime;
            ViewBag.PName = pName;
            return View();
        }

        public JsonResult ModifyEvalStatus(int evalid)
        {
            var result = new JsonResultObject();
            JXAPI.JXSdk.Domain.ProductEvaluationInfo productEvaluationInfo =
                                                                       ProductEvaluationService.Instance.Get(evalid);
            if (productEvaluationInfo == null)
            {
                result.status = false;
                result.msg = "需要更新的数据不完整！";
                return Json(result);
            }
            switch (productEvaluationInfo.status)
            {
                case 0: productEvaluationInfo.status = 1;
                    result.msg = "已修改为 已审核 状态！";
                    break;
                case 1: productEvaluationInfo.status = 0;
                    result.msg = "已修改为 未审核 状态！";
                    break;
            }
            result.status = ProductEvaluationService.Instance.CheckEvaluation(evalid, productEvaluationInfo.status, base.UNICKNAME);
            if (result.status == false)
            {
                result.msg = "更新审核失败,未能连接至网络！";
            }
            return Json(result);
        }

        public JsonResult Eval_Get(int evalid)
        {
            var result = new JsonResultObject();
            string parentContent = string.Empty;
            int parentId = 0;
            string keywordStr = string.Empty;
            JXAPI.JXSdk.Domain.ProductEvaluationInfo proudctEvaluationInfo
                                                    = ProductEvaluationService.Instance.Get(evalid);
            if (proudctEvaluationInfo == null)
            {
                result.msg = "评价信息获取失败，未能连接至网络！";
                return Json(result);
            }
            if (proudctEvaluationInfo.evaluationKeywordList != null && proudctEvaluationInfo.evaluationKeywordList.Count > 0)
            {
                for (int i = 0; i < proudctEvaluationInfo.evaluationKeywordList.Count; i++)
                {

                    if (i != proudctEvaluationInfo.evaluationKeywordList.Count - 1)
                    {
                        keywordStr += proudctEvaluationInfo.evaluationKeywordList[i].chineseName + "、";
                    }
                    else
                    {
                        keywordStr += proudctEvaluationInfo.evaluationKeywordList[i].chineseName;
                    }
                }
            }
            if (proudctEvaluationInfo.productID != 0)
            {
                JXAPI.JXSdk.Domain.ProductEvaluationInfo ParentProudctEvaluationInfo
                                        = ProductEvaluationService.Instance.Get(proudctEvaluationInfo.parentID);
                parentContent = ParentProudctEvaluationInfo.content;
                parentId = ParentProudctEvaluationInfo.evaluationID;
            }
            var data = new { keyword = keywordStr, selfContent = proudctEvaluationInfo.content, parentId = parentId, parentContent = parentContent };
            result.data = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            result.status = true;
            return Json(result);
        }
        #endregion

        #region 拼音
        public JsonResult GetPY(string name)
        {
            var result = new JsonResultObject(true);
            result.data = JXUtil.PinyinUtil.ConvertToPinyin(name);
            return Json(result);
        }
        #endregion
    }
}