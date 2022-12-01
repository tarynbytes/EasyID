using EasyID.Data;
using System.Text.RegularExpressions;

namespace EasyID.Data
{
    public class YouTubeLink : DataTemplate
    {
        private Driver _driver;
        private static int _length = 5;
        private List<string> _contentList = new List<string> { "alphasymbolic", "alphanumersymbolic" };
        private int _youTubeCount = 1; // ?: Can there be more than one "youtube" substring in a YouTube link?

        public YouTubeLink(Driver d) : base(d)
        {
            _driver = d;
        }


        // Is there a cap on the length?
        public int? Length
        {
            get
            {
                if (_driver.Length > _length)
                {
                    return _length;
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
        public int? YouTubeCount
        {
            get
            {
                if (_driver.YouTubeCount == _youTubeCount)
                {
                    return _youTubeCount;
                }
                return null;
            }
        }

        public override string Process()
        {
            string returnString = "";
            if (!Valid(_driver.Input)) { returnString += "This seems to be an invalid YouTube Link...<br>"; }

            returnString += "## Additional interesting details here about the YouTube Link go here. ##";
            // call a function to scrape the webpage and identify title of the video and/or other details.

            return returnString;
        }

        private static bool Valid(string link)
        {
            //TODO: fix regex
            Regex validateYouTubeRegex = new Regex("^((http|https)\\:\\/\\/)?(www\\.youtube\\.com|youtu\\.?be)\\/((watch\\?v=)?([a-zA-Z0-9]{11}))(&.*)*$"); 
            return validateYouTubeRegex.IsMatch(link);
        }

        public override string ToString()
        {
            return "YouTube Link";
        }
    }
}