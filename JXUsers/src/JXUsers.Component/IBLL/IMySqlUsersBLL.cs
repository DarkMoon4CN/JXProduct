using JXUsers.Component.Model.ResultModel;
using JXUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXUsers.Component.IBLL
{
    public interface IMySqlUsersBLL
    {
        OperationResult<IList<UsersInfo>> Users_GetUserByParms(string mobile, string phone, string trueName, int userID, int isJoinMoblie = 1);
    }
}
