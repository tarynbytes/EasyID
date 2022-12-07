using EasyID.Data;
using System.Text.RegularExpressions;
using System.Net;
using System;
using System.Text;
using static System.Net.WebRequestMethods;
using System.IO;
using Newtonsoft.Json.Linq;

namespace EasyID.Data
{
    public class URL : DataTemplate
    {
        private Driver _driver;
        private static int _length = 5;
        private List<string> _contentList = new List<string> { "alphasymbolic", "alphanumersymbolic" };
        private string _uriHostNameType = "";
        private string _urlDetails = "";
        private string _proto = ""; // http
        private string _url = "";   // google.com
        private string _tld = "";   // .com

        public URL(Driver d) : base(d)
        {
            _driver = d;
            ParseUrl();
        }

        // Removes the 'www' subdomain if provided, and separates the protocol from the URL
        private void ParseUrl()
        {
            string[] proto = _driver.Input.Split("//");
            try
            {
                if (proto.Length == 1)
                {
                    // Default to http
                    _proto = "http://";
                    if (proto[0].Substring(0, 4) == "www.")
                    {
                        _url = proto[0].Substring(4, proto[0].Length - 4);
                    }
                    else _url = _driver.Input;
                }
                else
                {
                    _proto = proto[0] + "//";
                    if (proto[1].Substring(0, 4) == "www.")
                    {
                        if (proto.Length == 2)
                        {
                            _url = proto[1].Substring(4, proto[1].Length - 4);
                        }
                        else
                        {
                            _url = proto[1].Substring(4, proto[1].Length - 4) + string.Join("", proto, 2, proto.Length - 1);
                        }
                    }
                    else
                    {
                        _url = string.Join("", proto, 1, proto.Length - 1);
                    }
                }
            }
            catch {; }
            // Verify parse
            // Console.WriteLine(this._proto);
            // Console.WriteLine(this._url);
        }
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

        // Extracts the top-level domain from the URL (i.e: ".edu")
        public string? TopLevelDomain
        {
            get
            {
                try
                {
                    string f = "tlds.txt";
                    FileInfo file = new FileInfo(f);
                    string[]? dir = file.FullName.Split('\\').Reverse().Skip(1).Reverse().ToArray();

                    var matches = new Dictionary<int, List<string>> { };

                    string[] lines = System.IO.File.ReadAllLines(file.Name);
                    string joined = string.Join("!.", lines);
                    string[] tlds = joined.Split("!");
                    string upper = _driver.Input.ToUpper();
                    foreach (string tld in tlds)
                    {
                        if (upper.Contains(tld))
                        {
                            int i = upper.IndexOf(tld);
                            if (matches.ContainsKey(i))
                            {
                                matches[i].Add(tld);

                            }
                            else
                            {
                                List<string> values = new List<string>();
                                values.Add(tld);
                                matches.Add(i, values);
                            }
                        }
                    }
                    var matchList = matches[matches.Keys.Max()].OrderByDescending(x => x.Length).ToList();
                    _tld = matchList.First();

                }
                catch {;}

                if (_tld != "")
                {
                    return _tld;
                }
                else
                {
                    return null;
                }
            }
        }

        // Returns null if the stripped domain URL is not a reachable site
        public string? UriHostNameType
        {
            get
            {
                string[] strip = this._url.ToUpper().Split(_tld);
                string urlOnly = strip[0] + _tld;

                UriHostNameType type = Uri.CheckHostName(urlOnly);
                if (type == System.UriHostNameType.Unknown)
                {
                    return null;
                }
                else if (type == System.UriHostNameType.Basic)
                {
                    this._uriHostNameType = "Basic";
                }
                else if (type == System.UriHostNameType.Dns)
                {
                    this._uriHostNameType = "Dns";
                }
                else
                {
                    return null;
                }

                return _uriHostNameType;
            }
        }

        // Attempts to retrieve renderable response from a webpage
        public string URLDetails
        {
            get
            {
                if (_uriHostNameType == "Basic" || _uriHostNameType == "Dns")
                {
                    try
                    {
                        HttpWebRequest request = WebRequest.Create(this._proto + this._url) as HttpWebRequest;
                        request.Method = "GET";
                        HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                        WebHeaderCollection header = response.Headers;

                        using (var reader = new StreamReader(response.GetResponseStream()))
                        {
                            string webPage = reader.ReadToEnd();
                            _urlDetails += webPage;
                        }
                    }
                    catch
                    {
                        _urlDetails += "Can't render.";
                        return _urlDetails;
                    }
                }
                else { return _urlDetails; }
                return _urlDetails;
            }
        }

        public override string Process()
        {
            string returnString = "";
            returnString += String.Format("Attempting to render response from the web page:<br>{0}", this._urlDetails);
            return returnString;
        }


        public override string ToString()
        {
            return "URL";
        }
    }
}