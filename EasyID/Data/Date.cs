using EasyID.Data;


namespace EasyID
{
    public class Date : DataTemplate
    {


        // TODO
        private Driver _driver;
        public Date(Driver d) : base(d)
        {
            _driver = d;

        }
        public override string Process()
        {
            string returnString = "";
            returnString += "";
            return returnString;
        }

        public override string ToString()
        {
            return "Date";
        }
    }

}