using JXAPI.Component.IBLL;
using JXAPI.Component.Model;
using JXAPI.Component.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.Component.BLL
{
    public class UsersBaseMySqlBLL : IUsersRelatedMySqlBLL
    {
        #region 变量

        private static UsersBaseMySqlBLL _instance;
        private static readonly UsersBaseMySqlDAL dal = new UsersBaseMySqlDAL();
        private static readonly object _object = new object();

        #endregion

        private UsersBaseMySqlBLL() { }

        public static UsersBaseMySqlBLL Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_object)
                    {
                        if (_instance == null)
                        {
                            _instance = new UsersBaseMySqlBLL();
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
