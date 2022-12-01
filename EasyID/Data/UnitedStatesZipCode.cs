// Install-Package Newtonsoft.Json
using Newtonsoft.Json;
using System.Net;
using System;

namespace EasyID.Data
{
    public class UnitedStatesZipCode : DataTemplate
    {
        private Driver _driver;
        private List<int> _lengthList = new List<int> { 5, 10 };
        private List<string> _contentList = new List<string> { "numeric", "numersymbolic" };
        private List<string> _symList = new List<string> { "", "-" };
        private string _zipCode;
        private string _state;
        private string _timeZone;
        private string _county;
        // API used: https://data.opendatasoft.com/explore/dataset/georef-united-states-of-america-zc-point%40public/api/?q=


        public UnitedStatesZipCode(Driver d) : base(d)
        {
            _driver = d;
        }

        public string ZipCode
        {
            get
            {
                try
                {
                    string url = "https://data.opendatasoft.com/api/records/1.0/search/?dataset=georef-united-states-of-america-zc-point%40public&q=" + _driver.Input.Substring(0, 5) + "&facet=stusps_code&facet=ste_name&facet=coty_name";
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    using (Stream stream = response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string json = reader.ReadToEnd();
                        dynamic array = JsonConvert.DeserializeObject(json);
                        dynamic records = array["records"];

                        dynamic zip_code = records[0]["fields"]["zip_code"];
                        dynamic ste_name = records[0]["fields"]["ste_name"];
                        dynamic timezone = records[0]["fields"]["timezone"];
                        dynamic coty_name = records[0]["fields"]["coty_name"];

                        _zipCode = Convert.ToString(zip_code);
                        _state = Convert.ToString(ste_name);
                        _timeZone = Convert.ToString(timezone);
                        _county = Convert.ToString(coty_name);

                        return _zipCode;
                    }
                }
                catch
                {
                    _zipCode = null;
                    _state = null;
                    _timeZone = null;
                    _county = null;

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


        public override string Process()
        {
            string returnString = "";
            returnString += String.Format("State: {0}<br>County: {1}<br>Timezone: {2}", this._state, this._county, this._timeZone);
            return returnString;
        }

        public override string ToString()
        {
            return "United States Zip Code";
        }
    }
}

