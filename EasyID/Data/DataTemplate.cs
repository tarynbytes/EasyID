using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using EasyID.Data;

namespace EasyID
{
    public abstract class DataTemplate
    {
        private static Driver _d;

        public static void SetDriver(Driver d)
        {
            _d = d;
        }
        protected DataTemplate(Driver d) { }
        public abstract string Process();
        

    }
}
