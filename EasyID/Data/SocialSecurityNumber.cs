using EasyID.Data;
using Microsoft.AspNetCore.Routing;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace EasyID
{
    public class SocialSecurityNumber : DataTemplate
    {
        private Driver _driver;

        private List<int> _lengthList = new List<int> { 9, 11 };
        private List<string> _indexList = new List<string> { "NNNNNNNNN", "NNNSNNSNNNN" };  //TODO :  Change eventually to deliniate just which chars S/L/N are allowed at which index. (i.e symbols only allowed at indices 4,7)
        private List<string> _contentList = new List<string> { "numeric", "numersymbolic" };
        private List<string> _symList = new List<string> { "  ", "--", ""};
        private string _letterList = "";
        private string _ssnState = "";
        private List<(int, int, string)> _states = new List<(int, int, string)>
        {
            (001, 003, "New Hampshire"),
            (004, 007, "Maine"),
            (008, 009, "Vermont"),
            (010, 034, "Massachusetts"),
            (035, 039, "Rhode Island"),
            (040, 049, "Connecticut"),
            (050, 134, "New York"),
            (135, 158, "New Jersey"),
            (159, 211, "Pennsylvania"),
            (212, 220, "Maryland"),
            (221, 222, "Delaware"),
            (223, 231, "Virginia"),
            (232, 232, "North Carolina"),
            (232, 236, "West Virginia"),
            (237, 246, "Not Issued"),
            (247, 251, "South Carolina"),
            (252, 260, "Georgia"),
            (261, 267, "Florida"),
            (268, 302, "Ohio"),
            (303, 317, "Indiana"),
            (318, 361, "Illinois"),
            (362, 386, "Michigan"),
            (387, 399, "Wisconsin"),
            (400, 407, "Kentucky"),
            (408, 415, "Tennessee"),
            (416, 424, "Alabama"),
            (425, 428, "Mississippi"),
            (429, 432, "Arkansas"),
            (433, 439, "Louisiana"),
            (440, 448, "Oklahoma"),
            (449, 467, "Texas"),
            (468, 477, "Minnesota"),
            (478, 485, "Iowa"),
            (486, 500, "Missouri"),
            (501, 502, "North Dakota"),
            (503, 504, "South Dakota"),
            (505, 508, "Nebraska"),
            (509, 515, "Kansas"),
            (516, 517, "Montana"),
            (518, 519, "Idaho"),
            (520, 520, "Wyoming"),
            (521, 524, "Colorado"),
            (525, 585, "New Mexico"),
            (526, 527, "Arizona"),
            (528, 529, "Utah"),
            (530, 680, "Nevada"),
            (531, 539, "Washington"),
            (540, 544, "Oregon"),
            (545, 573, "California"),
            (574, 574, "Alaska"),
            (575, 576, "Hawaii"),
            (577, 579, "District of Columbia"),
            (580, 580, "Virgin Islands"),
            (580, 584, "Puerto Rico"),
            (586, 586, "Guam, AMerican Samoa, or the Philippine Islands"),
            (587, 665, "Not Issued"),
            (667, 679, "Not Issued"),
            (681, 690, "Not Issued"),
            (691, 699, "Not Issued"),
            (700, 728, "Railroad Board**"),
            (729, 733, "Enumeration at Entry"),
            (750, 772, "Not Issued")
        };


        public SocialSecurityNumber(Driver d) : base(d)
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

        public int? SSNFirstThree
        {
            get
            {
                try
                {
                    bool match = false;
                    int firstThree = Int32.Parse(_driver.Input.Substring(0, 3));
                    foreach(var t in this._states)
                    {
                        if (firstThree >= t.Item1 && firstThree <= t.Item2)
                        {
                            this._ssnState += String.Format("{0} ", t.Item3);
                            match = true;
                        }
                    }
                    if (match == true)
                    {
                        return firstThree;
                    }
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

        public override string Process()
        {
            string returnString = "";

            if (!Valid(_driver.Input)) { returnString += "This seems to be an invalid Social Security Number...<br>"; }

            returnString += String.Format("This SSN was likely issued from the state of {0}.<br>" +
                "However, SSNs issued after June 25, 2011 implement randomization and won't follow this schematic.", this._ssnState);

            return returnString;
        }


        private static bool Valid(string number)
        {
            // SSN Regex
            // consists of 9 digits and usually divided by 3 parts by hyphen(XXX - XX - XXXX).
            // The first part can not be 000, 666, or between 900 - 900.
            // Second part can not be 00. Third part can not be 0000

            Regex validateSSNRegex = new Regex("^(?!666|000|9\\d{2})\\d{3}-?(?!00)\\d{2}-?(?!0{4})\\d{4}$");
            return validateSSNRegex.IsMatch(number);
        }

        public override string ToString()
        {
            return "Social Security Number";
        }
    }
}
