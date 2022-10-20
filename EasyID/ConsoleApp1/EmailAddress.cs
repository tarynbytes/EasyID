using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EasyID
{
    public class EmailAddress : Data
    {
        private Driver _driver;
        private static int _length = 3;
        private static string _content = "alphanumeric";

        public EmailAddress(Driver d) : base(d)
        {
            _driver = d;
        }

        public int? Length
        {
            get
            {
                if (_driver.Length >= _length)
                {
                    return _length;
                }
                return null;
            }
        }

        public string? Content
        {
            get
            {
                if (_driver.Content == _content && IsValid(_content))
                {
                    return _content ;
                }
                return null;
            }
        }

        public override void Process()
        {
            Console.WriteLine($"{_content} is" + (IsValid(_content) ? " a valid" : " an invalid") + " email address.");
        }


        private static bool IsValid(string email)
        {
            string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov|edu)$";

            return Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
        }

    }
}


//To-Do: Test Module 