using JXAPI.JXSdk.Response;
using JXUsers.Component.BLL;
using JXUsers.Component.IBLL;
using JXUsers.Component.Model.ResultModel;
using JXUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JXUsers.AdminUI.Controllers
{
    public class CRMController : Controller //JXAdminAuthBase.BaseController
    {
        // CRM：Customer Relationship Management 

        private readonly IMySqlUsersBLL usersBLL;
        public CRMController(IMySqlUsersBLL usersBLL)
        {
            this.usersBLL = usersBLL;
        }

        #region 查找用户
        public ActionResult FindUsers(string urlCalling, string mobile, string phone, string trueName, int userID = 0, int isJoinMoblie = 1)
        {
            if (!string.IsNullOrEmpty(urlCalling))//判定接入的是手机还是固话
            {
                if (StringUtility.ValidMobileFormat(urlCalling))
                {
                    mobile = urlCalling;
                }
                else
                {
                    phone = urlCalling;
                }
            }

            if (phone == null && mobile == null && trueName == null && userID == 0)//空提交
            {
                return View();
            }
            else if (!string.IsNullOrEmpty(phone))//手机赋值给电话
            {
                if (StringUtility.ValidMobileFormat(phone))
                {
                    mobile = phone;
                }
            }else if (!string.IsNullOrEmpty(mobile))//电话赋值给手机
            {
                if (!StringUtility.ValidMobileFormat(mobile))
                {
                    phone = mobile;
                }
            }
            OperationResult<IList<UsersInfo>> usersInfoResult=
                   usersBLL.Users_GetUserByParms(mobile, phone, trueName, userID, isJoinMoblie);
            if (usersInfoResult.ResultType == OperationResultType.Success) 
            {
                IList<UsersInfo> usersInfoList = usersInfoResult.AppendData;

                if (usersInfoList != null && usersInfoList.Count == 1) //单个用户去用户信息
                {
                    return Redirect("/CRM/CustomerDetail?uid=" + usersInfoList[0].UID);
                }
                else if(usersInfoList.Count== 0)//没有用户去用户注册
                {
                    return Redirect("/CRM/UsersAdd?urlCalling=" + urlCalling);
                }
            }
            return View();
        }
        #endregion 

        #region 客户详情
        public ActionResult CustomerDetail(string calling) 
        {
            ViewBag.moblie = string.Empty;
            ViewBag.phone = string.Empty;
            if (!string.IsNullOrEmpty(calling))//判定接入的是手机还是固话
            {
                string urlCalling = SqlUtil.ReplaceInjection(Request["urlCalling"].ToString());
                if (StringUtility.ValidMobileFormat(urlCalling))
                {
                    ViewBag.moblie = urlCalling;
                }
                else
                {
                    ViewBag.phone = urlCalling;
                }
            }
            return View();
        }
        #endregion

        #region 编辑客户信息
        public ActionResult CustomerUpdate() 
        {

            return View();
        }
        #endregion

        #region 添加用户
        public ActionResult UsersAdd() 
        {
            return View();
        }
        #endregion

        #region 绑定或解绑客户优惠券
        public JsonResult ReleaseOrBindCoupon(string cardNo, string cardPass, decimal remainingsum, int usersId, int status = 1)
        {
            var result = new JsonResultObject();
            if (status == 1) //绑定
            {
                if (cardNo.Length < 1 || cardPass.Length < 1)
                {
                    result.msg = "卡号或密码不可为空！";
                    result.status = false;
                }
                else 
                {
                    JXAPI.JXSdk.Service.CouponService cs = new JXAPI.JXSdk.Service.CouponService();
                    JXAPI.JXSdk.Request.CouponBindRequest request= new JXAPI.JXSdk.Request.CouponBindRequest();
                    request.userId = usersId;
                    request.code = cardNo;
                    request.pass = cardPass;
                    CommonResponse  response=cs.Bind(request);
                    if (response.success)
                    {
                        result.msg = "优惠券绑定成功！";
                        result.status = true;
                    }
                    else
                    {
                        result.msg = "优惠券绑定失败！原因：" + response.message;
                        result.status = false;
                    }
                }

            }
            else //解绑
            {
                if (cardNo.Length < 1)
                {
                    result.msg = "卡号不可为空！";
                    result.status = false;
                }
                else 
                {
                    JXAPI.JXSdk.Service.CouponService cs = new JXAPI.JXSdk.Service.CouponService();
                    JXAPI.JXSdk.Request.ReleaseBindRequest request = new JXAPI.JXSdk.Request.ReleaseBindRequest();
                    request.uid = 1; //UID;
                    request.code = cardNo;
                    request.remainingSum = remainingsum;
                    request.userId = usersId;
                    CommonResponse response = cs.ReleaseBind(request);
                    if (response.success)
                    {
                        result.msg = "优惠券解除绑定成功！";
                        result.status = true;
                    }
                    else
                    {
                        result.msg = "购物卡解除绑定失败！原因" + response.message;
                        result.status = false;
                    }
                }
            }
            return Json(result);
        }
        #endregion

    }
}
