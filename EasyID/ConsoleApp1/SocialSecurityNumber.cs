using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EasyID
{
    public class SocialSecurityNumber : Data
    {
        private Driver _driver;
        private static int _length = 9;
        private static string _content = "numeric";

        public SocialSecurityNumber(Driver d) : base(d)
        {
            _driver = d;
        }

        public int ?Length
        {
            get
            {
                if (_driver.Length == _length)
                {
                    return _length;
                }
                return null;
            }
        }

        public string ?Content
        {
            get
            {
                if (_driver.Content == _content)
                {
                    return _content;
                }
                return null;
            }
        }

        public override void Process()
        {
            Console.WriteLine("  ## Additional interesting details here about the SSN. ##");
        }

        //public override void Finalize()
        //{
            // Cleanup
        //}
    }
}
