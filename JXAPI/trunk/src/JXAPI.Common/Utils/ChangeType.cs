using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JXAPI.Common.Utils
{
    public class ChangeType
    {
        public static DbType MappingDbType(Type type)
        {
            if (type.Equals(typeof(System.String)))
                return DbType.String;
            if (type.Equals(typeof(System.Int32)))
                return DbType.Int32;
            if (type.Equals(typeof(System.DateTime)))
                return DbType.DateTime;
            if (type.Equals(typeof(System.Decimal)))
                return DbType.Decimal;
            if (type.Equals(typeof(System.Byte)))
                return DbType.Int16;
            return DbType.String;
        }
    }
}
