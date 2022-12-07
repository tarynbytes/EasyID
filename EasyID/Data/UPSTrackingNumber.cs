using EasyID.Data;


namespace EasyID
{
    public class UPSTrackingNumber : DataTemplate
    {
        // TODO: the other types of UPS tracking numbers (MI, international, etc.)
        private Driver _driver;
        private List<int> _lengthList = new List<int> { 9, 11, 12, 18 };
        private List<string> _letterList = new List<string> { "", "T", "Z" };
        private List<string> _indexList = new List<string> { "NNNNNNNNN", "LNNNNNNNNNN", "NNNNNNNNNNNN", "NLNNNNNNNNNNNNNNNN"};
        private List<string> _contentList = new List<string> { "numeric", "alphanumeric" };
        private string _symbols = "";

        public UPSTrackingNumber(Driver d) : base(d)
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
                foreach (string _s in _letterList)
                {
                    if (_s == _driver.LetterList)
                    {
                        return _s;
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

        public string Symbols
        {
            get
            {
                if (_driver.SymList == _symbols)
                {
                    return _symbols;
                }
                return null;
            }
        }

        public override string Process()
        {
            string returnString = "";
            returnString += "";
            return returnString;
        }

        public override string ToString()
        {
            return "UPS Tracking Number";
        }
    }
}
