using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JXAPI.ConsoleAppPush
{
    public class Program
    {
        static void Main(string[] args)
        {
            Push push = new Push();
            System.Threading.Thread myThrad = new System.Threading.Thread(push.PushStart);
            myThrad.IsBackground = false;
            myThrad.Start();
        }
    }
}

