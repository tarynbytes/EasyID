using EasyID.Data;
using System.Text.RegularExpressions;

namespace EasyID
{
    public class EmailAddress : DataTemplate
    {
        private Driver _driver;

        private List<int> _lengthList = new List<int>(); // between 3 - 256 per RFC
        private List<string> _contentList = new List<string> { "alphasymbolic", "numersymbolic", "alphanumersymbolic" };
        private int _atSymbolCount = 1;
        
        public EmailAddress(Driver d) : base(d)
        {
            _driver = d;
        }

        public int? Length
        {
            get
            {
                for (int i = 3; i <= 256; i++)
                {
                    _lengthList.Add(i);
                }
                foreach (int _length in _lengthList)
                {
                    if (_length == _driver.Length)
                    {
                        return _length;
                    }
                }

                return null;
            }
        }

        public string? Content
        {
            get
            {
                foreach (string _content in _contentList)
                {
                    if (_content == _driver.Content)
                    {
                        return _content;
                    }
                }
                return null;
            }
        }
        public int? AtSymbolCount
        {
            get
            {
                if (_driver.AtSymbolCount == _atSymbolCount)
                {
                    return _atSymbolCount;
                }
                return null;
            }
        }

        public override string Process()
        {
            string returnString = "";

            if (!Valid(_driver.Input))
            {
                returnString += "However, this seems to be an invalid Email Address...<br>";
            }

            else {; }
            return returnString;
        }

        private static bool Valid(string email)
        {
            // Email Regex 
            Regex validateEmailRegex = new Regex("^\\S+@\\S+\\.\\S+$");
            return validateEmailRegex.IsMatch(email);
        }

        public override string ToString()
        {
            return "Email Address";
        }
    }
}
