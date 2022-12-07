using EasyID.Data;


namespace EasyID
{
    public class USPSTrackingNumber : DataTemplate
    {
        // TODO: error check last digit
        private Driver _driver;
        private List<int> _lengthList = new List<int> { 10, 13, 20, 22 };
        private List<int> _letterListLength = new List<int> { 0, 1, 4 };
        private List<string> _indexList = new List<string> 
        { "LNNNSNNNNSNN", "NNSNNNSNNNSNN", "NNNNSNNNNSNNNNSNNNNSNNNN", "NNNNSNNNNSNNNNSNNNNSNNNNSNN"}; 
        private List<string> _noWhiteSpaceIndexList = new List<string> { "NNNNNNNNNN", "LNNNNNNNNNN", "LLNNNNNNNNNLL", "NNNNNNNNNNNNNNNNNNNN", "NNNNNNNNNNNNNNNNNNNNNN" }; // Used on nowhitespace input
        private List<string> _contentList = new List<string> { "numeric", "alphanumeric" };
        private string _symbols = "";

        public USPSTrackingNumber(Driver d) : base(d)
        {
            _driver = d;
        }

        public int? Length
        {
            get
            {
                foreach (int _length in _lengthList)
                {
                    if (_length == _driver.NoWhiteSpace.Length)
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
                if (_driver.SymList.Length != 0)
                {
                    foreach (string _s in _indexList)
                    {
                        if (_s == _driver.IndexList)
                        {
                            return _s;
                        }
                    }
                }
                else
                {
                    return "";
                }
                return null;
            }
                
        }

        // Checks that the number of letters in the input are either 0, 1, or 4.
        // If 4, it needs to end in "US".
        public int? LetterListLength
        {
            get
            {
                foreach (int _length in _letterListLength)
                {
                    if (_length == _driver.NoWhiteSpaceLetterList.Length)
                    {
                        if (_driver.NoWhiteSpaceLetterList.Length == 4)
                        {
                            if (_driver.NoWhiteSpaceLetterList.Substring(2, 2) == "US")
                            {
                                return _length;
                            }
                            else { return null; }
                        }
                        else
                        {
                            return _length;
                        }
                    }
                }
                return null;
            }
        }

        public string NoWhiteSpaceIndexList
        {
            get
            {
                
                foreach (string _s in _noWhiteSpaceIndexList)
                {
                    if (_s == _driver.NoWhiteSpaceIndexList)
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
                    if (_content == _driver.NoWhiteSpaceContent)
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
                if (_driver.NoWhiteSpaceSymList == _symbols)
                {
                    return _symbols;
                }
                return null;
            }
        }

        public override string Process()
        {
            string returnString = "";
            returnString += GetMailType();
            return returnString;
        }

        private string GetMailType()
        {
            string s = "The type of mail is identified as ";
            if (this.NoWhiteSpaceIndexList == "NNNNNNNNNN")
            {
                s += "Global Express Guaranteed®";
            }
            else if (this.NoWhiteSpaceIndexList == "LNNNNNNNNNN")
            {
                s += "Collect on Delivery";

            }
            else if (this.NoWhiteSpaceIndexList == "LLNNNNNNNNNLL")
            {
                s += "either Priority Mail Express International™, Priority Mail Express™, <br>" +
                    "Priority Mail International®, or Registered Mail™.";

            }
            else if (this.NoWhiteSpaceIndexList == "NNNNNNNNNNNNNNNNNNNN")
            {
                s += "either Certified Mail®, Priority Mail®, Signature Confirmation™, or USPS Tracking™.";

            }
            else if (this.NoWhiteSpaceIndexList == "NNNNNNNNNNNNNNNNNNNNNN")
            {
                s += "either Certified Mail®, Collect on Delivery, Priority Mail Express™, Priority Mail®, <br>" +
                    "Registered Mail™, Signature Confirmation™, or USPS Tracking™.";

            }
            else { ; }

            return s;
        }


        public override string ToString()
        {
            return "U.S Postal Service Tracking Number";
        }
    }
}
