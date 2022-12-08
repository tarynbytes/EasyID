using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
namespace EasyID.Data
{
    public class UnitedStatesPhoneNumber : DataTemplate
    {

        private Driver _driver;
        private static List<int> _lengthList = new List<int>();
        private List<string> _contentList = new List<string> { "numeric", "numersymbolic" };
        private List<string> _indexList = new List<string> { "NNNNNNN", "NNNSNNNN", "NNNNNNNNNN", "NNNSNNNSNNNN", "SNNNSNNNNNNN", "SNNNSNNNSNNNN", "SNNNSSNNNNNNN", "SNNNSSNNNSNNNN" };
        private List<string> _symList = new List<string> { "", "-", "--", "()", "()-", "()--" };
        private string _letterList = "";
        private string _areaCode = "";
        private string _areaCodeCallerID = "";
        private string _phoneInput = "";
        private string _phoneInputIndexList = "";
        private string _phoneInputSymList = "";

        // Caller ID Table Used: https://www.bennetyee.org/ucsd-pages/area.state.html


        public UnitedStatesPhoneNumber(Driver d) : base(d)
        {
            _driver = d;
            ParseDriver();
            GetPhoneInputIndexList();
            GetPhoneInputSymList();
            GetAreaCode();
        }


        // change format of the input to remove the country code and any white space
        // use this new format throughout the class to make our comparisons
        private void ParseDriver()
        {
            if (_driver.Input.StartsWith("1+"))
            {
                _phoneInput = _driver.Input.Substring(2, _driver.Input.Length -2);
            }
            else if (_driver.Input.StartsWith("1"))
            {
                _phoneInput = _driver.Input.Substring(1, _driver.Input.Length - 1);
            }
            else
            {
                _phoneInput = _driver.Input;
            }
            _phoneInput = Regex.Replace(_phoneInput, @"\s", "");
        }

        // Returns first three digits of the input
        private void GetAreaCode()
        {
            string numsOnly = "";
            foreach (char ch in _phoneInput)
            {
                if (char.IsDigit(ch))
                {
                    numsOnly += ch;
                }
            }
            if (numsOnly.Length == 10)
            {
                _areaCode = numsOnly.Substring(0, 3);
            }
            else
            {
                _areaCode = "";
            }
        }
        
        // Reads from online HTML table of area codes and pulls out the matching geographical location
        // Returns null if no caller ID match.
        public string CallerID
        {
            get
            {
                WebClient client = new WebClient();
                string page = client.DownloadString("https://www.bennetyee.org/ucsd-pages/area.state.html");
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(page);

                var query = from table in doc.DocumentNode.SelectNodes("//table").Cast<HtmlNode>()
                            from row in table.SelectNodes("tr").Cast<HtmlNode>()
                            from cell in row.SelectNodes("th|td").Cast<HtmlNode>()
                            select new { Table = table.Id, CellText = cell.InnerText };

                int idx = 0;
                foreach (var cell in query)
                {
                    if (cell.CellText == _areaCode)
                    {
                        _areaCodeCallerID = query.ElementAt(idx + 3).CellText;
                    }
                    idx++;
                }

                if (_areaCodeCallerID == "")
                {
                    _areaCodeCallerID = null;
                }
                return _areaCodeCallerID;
            }

            
        }

        // Sets the _phoneInputIndexList int the form of LSNNSLSLN based on the letters, symbols, or numbers within the input
        private void GetPhoneInputIndexList()
        {
            foreach (char ch in _phoneInput)
            {
                if (char.IsLetter(ch))
                {
                    _phoneInputIndexList += "L";
                }
                else if (char.IsDigit(ch))
                {
                    _phoneInputIndexList += "N";
                }
                else
                {
                    _phoneInputIndexList += "S";
                }
            }

        }

        // Sets _phoneInputSymList int the form of LSNNSLSLN based on the letters, symbols, or numbers within the input
        private void GetPhoneInputSymList()
        {

                foreach (char ch in _phoneInput)
                {
                    if (char.IsLetter(ch) || char.IsDigit(ch))
                    {
                    _phoneInputSymList += "";
                    }
                    else
                    {
                    _phoneInputSymList += ch;
                    }
                }

        }

        public int? Length
        {
            get
            {
                for (int i = 7; i <= 17; i++) // Shortest format: 3334444 | Longest format: 1+ (222) 333-4444
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
                    if (_s == _phoneInputSymList)
                    {
                        return _s;
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
                    if (_s == _phoneInputIndexList)
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
            if (!Valid(_driver.Input)) { returnString += "This seems to be an invalid Phone Number...<br>"; }
            returnString += String.Format("The area code listing for this phone number is: {0}", _areaCodeCallerID);
            return returnString;
        }

        private static bool Valid(string number)
        {
            // This regular expression will match phone numbers entered with delimiters (spaces, dots, brackets, etc.)
            Regex validatePhoneNumberRegex = new Regex("^\\+?\\d{1,4}?[-.\\s]?\\(?\\d{1,3}?\\)?[-.\\s]?\\d{1,4}[-.\\s]?\\d{1,4}[-.\\s]?\\d{1,9}$"); 
            return validatePhoneNumberRegex.IsMatch(number);
        }


        public override string ToString()
        {
            return "A phone number from the United States";
        }
    }
}