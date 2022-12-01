namespace EasyID.Data
{
    public class CreditCardNumber : DataTemplate
    {
        private Driver _driver;
        private List<int> _lengthList = new List<int> { 15, 16, 17, 19 };
        private List<string> _indexList = new List<string> { "NNNNSNNNNSNNNNSNNNN", "NNNNNNNNNNNNNNNN", "NNNNSNNNNNNSNNNNN", "NNNNNNNNNNNNNNN" };
        private List<string> _contentList = new List<string> { "numeric", "numersymbolic" };
        private List<string> _symList = new List<string> { "", "  ", "--", "---", "   " };
        private string _letterList = "";

        public CreditCardNumber(Driver d) : base(d)
        {
            _driver = d;
        }

        public int? Length
        {
            get
            {
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
        public string IndexList
        {
            get
            {
                foreach (string _s in _indexList)
                {
                    if (_s == _driver.IndexList)
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

            if (!Valid(_driver.Input))
            {
                returnString += "However, this seems to be an invalid Credit Card Number...<br>";
            }

            else
            {
                returnString += IndustryIdentifier(_driver.Input);
                //returnString += BankIdentifier(_driver.Input);        // TODO
                //returnString += AccountIdentifier(_driver.Input);     // TODO
            }
            return returnString;
        }

        private string IndustryIdentifier(string s)
        {
            s.Trim(); // Removes white space if any
            string numbers = s.Replace("-", string.Empty); // Removes dashes if any
            if (numbers.Substring(0, 1) == "2" || numbers.Substring(0, 1) == "5")
            {
                return "It is likely a Mastercard. Otherwise, it could be a card associated with an air travel service.";
            }
            else if (numbers.Substring(0, 1) == "3")
            {
                return "It is likely an American Express.";
            }
            else if (numbers.Substring(0, 1) == "4")
            {
                return "It is likely a Visa.";
            }
            else if (numbers.Substring(0, 1) == "6")
            {
                return "It is likely a Discover.";
            }
            else if (numbers.Substring(0, 1) == "7")
            {
                return "It is likely a card associated with the petroleum industry.";
            }
            else if (numbers.Substring(0, 1) == "8")
            {
                return "It is likely a card associated with healthcare or telecommunication services.";
            }
            else if (numbers.Substring(0, 1) == "7")
            {
                return "It is likely a card open for miscellaneous use by the government.";
            }
            else { return ""; }
        }

        private string BankIdentifier(string s)
        {
            return "";
        }
        private string AccountIdentifier(string s)
        {
            return "";
        }
        private static bool Valid(string s)
        {
            s.Trim(); // Removes white space if any
            string numbers = s.Replace("-", string.Empty); // Removes dashes if any

            System.Collections.Generic.IEnumerable<char> rev = numbers.Reverse();
            int sum = 0, i = 0;
            foreach (char c in rev)
            {
                if (c < '0' || c > '9')
                    return false;
                int tmp = c - '0';
                if ((i & 1) != 0)
                {
                    tmp <<= 1;
                    if (tmp > 9)
                        tmp -= 9;
                }
                sum += tmp;
                i++;
            }
            return ((sum % 10) == 0);
        }

        public override string ToString()
        {
            return "Credit Card Number";
        }
    }
}