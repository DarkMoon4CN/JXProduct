using JXProduct.AdminUI.Models.Classification;
using JXProduct.AdminUI.Models.Keyword;
using JXProduct.Component.BLL;
using JXProduct.Component.Model;
using JXUtil;
using System;
using System.Linq;
using System.Web.Mvc;
namespace JXProduct.AdminUI.Controllers
{
    public class ClassificationController : JXAdminAuthBase.BaseController
    {
        public ActionResult List()
        {
            var model = new ClassificationModel();
            int recordCount = 0;
            var cflist = ClassificationBLL.Instance.Classification_GetList(1, 0, "sort asc", "parentid=0", out recordCount);
            var sectionlist = SectionBLL.Instance.Section_GetList(0);

            model.CFList = cflist.ToList();
            model.SectionList = sectionlist.ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int? parentid, int? cfid)
        {
            if (parentid.HasValue || cfid.HasValue)
            {
                var model = new ClassificationEditModel();
                if (parentid.HasValue)
                {
                    var parent = ClassificationBLL.Instance.Classification_Get(parentid.Value);
                    model.ParentChineseName = parent.ChineseName;
                    model.ParentID = parent.CFID;
                }
                else
                {
                    var cf = ClassificationBLL.Instance.Classification_Get(cfid.Value);
                    model.CFID = cf.CFID;
                    model.ChineseName = cf.ChineseName;
                }
                return View(model);
            }
            else
            {
                return new EmptyResult();
            }
        }
        [HttpPost]
        public JsonResult Edit(ClassificationEditModel model)
        {
            var result = new JsonResultObject(true);
            if (model.ChineseName.Length > 16)
            {
                result.data = "3";
                return Json(result);
            }

            ClassificationInfo cfinfo = null;
            if (model.CFID > 0)
            {
                cfinfo = ClassificationBLL.Instance.Classification_Get(model.CFID);
                model.ParentID = cfinfo.ParentID;
            }

            // 检查当前分类下是否存在该名称
            int recordCount = 0;
            var list = ClassificationBLL.Instance.Classification_GetList(1, 0, string.Empty, string.Format("ParentID={0} and Chinesename='{1}'", model.ParentID, model.ChineseName), out recordCount);
            if (recordCount > 0)
            {
                result.data = "2";
            }
            else
            {
                if (cfinfo != null)
                {
                    cfinfo.ChineseName = model.ChineseName;
                    cfinfo.PinyinName = JXUtil.PinyinUtil.ConvertToPinyin(cfinfo.ChineseName);
                    cfinfo.Updater = base.UNICKNAME;
                    cfinfo.UpdateTime = DateTime.Now;
                    result.data = ClassificationBLL.Instance.Classification_Update(cfinfo) > 0 ? 1 : 0;
                }
                else
                {
                    cfinfo = new ClassificationInfo();
                    cfinfo.ParentID = model.ParentID;
                    cfinfo.ChineseName = model.ChineseName;
                    cfinfo.PinyinName = JXUtil.PinyinUtil.ConvertToPinyin(cfinfo.ChineseName);
                    cfinfo.Creator = base.UNICKNAME;
                    cfinfo.CreateTime = DateTime.Now;
                    result.data = ClassificationBLL.Instance.Classification_Insert(cfinfo) > 0 ? 1 : 0;
                }

            }
            return Json(result);
        }

        public JsonResult GetByParentid(int parentid)
        {
            var list = ClassificationBLL.Instance.Classification_GetList(parentid);
            var result = new JsonResultObject(true);
            result.data = list;
            if (list.Any(t => t.KeywordID > 0))
            {
                result.msg = "1";
            }
            return Json(result);
        }

        public JsonResult GetByCFID(int cfid)
        {
            var result = new JsonResultObject(true);
            result.data = ClassificationBLL.Instance.Classification_Get(cfid);
            return Json(result);
        }

        public JsonResult GetByCFName(string cfname)
        {
            int recordCount = 0;
            cfname = SqlUtil.ReplaceInjection2(cfname);
            var result = new JsonResultObject(true);
            var list = ClassificationBLL.Instance.Classification_GetList(1, 0, "0", "ChineseName like '%" + cfname + "%'", out recordCount);
            result.data = list;
            return Json(result);
        }
    }
}