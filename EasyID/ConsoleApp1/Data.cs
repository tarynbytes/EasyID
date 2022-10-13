using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace EasyID
{
    public abstract class Data
    {
        private static Driver _d;

        public static void SetDriver(Driver d)
        {
            _d = d;
        }
        protected Data(Driver d) { }
        public abstract void Process();
        //public abstract void Finalize();
        // "The.NET garbage collector implicitly manages the allocation and release
        // of memory for your objects. However, when your application encapsulates
        // unmanaged resources, such as windows, files, and network connections,
        // you should use finalizers to free those resources"


    }
}
