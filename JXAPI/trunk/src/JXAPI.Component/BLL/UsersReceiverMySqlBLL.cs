using JXAPI.Component.IBLL;
using JXAPI.Component.Model;
using JXAPI.Component.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JXAPI.Component.BLL
{
    public class UsersReceiverMySqlBLL : IUsersRelatedMySqlBLL
    {
        #region 变量

        private static UsersReceiverMySqlBLL _instance;
        private static readonly UsersReceiverMySqlDAL dal = new UsersReceiverMySqlDAL();
        private static readonly object _object = new object();

        #endregion

        private UsersReceiverMySqlBLL() { }

        public static UsersReceiverMySqlBLL Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock(_object)
                    {
                        if(_instance == null)
                        {
                            _instance = new UsersReceiverMySqlBLL();
                        }
                    }
                }
                return _instance;
            }
        }

        #region CURD

        public int GetMaxID()
        {
            return dal.GetMaxUsersReceiverID();
        }

        public bool Add(DataTable table, out int errorCount)
        {
            return dal.AddUsersReceiver(table, out errorCount);
        }

        public bool Update(DataTable table, out int errorCount)
        {
            return dal.UpdateUsersReceiver(table, out errorCount);
        }

        #endregion
    }
}
