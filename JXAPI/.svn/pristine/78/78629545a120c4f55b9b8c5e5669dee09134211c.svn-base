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
    public class UsersBLL 
    {
        public static UsersBLL Instance
        {
            get { return new UsersBLL(); }
        }

        private static readonly JXAPI.Component.SQLServerDAL.UsersDAL dal = new JXAPI.Component.SQLServerDAL.UsersDAL();

        /// <summary>
        /// mssql 数据查询
        /// </summary>
        /// <param name="pageSize">数量</param>
        /// <param name="id">ID</param>
        /// <param name="tableName">表名称</param>
        /// <returns></returns>
        public DataTable GetList(int pageSize, int id, string tableName)
        {
            return dal.GetList(pageSize, id, tableName);
        }

        public DataTable GetUpdateList(int MaxID, int NowID, string updateTime, int pageSize, string tableName)
        {
            return dal.GetUpdateList(MaxID, NowID, updateTime, pageSize, tableName);
        }
    }
}
