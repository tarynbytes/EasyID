using System.Text.RegularExpressions;

namespace EasyID.Data
{
    public class IPv6 : DataTemplate
    {
        private Driver _driver;
        private List<int> _lengthList = new List<int>();
        private static string _content = "alphanumersymbolic";
        private List<string> _symList = new List<string> { "::", ":::", "::::", ":::::", ":::::::", };
        private Dictionary<string, string> _hex = new Dictionary<string, string> { };

        public IPv6(Driver d) : base(d)
        {
            _driver = d;
        }

        public int? Length
        {
            get
            {
                for (int i = 7; i <= 39; i++)
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

        public Dictionary<string, string> Hex
        {
            get
            {
                _hex[_driver.Input] = "true";
                if (_driver.Hex[_driver.Input] == "true")
                {
                    return _hex;
                }
                else
                {
                    return null;
                }
            }
        }

        public string? Content
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
        public string SymList
        {
            get
            {
                foreach (string _s in _symList)
                {
                    if (_s == _driver.SymList)
                    {
                        return _s;
                    }
                }
                return null;
            }
        }
        public override string Process()
        {
            string returnString = "";

            //TODO
            returnString += "## Additional interesting details here about the IPv6 Address go here. ##";
            // call Class()

            return returnString;
        }

        public override string ToString()
        {
            return "IPv6 Address";
        }
    }
}