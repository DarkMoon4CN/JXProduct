using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JXAPI.ConsoleAppSpider
{
    class Program
    {
        static void Main(string[] args)
        {
            Spider spider = new Spider();
            System.Threading.Thread myThrad = new System.Threading.Thread(spider.Init);
            myThrad.IsBackground = false;
            myThrad.Start();
        }
    }
}
