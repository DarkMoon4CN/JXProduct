using JXAPI.Component.IBLL;
using JXAPI.Component.Model;
using JXAPI.Component.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace JXAPI.Component.BLL
{
    public class UsersMySqlBLL
    {
        public static UsersMySqlBLL Instance
        {
            get { return new UsersMySqlBLL(); }
        }

        private static readonly JXAPI.Component.SQLServerDAL.UsersMySqlDAL dal = new JXAPI.Component.SQLServerDAL.UsersMySqlDAL();

        /// <summary>
        /// 数据添加
        /// </summary>
        /// <param name="dTable">添加数据</param>
        /// <param name="tableName">表名称</param>
        /// <param name="errorCount">失败数量</param>
        /// <returns></returns>
        public bool AddUsers(DataTable dTable, string tableName, out int errorCount)
        {
            return dal.AddUsers(dTable, tableName, out errorCount);
        }

        public bool UpdateUsers(DataTable dTable, string tableName, out int errorCount)
        {
            return dal.UpdateUsers(dTable, tableName,out errorCount);
        }
    }
}
