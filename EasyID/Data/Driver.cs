using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace EasyID.Data
{
    public class Driver
    {
        private int _length;
        private string _content;
        private string _results;
        private string _letterList = "";
        private string _numList = "";
        private string _symList = "";
        private string _indexList = "";
        private Dictionary<string, string> _hex = new Dictionary<string, string> { };
        private int _atSymbolCount = 0;
        private int _youTubeCount = 0;
        private string _abbreviation = "";
        private string _zipCode = "";
        private string _btcStart = "";
        private int[] _octets = new int[4];
        private string _mac = "";
        private int _ssnFirstThree = 0;
        private string _uriHostNameType = "";
        private string _urlDetails = "";
        private string _tld = "";

        public void Reset()
        {
            this.Length = 0;
            this.Content = "";
            this.LetterList = "";
            this.NumList = "";
            this.SymList = "";
            this.IndexList = "";
            this.Hex = new Dictionary<string, string> { };
            this.AtSymbolCount = 0;
            this.YouTubeCount = 0;
            this.Abbreviation = "";
            this.ZipCode = "";
            this.BtcStart = "";
            this.Octets = new int[4];
            this.Mac = "";
            this.SSNFirstThree = 0;
            this.UriHostNameType = "";
            this.URLDetails = "";
            this.TopLevelDomain = "";
        }


        [Required]
        [StringLength(200, ErrorMessage = "Identifier too long (200 character limit).")]
        public string? Input { get; set; }
        [Required]
        public string? Type { get; set; }
        public bool IsValidatedDesign { get; set; }

        public int Length // Returns the length of the input
        {
            get { return _length; }
            set { _length = value; }
        }
        public string Content // Returns a string indicating the alphabetic/numeric/symbolic composition of the input
        {
            get
            {
                if (this.LetterList.Length > 0 && this.NumList.Length == 0 && this.SymList.Length == 0) { _content = "alphabetic"; }       // aBcDeefGg
                if (this.LetterList.Length == 0 && this.NumList.Length > 0 && this.SymList.Length == 0) { _content = "numeric"; }          // 180454365
                if (this.LetterList.Length == 0 && this.NumList.Length == 0 && this.SymList.Length > 0) { _content = "symbolic"; }         // $#?- *^!!
                if (this.LetterList.Length > 0 && this.NumList.Length > 0 && this.SymList.Length == 0) { _content = "alphanumeric"; }      // a1B2ff7Q8
                if (this.LetterList.Length > 0 && this.NumList.Length == 0 && this.SymList.Length > 0) { _content = "alphasymbolic"; }     // a&b$c#@Dx
                if (this.LetterList.Length == 0 && this.NumList.Length > 0 && this.SymList.Length > 0) { _content = "numersymbolic"; }     // 1&23$*4!#
                if (this.LetterList.Length > 0 && this.NumList.Length > 0 && this.SymList.Length > 0) { _content = "alphanumersymbolic"; } // !1q@2w#3e

                return _content;
            }
            set { _content = value; }
        }
        public string LetterList // Returns a string of the letters within the input in the order they appear
        {
            get
            {
                _letterList = string.Empty;
                foreach (char ch in this.Input)
                {
                    if (char.IsLetter(ch))
                    {
                        _letterList += ch;
                    }
                }
                return _letterList;
            }
            set { _letterList = value; }
        }
        public string NumList // Returns a string of the numbers within the input in the order they appear
        {
            get
            {
                _numList = string.Empty;
                foreach (char ch in this.Input)
                {
                    if (char.IsDigit(ch))
                    {
                        _numList += ch;
                    }
                }
                return _numList;
            }
            set { _numList = value; }
        }

        public string SymList // Returns a string of the symbols within the input in the order they appear
        {
            get
            {
                _symList = string.Empty;
                foreach (char ch in this.Input)
                {
                    if (char.IsLetter(ch) || char.IsDigit(ch))
                    {
                        _symList += "";
                    }
                    else
                    {
                        _symList += ch;
                    }
                }
                return _symList;
            }
            set { _symList = value; }
        }


        public string IndexList // Returns the input int the form of LSNNSLSLN based on the letters, symbols, or numbers within the input
        {
            get
            {
                _indexList = string.Empty;
                foreach (char ch in this.Input)
                {
                    if (char.IsLetter(ch))
                    {
                        _indexList += "L";
                    }
                    else if (char.IsDigit(ch))
                    {
                        _indexList += "N";
                    }
                    else
                    {
                        _indexList += "S";
                    }
                }
                return _indexList;
            }
            set { _indexList = value; }
        }
        public Dictionary<string, string> Hex // Returns a string of the symbols within the input in the order they appear
        {
            get
            {
                char[] separators = new char[] { ' ', ';', ',', ':', '-', '.', '\r', '\t', '\n' };
                string[] temp = this.Input.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                string onlyHex = String.Join("", temp);

                foreach (char ch in onlyHex)
                {
                    if (Regex.IsMatch(Char.ToString(ch), @"\A\b[0-9a-fA-F]+\b\Z") == true)
                        _hex[this.Input] = "true";
                    else
                    {
                        _hex[this.Input] = "false";
                        break;
                    }
                }
                return _hex;
            }
            set { _hex = value; }
        }
        public int AtSymbolCount // Returns the number of '@' symbols within the input
        {
            get
            {
                _atSymbolCount = 0;
                foreach (char ch in this.Input)
                {
                    if (ch == '@')
                    {
                        _atSymbolCount = _atSymbolCount + 1;
                    }
                }
                return _atSymbolCount;
            }
            set { _atSymbolCount = value; }
        }


        public int YouTubeCount // Returns the number of case-insensitive "youtube" substrings within the input
        {
            get
            {
                _youTubeCount = Regex.Matches(this.LetterList, "youtube").Count;
                return _youTubeCount;
            }
            set { _youTubeCount = value; }
        }

        public string Results
        {
            get { return _results; }
            set { _results = value; }
        }

        public string Abbreviation
        {
            get
            {
                _abbreviation = this.Input.Substring(0, 2);
                return _abbreviation;
            }
            set { _abbreviation = value; }
        }
        public string ZipCode
        {
            get
            {
                _zipCode = this.Input.Substring(0, 5);
                return _zipCode;
            }
            set { _zipCode = value; }
        }
        public string Mac
        {
            get
            {
                _mac = this.Input;
                return _mac;
            }
            set { _mac = value; }
        }
        public string BtcStart
        {
            get
            {
                try
                {
                    _btcStart = string.Empty;
                    if (this.Input.Substring(0, 1) == "1")
                    {
                        _btcStart += "1";
                    }
                    else if (this.Input.Substring(0, 1) == "3")
                    {
                        _btcStart += "3";
                    }
                    else if (this.Input.Substring(0, 3) == "bc1")
                    {
                        _btcStart += "bc1";
                    }
                    else
                    {
                        return null;
                    }
                    return _btcStart;
                }
                catch
                {
                    return null;
                }
            }
            set { _btcStart = value; }
        }
        public int[] Octets
        {
            get
            {
                try
                {
                    string[] splits = this.Input.Split('.');
                    for (int i = 0; i < 4; i++)
                    {
                        _octets[i] = Int32.Parse(splits[i]);
                    }
                }
                catch {; }

                return _octets;
            }
            set { _octets = value; }
        }
        public int SSNFirstThree
        {
            get
            {
                try
                {
                    return Int32.Parse(this.Input.Substring(0, 3));
                }
                catch
                { return _ssnFirstThree; }
            }
            set { _ssnFirstThree = value; }
        }
        public string UriHostNameType
        {
            get
            {
                _uriHostNameType = Uri.CheckHostName(this.Input).ToString();
                return _uriHostNameType;
            }
            set { _uriHostNameType = value; }
        }
        public string URLDetails
        {
            get { return _urlDetails; }
            set { _urlDetails = value; }
        }
        public string TopLevelDomain
        {
            get { return _tld; }
            set { _tld = value; }
        }
    }
}