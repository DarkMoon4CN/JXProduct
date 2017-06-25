using JXProduct.Component.BLL;
using JXProduct.Component.Model;
using JXProduct.AdminUI.Models.Section;
using JXUtil;
using System.Linq;
using System.Web.Mvc;


namespace JXProduct.AdminUI.Controllers
{
    public class SectionController : JXAdminAuthBase.BaseController
    {

        #region 科室编辑

        //view
        [HttpGet]
        public ActionResult Edit(SectionEditModel model)
        {
            if (model.SectionID > 0 || model.ParentID > 0)
            {
                if (model.ParentID > 0)
                {
                    var parent = SectionBLL.Instance.Section_Get(model.ParentID);
                    model.ParentChineseName = parent.SectionName;
                    model.ParentID = parent.SectionID;
                }
                else
                {
                    var section = SectionBLL.Instance.Section_Get(model.SectionID);
                    model.SectionID = section.SectionID;
                    model.ChineseName = section.SectionName;
                }
                return View(model);
            }
            else
            {
                return new EmptyResult();
            }
        }

        //post
        [HttpPost]
        public ActionResult EditModel(SectionEditModel model)
        {
            var result = new JsonResultObject();

            if (!string.IsNullOrEmpty(model.ChineseName) && model.ChineseName.Length < 16)
            {
                result.status = true;
                if (model.SectionID > 0)
                {
                    var section = SectionBLL.Instance.Section_Get(model.SectionID);
                    if (model != null)
                    {
                        section.SectionName = model.ChineseName;
                        section.SpellName = JXUtil.PinyinUtil.ConvertToPinyin(section.SectionName);
                        result.data = SectionBLL.Instance.Section_Update(section);
                        OperateLogBLL.Instance.OperateLog_Insert(base.UID, base.UNICKNAME, "科室编辑", section.SectionID.ToString() + section.SectionName);
                    }
                }
                else if (model.ParentID > 0)
                {
                    var section = new SectionInfo();
                    section.SectionName = model.ChineseName;
                    section.SpellName = JXUtil.PinyinUtil.ConvertToPinyin(section.SectionName);
                    section.ParentID = model.ParentID;
                    section.Sort = 0;
                    section.Status = 0;
                    section.SectionID = SectionBLL.Instance.Section_Insert(section);
                    result.data = section.SectionID;
                    OperateLogBLL.Instance.OperateLog_Insert(base.UID, base.UNICKNAME, "科室添加", section.SectionID.ToString() + section.SectionName);
                }
            }
            else
            {
                result.data = 3;
            }
            return Json(result);
        }


        #endregion

        public JsonResult GetBySectionID(int sectionid)
        {
            var result = new JsonResultObject(true);
            result.data = SectionBLL.Instance.Section_Get(sectionid);
            return Json(result);
        }
        public JsonResult GetByParentID(int parentid)
        {
            var result = new JsonResultObject(true);
            result.data = SectionBLL.Instance.Section_GetList(parentid);
            return Json(result);
        }
        public JsonResult GetBySectionName(string sectionname)
        {
            var result = new JsonResultObject(true);
            if (!string.IsNullOrEmpty(sectionname))
            {
                var list = SectionBLL.Instance.Section_GetList(sectionname.Trim()).Select(t => new { t.SectionName }).Distinct().ToList();
                result.data = list;
            }
            else
                result.data = "[]";
            return Json(result);
        }
    }
}