namespace EasyID.Data
{
    public class BitcoinWalletAddress : DataTemplate
    {
        private Driver _driver;
        private static List<int> _lengthList = new List<int>();
        private static string _content = "alphanumeric";
        private static List<string> _btcStartList = new List<string> { "1", "3", "bc1"};

        public BitcoinWalletAddress(Driver d) : base(d)
        {
            _driver = d;
        }

        public int? Length
        {
            get
            {
                for (int i = 26; i <= 35; i++)
                {
                    _lengthList.Add(i);
                }
                foreach(int _length in _lengthList)
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

        public string? BtcStart
        {
            get
            {
                foreach (string _s in _btcStartList)
                {
                    if (_s == _driver.BtcStart)
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
            returnString += HashUsed();
            return returnString;
        }

        private string HashUsed()
        {
            if (_driver.BtcStart == "1")
            {
                return @"This script used for this address is Pay-to-Public-Key-Hash (P2PKH).<br>
                    This controls how bitcoin can be spent, locking the bitcoin to the hash of a public key.<br>
                    It is the most common type of bitcoin transaction script there is.<br>";
            }
            else if (_driver.BtcStart == "3")
            {
                return @"This script used for this address is Pay-to-Script-Hash (P2SH).<br>
                    This controls how bitcoin can be spent, locking the bitcoin to a P2SH ScriptPubKey<br>.
                    It is flexible and allows users to construct arbitraty scripts.<br>";
            }
            else if (_driver.BtcStart == "btc1")
            {
                return @"This protocol implemeneted to generate this address is Segregated Witness (SegWit).<br>
                    This type of address was activated in 2017 and increased the use of block space.<br>
                    It makes use of Bech32 encoding, upgraded from the previous Base58 encoding scheme.<br>";
            }
            else
            {
                return "The hash used to generate this address is unkown.";
            }
        }


        public override string ToString()
        {
            return "Bitcoin Wallet Address";
        }
    }
}