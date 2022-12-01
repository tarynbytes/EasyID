using System.Text.RegularExpressions;

namespace EasyID.Data
{
    public class WindowsGUID : DataTemplate
    {
        private Driver _driver;
        private List<int> _lengthList = new List<int> { 32, 36, 38 }; //https://stackoverflow.com/questions/968175/what-is-the-string-length-of-a-guid
        private static string _content = "alphanumersymbolic";
        private List<string> _symList = new List<string> { "----", "{----}", "(----)" };

        public WindowsGUID(Driver d) : base(d)
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

            if (!Valid(_driver.Input)) { returnString += "This seems to be an invalid GUID...<br>"; }

            returnString += "## Additional interesting details here about the GUID go here. ##";
            //call User()

            return returnString;
        }

        private static bool Valid(string guid)
        {
            // GUID Regex 
            // GUID is an acronym for Globally Unique Identifier and used for resource identification. The term is generally used instead of UUID when working with Microsoft technologies.
            // A GUID is a 128-bit value consisting of one group of 8 hexadecimal digits, followed by three groups of 4 hexadecimal digits each, followed by one group of 12 hexadecimal digits.
            // The following example GUID shows the groupings of hexadecimal digits in a GUID: 6B29FC40-CA47-1067-B31D-00DD010662DA
            // Often braces are added to enclose the above format, as such: {3F2504E0-4F89-11D3-9A0C-0305E82C3301}, So a total of 38 characters in the typical hexadecimal encoding with curly braces.

            Regex validateGUIDRegex = new Regex("^(?:\\{{0,1}(?:[0-9a-fA-F]){8}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){12}\\}{0,1})$");
            return validateGUIDRegex.IsMatch(guid);
        }


        public override string ToString()
        {
            return "Windows Globally Unique Identifier (GUID)";
        }
    }
}

