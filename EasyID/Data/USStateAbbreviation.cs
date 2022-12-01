using EasyID.Data;
using System.Text.RegularExpressions;

namespace EasyID
{
    public class USStateAbbreviation : DataTemplate
    {
        private Driver _driver;

        private List<int> _lengthList = new List<int> { 2, 3 };
        private List<string> _indexList = new List<string> { "LL", "LLS" };
        private List<string> _contentList = new List<string> { "alphabetic", "alphasymbolic" };
        private List<string> _symList = new List<string> { "", "." };
        private string _numList = "";
        private Dictionary<string, string> _abbreviations = new Dictionary<string, string>
            {
                ["AL"] = "Alabama",
                ["AR"] = "Arkansas",
                ["CA"] = "California",
                ["CO"] = "Colorado",
                ["CT"] = "Connecticut",
                ["DE"] = "Delaware",
                ["DC"] = "District of Columbia",
                ["FL"] = "Florida",
                ["GA"] = "Georgia",
                ["HI"] = "Hawaii",
                ["ID"] = "Idaho",
                ["IL"] = "Illinois",
                ["IN"] = "Indiana",
                ["IA"] = "Iowa",
                ["KS"] = "Kansas",
                ["KY"] = "Kentucky",
                ["LA"] = "Louisiana",
                ["ME"] = "Maine",
                ["MD"] = "Maryland",
                ["MA"] = "Massachusetts",
                ["MI"] = "Michigan",
                ["MN"] = "Minnesota",
                ["MS"] = "Mississippi",
                ["MO"] = "Missouri",
                ["MT"] = "Montana",
                ["NE"] = "Nebraska",
                ["NV"] = "Nevada",
                ["NH"] = "New Hampshire",
                ["NJ"] = "New Jersey",
                ["NM"] = "New Mexico",
                ["NY"] = "New York",
                ["NC"] = "North Carolina",
                ["ND"] = "North Dakota",
                ["OH"] = "Ohio",
                ["OK"] = "Oklahoma",
                ["OR"] = "Oregon",
                ["PA"] = "Pennsylvania",
                ["RI"] = "Rhode Island",
                ["SC"] = "South Carolina",
                ["SD"] = "South Dakota",
                ["TN"] = "Tennessee",
                ["TX"] = "Texas",
                ["UT"] = "Utah",
                ["VT"] = "Vermont",
                ["VA"] = "Virginia",
                ["WA"] = "Washington",
                ["WV"] = "West Virginia",
                ["WI"] = "Wisconsin",
                ["WY"] = "Wyoming"
        };


        public USStateAbbreviation(Driver d) : base(d)
        {
            _driver = d;
        }


        public string Abbreviation
        {
            get
            {
                try
                {
                    bool keyExists = _abbreviations.ContainsKey(_driver.Abbreviation);
                    if (keyExists)
                        return _driver.Abbreviation;
                    else
                    {
                        return null;
                    }
                }
                catch
                {
                    return null;
                }
            }
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

        public string NumList
        {
            get
            {
                if (_driver.NumList == _numList)
                {
                    return _numList;
                }
                return null;
            }
        }
        public override string Process()
        {
            string returnString = "";
            returnString += String.Format("The state for this abbreviation is {0}.", _abbreviations[_driver.Input.Substring(0, 2)]);
            return returnString;
        }

        public override string ToString()
        {
            return "A state abbreviation for the United States";
        }
    }
}
