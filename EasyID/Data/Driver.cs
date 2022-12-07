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

        private string _noWhiteSpace = "";
        private string _noWhiteSpaceContent = "";
        private string _noWhiteSpaceLetterList = "";
        private string _noWhiteSpaceNumList = "";
        private string _noWhiteSpaceSymList = "";
        private string _noWhiteSpaceIndexList = "";

        private Dictionary<string, string> _hex = new Dictionary<string, string> { };
        private int _atSymbolCount = 0;
        private int _youTubeCount = 0;
        private string _btcStart = "";



        public void Reset()
        {
            this.Length = 0;
            this.Content = "";
            this.LetterList = "";
            this.NumList = "";
            this.SymList = "";
            this.IndexList = "";

            this.NoWhiteSpace = "";
            this.NoWhiteSpaceContent = "";
            this.NoWhiteSpaceLetterList = "";
            this.NoWhiteSpaceIndexList = "";
            this.NoWhiteSpaceNumList = "";
            this.NoWhiteSpaceSymList = "";

            this.Hex = new Dictionary<string, string> { };
            this.AtSymbolCount = 0;
            this.YouTubeCount = 0;
            this.BtcStart = "";
        }


        [Required]
        [StringLength(200, ErrorMessage = "Identifier too long (200 character limit).")]
        public string? Input { get; set; }  // The exact user input of the search field
        [Required]
        public string? Type { get; set; }   // Either "data string" or "file"
        public bool IsValidatedDesign { get; set; }

        public int Length // Returns the length of the input
        {
            get { return _length; }
            set { _length = value; }
        }
        public string NoWhiteSpace
        {
            get
            {
                _noWhiteSpace = Regex.Replace(this.Input, @"\s", "");
                return _noWhiteSpace;
            }
            set { _noWhiteSpace = value; }
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
        public string NoWhiteSpaceContent // Returns a string indicating the alphabetic/numeric/symbolic composition of the white space stripped input
        {
            get
            {
                if (this.NoWhiteSpaceLetterList.Length > 0 && this.NoWhiteSpaceNumList.Length == 0 && this.NoWhiteSpaceSymList.Length == 0) { _noWhiteSpaceContent = "alphabetic"; }       // aBcDeefGg
                if (this.NoWhiteSpaceLetterList.Length == 0 && this.NoWhiteSpaceNumList.Length > 0 && this.NoWhiteSpaceSymList.Length == 0) { _noWhiteSpaceContent = "numeric"; }          // 180454365
                if (this.NoWhiteSpaceLetterList.Length == 0 && this.NoWhiteSpaceNumList.Length == 0 && this.NoWhiteSpaceSymList.Length > 0) { _noWhiteSpaceContent = "symbolic"; }         // $#?- *^!!
                if (this.NoWhiteSpaceLetterList.Length > 0 && this.NoWhiteSpaceNumList.Length > 0 && this.NoWhiteSpaceSymList.Length == 0) { _noWhiteSpaceContent = "alphanumeric"; }      // a1B2ff7Q8
                if (this.NoWhiteSpaceLetterList.Length > 0 && this.NoWhiteSpaceNumList.Length == 0 && this.NoWhiteSpaceSymList.Length > 0) { _noWhiteSpaceContent = "alphasymbolic"; }     // a&b$c#@Dx
                if (this.NoWhiteSpaceLetterList.Length == 0 && this.NoWhiteSpaceNumList.Length > 0 && this.NoWhiteSpaceSymList.Length > 0) { _noWhiteSpaceContent = "numersymbolic"; }     // 1&23$*4!#
                if (this.NoWhiteSpaceLetterList.Length > 0 && this.NoWhiteSpaceNumList.Length > 0 && this.NoWhiteSpaceSymList.Length > 0) { _noWhiteSpaceContent = "alphanumersymbolic"; } // !1q@2w#3e

                return _noWhiteSpaceContent;
            }
            set { _noWhiteSpaceContent = value; }
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


        public string IndexList // Returns the input in the form of LSNNSLSLN based on the letters, symbols, or numbers within the input
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

        public string NoWhiteSpaceLetterList // Returns a string of the letters within the white space stripped input in the order they appear
        {
            get
            {
                _noWhiteSpaceLetterList = string.Empty;
                foreach (char ch in this.NoWhiteSpace)
                {
                    if (char.IsLetter(ch))
                    {
                        _noWhiteSpaceLetterList += ch;
                    }
                }
                return _noWhiteSpaceLetterList;
            }
            set { _noWhiteSpaceLetterList = value; }
        }

        public string NoWhiteSpaceNumList // Returns a string of the numbers within the white space stripped input in the order they appear
        {
            get
            {
                _noWhiteSpaceNumList = string.Empty;
                foreach (char ch in this.NoWhiteSpace)
                {
                    if (char.IsDigit(ch))
                    {
                        _noWhiteSpaceNumList += ch;
                    }
                }
                return _noWhiteSpaceNumList;
            }
            set { _noWhiteSpaceNumList = value; }
        }

        public string NoWhiteSpaceSymList // Returns a string of the symbols within the white space stripped input in the order they appear
        {
            get
            {
                _noWhiteSpaceSymList = string.Empty;
                foreach (char ch in this.NoWhiteSpace)
                {
                    if (char.IsLetter(ch) || char.IsDigit(ch))
                    {
                        _noWhiteSpaceSymList += "";
                    }
                    else
                    {
                        _noWhiteSpaceSymList += ch;
                    }
                }
                return _noWhiteSpaceSymList;
            }
            set { _noWhiteSpaceSymList = value; }
        }


        public string NoWhiteSpaceIndexList // Returns the input in the form of LSNNSLSLN based on the letters, symbols, or numbers within the white space stripped input
        {
            get
            {
                _noWhiteSpaceIndexList = string.Empty;
                foreach (char ch in this.NoWhiteSpace)
                {
                    if (char.IsLetter(ch))
                    {
                        _noWhiteSpaceIndexList += "L";
                    }
                    else if (char.IsDigit(ch))
                    {
                        _noWhiteSpaceIndexList += "N";
                    }
                    else
                    {
                        _noWhiteSpaceIndexList += "S";
                    }
                }
                return _noWhiteSpaceIndexList;
            }
            set { _noWhiteSpaceIndexList = value; }
        }

        public Dictionary<string, string> Hex // Returns a dictionary for each char of the input with the value of true or false whether or not the char is a hex character 
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


        public string BtcStart  // Returns the starting substring of the input if it matches the start of a BTC wallet address
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

        public string Results       // Holds a string of the search output
        {
            get { return _results; }
            set { _results = value; }
        }
    }
}