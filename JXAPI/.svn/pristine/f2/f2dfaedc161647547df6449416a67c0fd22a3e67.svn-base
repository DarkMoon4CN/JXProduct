using JXAPI.Component.IBLL;
using JXAPI.Component.Model;
using JXAPI.Component.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.Component.BLL
{
    public class UsersScoreMySqlBLL : IUsersRelatedMySqlBLL
    {
        #region 变量

        private static UsersScoreMySqlBLL _instance;
        private static readonly UsersScoreMySqlDAL dal = new UsersScoreMySqlDAL();
        private static readonly object _object = new object();

        #endregion

        private UsersScoreMySqlBLL() { }

        public static UsersScoreMySqlBLL Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock(_object)
                    {
                        if(_instance == null)
                        {
                            _instance = new UsersScoreMySqlBLL();
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
