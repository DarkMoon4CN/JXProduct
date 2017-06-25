using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JXAPI.Common.Utils
{
    public class CollectionHelper
    {
        private CollectionHelper()
        { 
        }
        #region 帮助函数

        //public static T DoTransferToModel<T>(IDataReader dr)
        //{
        //    T model = default(T);
        //    int count = dr.FieldCount;
        //    if (count > 0)
        //    {
        //        model = Activator.CreateInstance<T>();
        //        PropertyInfo[] property_lst = model.GetType().GetProperties();
        //        foreach (PropertyInfo property in property_lst)
        //        {
        //            for (int i = 0; i < count; i++)
        //            {
        //                if (!Convert.IsDBNull(dr[i]))//判断值是否为空  
        //                {
        //                    string name = dr.GetName(i).ToUpper();//字段名  
        //                    if (name.Equals(property.Name.ToUpper()))//判断字段名是否和model里的相等  
        //                    {
        //                        try
        //                        {
        //                            property.SetValue(model, dr.GetValue(i), null);//为model赋值  
        //                            break;
        //                        }
        //                        catch { }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return model;
        //}

        public static T DoTransferToModel<T>(IDataReader dr)
        {
            T model = default(T);
            int count = dr.FieldCount;
            if (count > 0)
            {
                model = Activator.CreateInstance<T>();
                PropertyInfo[] property_lst = model.GetType().GetProperties();
                foreach (PropertyInfo property in property_lst)
                {
                    if (!Convert.IsDBNull(dr[property.Name]))//判断值是否为空  
                    {
                        try
                        {
                            property.SetValue(model, dr[property.Name], null);//为model赋值  
                        }
                        catch
                        {

                        }
                    }
                }
            }
            return model;
        }

        /// <summary>
        /// 创建一个6字节长度的随机数字
        /// </summary>
        /// <returns></returns>
        public static string GenerateRandomDigit(int n)
        {
            Random ra = new Random(DateTime.Now.Millisecond + n);
            int rnum = ra.Next(100000, 999999);
            return rnum.ToString();
        }

        /// <summary>
        /// 生成随机3位纯字母字符串
        /// </summary>
        /// <returns></returns>
        public static string GenerateRandomChar(int n)
        {
            string rstr = string.Empty;
            Random ra = new Random(n);
            for (int i = 0; i < 3; i++)
            {
                int index = ra.Next(0, letterArry.Length - 1);
                rstr += letterArry[index];
            }
            return rstr;
        }

        /// <summary>
        /// 创建随机匿名字符串
        /// </summary>
        /// <param name="randomDigit"></param>
        /// <param name="randomStr"></param>
        /// <returns></returns>
        public static string CreateRandomNikeNameStr(int n)
        {
            string randomDigit = GenerateRandomDigit(n);
            string randomStr = GenerateRandomChar(n);
            return string.Format("jx{0}{1}", randomDigit, randomStr);
        }

        /// <summary>
        /// 在100张图片中随机获取一张当作用户头像
        /// </summary>
        /// <returns></returns>
        public static string CreateRandomAvatar()
        {
            Random ra = new Random();
            int index = ra.Next(0, AvatarArry.Length - 1);
            return AvatarArry[index];
        }

        private static string[] AvatarArry = new string[]{ "2bd3/d98/a743/94b6e5297_O.jpg",
            "e27d/236/594b/9478aebb5_O.jpg", "a27b/69e/8248/64753dade_O.jpg", "2959/6ba/274d/24ed583d6_O.jpg", 
            "9832/5bd/7e42/34893a6be_O.jpg", "da32/365/9d46/54b8e7a57_O.jpg", "b2de/638/5a4e/249b27d8e_O.jpg", 
            "5ae3/e76/5247/84b8d9a62_O.jpg", "8263/62e/8949/a4b57ad57_O.jpg", "673b/96d/8a45/64e2357a8_O.jpg", 
            "3bd9/5b8/e74a/9e4da632e_O.jpg", "2bd3/d98/a743/994b6e529_O.jpg", "e27d/236/594b/a9478aebb_O.jpg", 
            "a27b/69e/8248/a64753dad_O.jpg", "2959/6ba/274d/824ed583d_O.jpg", "9832/5bd/7e42/534893a6b_O.jpg",
            "da32/365/9d46/254b8e7a5_O.jpg", "b2de/638/5a4e/d249b27d8_O.jpg", "5ae3/e76/5247/b84b8d9a6_O.jpg", 
            "8263/62e/8949/6a4b57ad5_O.jpg", "673b/96d/8a45/964e2357a_O.jpg", "3bd9/5b8/e74a/7e4da632e_O.jpg", 
            "2bd3/d98/a743/794b6e529_O.jpg", "e27d/236/594b/99478aebb_O.jpg", "a27b/69e/8248/864753dad_O.jpg", 
            "2959/6ba/274d/924ed583d_O.jpg", "9832/5bd/7e42/734893a6b_O.jpg", "da32/365/9d46/354b8e7a5_O.jpg",
            "b2de/638/5a4e/a249b27d8_O.jpg", "5ae3/e76/5247/684b8d9a6_O.jpg", "8263/62e/8949/9a4b57ad5_O.jpg", 

            "673b/96d/8a45/a64e2357a_O.jpg", "3bd9/5b8/e74a/ae4da632e_O.jpg", "2bd3/d98/a743/694b6e529_O.jpg", 
            "e27d/236/594b/39478aebb_O.jpg", "a27b/69e/8248/664753dad_O.jpg", "2959/6ba/274d/624ed583d_O.jpg", 
            "9832/5bd/7e42/234893a6b_O.jpg", "da32/365/9d46/954b8e7a5_O.jpg", "b2de/638/5a4e/6249b27d8_O.jpg", 
            "5ae3/e76/5247/d84b8d9a6_O.jpg", "8263/62e/8949/5a4b57ad5_O.jpg", "673b/96d/8a45/e64e2357a_O.jpg", 
            "3bd9/5b8/e74a/be4da632e_O.jpg", "2bd3/d98/a743/a94b6e529_O.jpg", "e27d/236/594b/89478aebb_O.jpg",
            "a27b/69e/8248/364753dad_O.jpg", "2959/6ba/274d/224ed583d_O.jpg", "9832/5bd/7e42/e34893a6b_O.jpg",
            "da32/365/9d46/854b8e7a5_O.jpg", "b2de/638/5a4e/e249b27d8_O.jpg", "5ae3/e76/5247/584b8d9a6_O.jpg", 
            "8263/62e/8949/ba4b57ad5_O.jpg", "673b/96d/8a45/b64e2357a_O.jpg", "3bd9/5b8/e74a/6e4da632e_O.jpg", 
            "2bd3/d98/a743/594b6e529_O.jpg", "e27d/236/594b/79478aebb_O.jpg", "a27b/69e/8248/e64753dad_O.jpg", 
            "2959/6ba/274d/324ed583d_O.jpg", "9832/5bd/7e42/334893a6b_O.jpg", "da32/365/9d46/e54b8e7a5_O.jpg", 

            "b2de/638/5a4e/5249b27d8_O.jpg", "5ae3/e76/5247/984b8d9a6_O.jpg", "8263/62e/8949/ea4b57ad5_O.jpg", 
            "673b/96d/8a45/d64e2357a_O.jpg", "3bd9/5b8/e74a/8e4da632e_O.jpg", "2bd3/d98/a743/294b6e529_O.jpg", 
            "e27d/236/594b/d9478aebb_O.jpg", "a27b/69e/8248/964753dad_O.jpg", "2959/6ba/274d/724ed583d_O.jpg", 
            "9832/5bd/7e42/d34893a6b_O.jpg", "da32/365/9d46/554b8e7a5_O.jpg", "b2de/638/5a4e/9249b27d8_O.jpg", 
            "5ae3/e76/5247/784b8d9a6_O.jpg", "8263/62e/8949/7a4b57ad5_O.jpg", "673b/96d/8a45/764e2357a_O.jpg", 
            "3bd9/5b8/e74a/3e4da632e_O.jpg", "2bd3/d98/a743/d94b6e529_O.jpg", "e27d/236/594b/b9478aebb_O.jpg", 
            "a27b/69e/8248/564753dad_O.jpg", "2959/6ba/274d/d24ed583d_O.jpg", "9832/5bd/7e42/834893a6b_O.jpg", 
            "da32/365/9d46/a54b8e7a5_O.jpg", "b2de/638/5a4e/2249b27d8_O.jpg", "5ae3/e76/5247/384b8d9a6_O.jpg", 
            "8263/62e/8949/8a4b57ad5_O.jpg", "673b/96d/8a45/564e2357a_O.jpg", "3bd9/5b8/e74a/2e4da632e_O.jpg", 
            "2bd3/d98/a743/e94b6e529_O.jpg", "e27d/236/594b/59478aebb_O.jpg", "a27b/69e/8248/d64753dad_O.jpg", 

            "2959/6ba/274d/524ed583d_O.jpg", "9832/5bd/7e42/634893a6b_O.jpg", "da32/365/9d46/b54b8e7a5_O.jpg", 
            "b2de/638/5a4e/3249b27d8_O.jpg", "5ae3/e76/5247/884b8d9a6_O.jpg", "8263/62e/8949/2a4b57ad5_O.jpg", 
            "673b/96d/8a45/364e2357a_O.jpg", "3bd9/5b8/e74a/5e4da632e_O.jpg", "3bd9/5b8/e74a/594da632e_O.jpg"};

        private static string[] letterArry = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

        #endregion

    }
}
