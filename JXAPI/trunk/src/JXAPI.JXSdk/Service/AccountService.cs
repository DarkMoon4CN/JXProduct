using JXAPI.JXSdk.Base;
using JXAPI.JXSdk.Request;
using JXAPI.JXSdk.Response;
using JXAPI.JXSdk.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.JXSdk.Service
{
    public class AccountService
    {
        private static DefaultJXClient client = new DefaultJXClient("user");
        private static string list = "admin/account/list";
        private static string resetPwd = "admin/account/reset_pwd";
        private static string updateBalance = "admin/balance/update";
        private static string updateStatus = "admin/account/status/update";
        private static string balanceList = "/admin/balance/record/list";
        private static string recordList="/admin/score/record/list";
        private static string register = "/admin/account/add";
        private static string isUserExist = "/user/isUserExist";
        public static AccountService Instance
        {
            get { return new AccountService(); }
        }

        /// <summary>
        ///  用户信息
        /// </summary>
        /// <param name="request">请求实体</param>
        /// <returns></returns>
        public AccountListResponse List(AccountListRequest request)
        {
            string postData = JsonHelper.GetJson(request);
            AccountListResponse response = client.Execute(request, list, postData);
            return response;
        }

        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="passportID">用户ID</param>
        /// <returns>密码重置以后为 123456</returns>
        public bool ResetPwd(string passportID) 
        {
            AccountResetPwdRequest  request=new AccountResetPwdRequest();
            request.passportID = passportID;
            string postData = JsonHelper.GetJson(request);
            CommonResponse response = client.Execute(request, resetPwd, postData);
            if (response != null && response.code ==0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 更改用户金额
        /// </summary>
        /// <param name="request">请求实体</param>
        /// <returns></returns>
        public bool UpdateBalance(AccountUpdateBalanceRequest request)
        {
            string postData = JsonHelper.GetJson(request);
            CommonResponse response = client.Execute(request, updateBalance, postData);
            if (response != null && response.code == 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool EnableStutus(AccountEnableStutusRequest request)
        {
            string postData = JsonHelper.GetJson(request);
            CommonResponse response = client.Execute(request, updateStatus, postData);
            if (response != null && response.code == 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///  用户余额查询
        /// </summary>
        /// <param name="request">请求实体</param>
        /// <returns></returns>
        public AccountBalanceListResponse BalanceList(AccountBalanceListRequest request)
        {
            string postData = JsonHelper.GetJson(request);
            AccountBalanceListResponse response = client.Execute(request, balanceList, postData);
            return response;
        }

        /// <summary>
        /// 用户积分查询
        /// </summary>
        /// <param name="request">请求实体</param>
        /// <returns></returns>
        public AccountRecordListResponse RecordList(AccountRecordListRequest request) 
        {
            string postData = JsonHelper.GetJson(request);
            AccountRecordListResponse response = client.Execute(request, recordList, postData);
            return response;
        }

        /// <summary>
        /// 客服后台注册 
        /// </summary>
        /// <param name="request">请求实体 user 和 receiver</param>
        /// <returns>成功：uid,失败 0</returns>
        public int  Register(RegisterRequest request)
        {
            string postData = JsonHelper.GetJson(request);
            RegisterResponse response = client.Execute(request, register, postData);
            if (response.code == 0)
            {
                return response.data.passportID;
            }
            return 0;
        }

        /// <summary>
        /// 验证客户登录名或手机号是否存在
        /// </summary>
        /// <param name="userNameOrPhone">登录名 或手机号</param>
        /// <returns></returns>
        public bool IsUserExist(string userNameOrPhone) 
        {
            UserExistRequest request = new UserExistRequest() 
            {
                userName = userNameOrPhone
            };
            string postData = JsonHelper.GetJson(request);
            UserExistResponse response = client.Execute(request, isUserExist, postData);
            if (response.code == 0)
            {
                return response.success;
            }
            return false;
        }
    }


}
