using System.Text.RegularExpressions;

namespace EasyID.Data
{
    public class WindowsUUID : DataTemplate
    {
        private Driver _driver;
        private List<int> _lengthList = new List<int> { 32, 36 }; //https://stackoverflow.com/questions/13397038/uuid-max-character-length
        private static string _content = "alphanumersymbolic"; // TODO is this correct? 
        private List<string> _symList = new List<string> { "-" };

        public WindowsUUID(Driver d) : base(d)
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

            if (!Valid(_driver.Input)) { returnString += "This seems to be an invalid UUID...<br>"; }

            returnString += "## Additional interesting details here about the UUID go here. ##";
            // call Class()

            return returnString;
        }

        private static bool Valid(string uuid)
        {
            // UUID Regex 
            // UUID is a 128-bit label used for identifications in computer systems.
            // A standard UUID code contains 32 hex digits along with 4 “-” symbols, which makes its length equal to 36 characters. There is also a Nil UUID code where all bits are set to zero.

            Regex validateUUIDRegex = new Regex("^[0-9a-f]{8}-[0-9a-f]{4}-[0-5][0-9a-f]{3}-[089ab][0-9a-f]{3}-[0-9a-f]{12}$");
            return validateUUIDRegex.IsMatch(uuid);
        }


        public override string ToString()
        {
            return "UUID";
        }
    }
}

