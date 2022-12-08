using System.Text.RegularExpressions;

namespace EasyID.Data
{
    public class IPv4 : DataTemplate
    {
        // TODO: Decimal notation, hex notiation
        private Driver _driver;
        private List<int> _lengthList = new List<int>();
        private static string _content = "numersymbolic";
        private List<string> _symList = new List<string> { "..." };
        private string _letterList = "";
        private int[] _octets = new int[4];


        public IPv4(Driver d) : base(d)
        {
            _driver = d;
        }

        // Splits the input into four octets and checks that each are bound between 0 and 255
        public int[] Octets
        {
            get
            {
                try
                {
                    string[] splits = _driver.Input.Split('.');
                    int[] octets = new int[4];

                    for (int i = 0; i < 4; i++)
                    {
                        _octets[i] = Int32.Parse(splits[i]);
                    }
                    if ( _octets[0] > 255 || _octets[1] > 255 || _octets[2] > 255 || _octets[3] > 255 )
                    {
                        return null;
                    }
                    else
                    {
                        return _octets;
                    }
                }
                catch { return null;  }
            }
            
}

        public int? Length
        {
            get
            {
                for (int i = 7; i <= 15; i++)
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
        public string LetterList
        {
            get
            {
                if (_driver.LetterList == _letterList)
                {
                    return _letterList;
                }
                return null;
            }
        }
        public override string Process()
        {
            string returnString = "";


            string classString = IPClass(this._octets);
            if (classString != "Loopback")
            {
                returnString += String.Format("This address is in Class {0} of the IPv4 class hiearchy.", classString);
            }
            else
            {
                returnString += "This is a loopback address.";
            }

            if (classString == "A" || classString == "B" || classString == "C")
            {
                returnString += PrivateIP(this._octets);
            }

            return returnString;

        }


        // Returns whether or not the IP address is private or public
        private string PrivateIP(int[] octets)
        {
            if ((octets[0] == 10) || (octets[0] == 192 && octets[1] == 168) || (octets[0] == 172 && (octets[1] >= 16 && octets[1] <= 31)))
            {
                return "<br>It is also a private, non-routable address.";
            }
            else
                return "";
        }

        // Returns the hiearchical class of the IP address
        private string IPClass(int[] octets)
        {

            if(octets[0] >= 1 && octets[0] <= 126)
            {
                return "A";
            }
            else if (octets[0] == 127)
            {
                return "Loopback";
            }
            else if (octets[0] >= 128 && octets[0] <= 191)
            {
                return "B";
            }
            else if (octets[0] >= 192 && octets[0] <= 223)
            {
                return "C";
            }
            else if (octets[0] >= 224 && octets[0] <= 239)
            {
                return "D";
            }
            else if (octets[0] >= 240 && octets[0] <= 255)
            {
                return "E";
            }
            else { return ""; }
        }
        public override string ToString()
        {
            return "IPv4 Address";
        }
    }
}