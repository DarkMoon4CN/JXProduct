using JXProduct.AdminUI.App_Start.Auth;
using JXProduct.AdminUI.Image;
using JXProduct.AdminUI.Models.Audit;
using JXProduct.Component.BLL;
using JXProduct.Component.Model;
using JXProduct.Component.Enums;
using JXUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JXProduct.AdminUI.Controllers
{
    public class AuditController : JXAdminAuthBase.BaseController
    {
        #region 商品资质审核

        public ActionResult Edit(int productid)
        {
            var model = new AuditEditModel();
            model.Roles = base.ROLENAME;
            var productinfo = ProductBLL.Instance.Product_Get(productid);
            if (productinfo == null)
                return new EmptyResult();

            var category = new List<QualificationInfo>();
            var audit = ProductBLL.Instance.ProductAudit_Get(productid).FirstOrDefault(t => t.Type == (short)JXProduct.Component.Enums.Product.AuditType.商品资质) ?? new ProductAuditInfo() { Type = (short)JXProduct.Component.Enums.Product.AuditType.商品资质 };

            model.audit = audit;
            model.product = productinfo;

            int recordCount = 0;
            model.cfList = ClassificationBLL.Instance.Classification_GetList(1, 0, "", "", out recordCount);
            model.productCF = ProductClassificationBLL.Instance.ProductClassification_GetList(productid);
            model.instructions = ProductBLL.Instance.Product_GetInstructions(productid);
            model.ProductItems = QualificationBLL.Instance.Qualification_GetProductList(productid);
            return View(model);
        }

        public PartialViewResult PartialEdit(AuditEditPartialModel model)
        {
            model.IsEdit = UserAuth.hasEditQualification(base.ROLENAME);

            if (model.CID > 0)
                model.Type = 1;
            else if (model.PID > 0)
                model.Type = 2;
            switch (model.Type)
            {
                case 0:
                    {
                        //企业资质
                        model.ManufacturerItems = QualificationBLL.Instance.Qualification_GetManufacturerList(model.MID, 0, 0);
                    } break;
                case 1:
                    {
                        //企业分类资质
                        model.ManufacturerItems = QualificationBLL.Instance.Qualification_GetManufacturerList(model.MID, 1, model.CID);
                    } break;
                case 2:
                    {
                        //商品资质
                        model.ProductItems = QualificationBLL.Instance.Qualification_GetProductList(model.PID);
                    } break;
                default:
                    break;
            }
            return PartialView("_PartialEdit", model);
        }

        [HttpPost]
        public JsonResult Insert(int qid, int mid, int pid, string number, string imgpath, string StartDate, string EndDate, int pqid = 0)
        {
            var result = new JsonResultObject();
            var start = Convert.ToDateTime("1999-01-01");
            var end = Convert.ToDateTime("1999-01-01");
            DateTime.TryParse(StartDate, out  start);
            DateTime.TryParse(EndDate, out end);
            if (mid == 0 && pid == 0)
            {
                result.msg = "参数错误!";
            }
            else if (start.Year < 2000 || end.Year < 2000 || start > end)
            {
                result.msg = "请输入有效期!";
            }
            else if (string.IsNullOrWhiteSpace(number) || number.Length < 3)
            {
                result.msg = "请输入证件号!";
            }
            else if (imgpath.Length < 3)
            {
                result.msg = "请上传图片!";
            }
            else
            {
                var pqinfo = new ProductQualificationInfo()
                     {
                         PQID = pqid,
                         QualificationID = qid,
                         ProductID = pid,
                         ManufacturerID = mid,
                         Number = number,
                         Image = imgpath,
                         StartDate = start,
                         EndDate = end,
                         Status = 0,
                         CreateTime = DateTime.Now
                     };
                var id = QualificationBLL.Instance.Qualification_Insert(pqinfo);
                var productid = 0;
                int.TryParse(Request["productid"], out productid);
                if (productid > 0)
                {
                    ProductBLL.Instance.ProductAudit_Save(productid, Component.Enums.Product.AuditType.商品资质, base.UNICKNAME);
                    ProductProcessBLL.Instance.Update(productid, Component.Enums.Product.Process.质管部信息维护, base.UNICKNAME);
                }
                result.status = id > 0;
            }
            return Json(result);
        }

        public JsonResult upload(int qid, int mid = 0, int pid = 0)
        {
            var file = HttpContext.Request.Files[0];
            var imgname = string.Empty;
            if (mid > 0)
            {
                imgname = string.Format("M_{0}_{1}", mid, qid);
            }
            else if (pid > 0)
            {
                imgname = string.Format("P_{0}_{1}", pid, qid);
            }
            var result = ProductImage.Instance.OtherUpload(imgname, 0, file);
            if (result.status)
            {
                var path = JXAPI.JXSdk.Utils.JsonHelper.ConvertToObj<List<JXAPI.JXSdk.Request.ImagePathRequest>>(result.data.ToString());
                var p = path.First();
                p.ImagePath = p.ImagePath.Replace("\\", "/").TrimStart('/');
                result.data = p;
                if (!result.status)
                {
                    result.msg = "更新信息失败!";
                    result.data = "";
                }
            }
            return Json(result);
        }

        public JsonResult EditAudit(int productid, string cfids)
        {
            var result = new JsonResultObject();
            var cflist = cfids.Split(',').ToList().ConvertAll(t =>
            {
                int id = 0;
                int.TryParse(t, out id);
                return id;
            }).Distinct().Where(t => t > 0).ToList();
            if (cflist.Count > 0)
            {
                ProductBLL.Instance.ProductAudit_Save(productid, Component.Enums.Product.AuditType.商品资质, base.UNICKNAME);
                ProductProcessBLL.Instance.Update(productid, Component.Enums.Product.Process.质管部信息维护, base.UNICKNAME);
                ProductProcessBLL.Instance.Product_RoleMessage_Save(RoleType.质管编辑, productid, base.UNICKNAME);
                result.status = ProductClassificationBLL.Instance.ProductClassification_Insert(productid, cflist, base.UNICKNAME);
            }
            else
            {
                result.msg = "请选择分类";
            }
            return Json(result);
        }

        public ActionResult Preview(int productid)
        {
            var model = new AuditEditModel();
            var category = new List<QualificationInfo>();
            int recordCount = 0;

            model.product = ProductBLL.Instance.Product_Get(productid);
            model.audit = ProductBLL.Instance.ProductAudit_Get(productid).FirstOrDefault(t => t.Type == (short)JXProduct.Component.Enums.Product.AuditType.商品资质) ?? new ProductAuditInfo();

            model.cfList = ClassificationBLL.Instance.Classification_GetList(1, 0, "", "", out recordCount);
            model.productCF = ProductClassificationBLL.Instance.ProductClassification_GetList(productid);
            model.instructions = ProductBLL.Instance.Product_GetInstructions(productid);


            model.Category = QualificationBLL.Instance.Qualification_GetCategory();
            var mitems = QualificationBLL.Instance.Qualification_GetManufacturerList(model.product.ManufacturerID, 0, 0);
            mitems.AddRange(QualificationBLL.Instance.Qualification_GetManufacturerList(model.product.ManufacturerID, 1, 0));
            model.ManufacturerItems = mitems;

            model.ProductItems = QualificationBLL.Instance.Qualification_GetProductList(productid);

            return View(model);
        }

        public ActionResult Expire(int mid)
        {
            var result = new JsonResultObject(true);
            var list = QualificationBLL.Instance.Qualification_GetManuExpire(mid).OrderBy(t => t.Key).Select(t => new
            {
                cid = t.Key,
                count = t.Value
            });
            result.data = list;
            return Json(result);
        }

        #endregion

        #region 商品审核

        public PartialViewResult Get(int mid, int productid, int auditid)
        {
            return PartialView("_PartialAudit");
        }

        public ActionResult NotPass(string productids, int type)
        {
            if (!string.IsNullOrWhiteSpace(productids))
            {
                ViewBag.ProductIDs = productids;
                ViewBag.Type = type;
                return View();
            }
            return new EmptyResult();
        }

        [HttpPost]
        public JsonResult Product(string productids, short type, short status, string remarks)
        {
            var result = new JsonResultObject();
            if (string.IsNullOrWhiteSpace(productids))
            {
                result.data = "缺少必要数据";
            }
            else if (status == (short)Component.Enums.Product.AuditStatus.未通过审核 && string.IsNullOrWhiteSpace(remarks))
            {
                result.data = "需要填写未通过原因";
            }
            else
            {
                var audit = new ProductAuditInfo();
                audit.Type = type;
                audit.Status = status;
                audit.Remarks = remarks;
                audit.Creator = base.UNICKNAME;
                var ids = productids.Split(',').ToList().ConvertAll(t => { int pid = 0; int.TryParse(t, out pid); return pid; });
                foreach (var id in ids)
                {
                    audit.ProductID = id;
                    ProductBLL.Instance.ProductAudit_Save(audit);
                    var auditlist = ProductBLL.Instance.ProductAudit_Get(id);
                    if (audit.Status == 2)
                    {
                        switch (audit.Type)
                        {
                            case 2:
                            case 3:
                                ProductProcessBLL.Instance.Update(id, Component.Enums.Product.Process.产品信息维护, base.UNICKNAME, true);
                                ProductProcessBLL.Instance.Product_RoleMessage_Save(RoleType.产品编辑, id, base.UNICKNAME, true);
                                break;
                            case 1:
                                ProductProcessBLL.Instance.Update(id, Component.Enums.Product.Process.质管部信息维护, base.UNICKNAME, true);
                                ProductProcessBLL.Instance.Product_RoleMessage_Save(RoleType.质管编辑, id, base.UNICKNAME, true);
                                break;
                            default: break;
                        }
                    }
                    else
                    {
                        //同时通过
                        if (auditlist.Any(t => t.Type == 1 && t.Status == 1) && auditlist.Any(t => t.Type == 2 && t.Status == 1))
                        {
                            ProductProcessBLL.Instance.Update(id, Component.Enums.Product.Process.质管部信息审核, base.UNICKNAME);
                            ProductProcessBLL.Instance.Product_RoleMessage_Save(RoleType.运营报价员, id, base.UNICKNAME);
                        }
                    }
                }
                result.status = true;
            }
            return Json(result);
        }

        #endregion

        #region 邮件

        public ActionResult Email()
        {
            var list = AuditStuffEmailBLL.Instance.GetList();
            ViewBag.List = list;
            return View();
        }

        [HttpGet]
        public ActionResult EmailEdit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EmailEdit(AuditStuffEmailInfo model)
        {
            var result = new JsonResultObject();
            model.Creator = base.UNICKNAME;
            model = AuditStuffEmailBLL.Instance.Insert(model);
            result.status = model.EmailID > 0;
            return Json(result);
        }

        [HttpPost]
        public JsonResult EmailDel(int emailid)
        {
            var result = new JsonResultObject();
            result.status = AuditStuffEmailBLL.Instance.Delete(emailid);
            return Json(result);
        }
        #endregion
    }
}