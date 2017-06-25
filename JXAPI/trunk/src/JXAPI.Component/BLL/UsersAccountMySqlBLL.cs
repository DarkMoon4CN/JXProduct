using JXAPI.Component.IBLL;
using JXAPI.Component.Model;
using JXAPI.Component.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.Component.BLL
{
    public class UsersAccountMySqlBLL : IUsersRelatedMySqlBLL
    {
        #region 变量

        private static UsersAccountMySqlBLL _instance;
        private static readonly UsersAccountMySqlDAL dal = new UsersAccountMySqlDAL();
        private static readonly object _object = new object();

        #endregion

        private UsersAccountMySqlBLL() { }

        public static UsersAccountMySqlBLL Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock(_object)
                    {
                        if(_instance == null)
                        {
                            _instance = new UsersAccountMySqlBLL();
                        }
                    }
                }
                return _instance;
            }
        }

        #region CURD

        public int GetMaxID()
        {
            throw new NotImplementedException();
        }

        public bool Add(System.Data.DataTable table, out int errorCount)
        {
            throw new NotImplementedException();
        }

        public bool Update(System.Data.DataTable table, out int errorCount)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
