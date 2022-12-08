using EasyID.Data;


namespace EasyID
{
    public class FedExTrackingNumber : DataTemplate
    {


        // TODO
        private Driver _driver;
        public FedExTrackingNumber(Driver d) : base(d)
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
            return "FedEx Tracking Number";
        }
    }

}