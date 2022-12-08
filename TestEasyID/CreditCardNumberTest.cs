using EasyID;
using EasyID.Data;

namespace TestEasyID
{
    public class CreditCardNumberTest : IDisposable
    {

        Driver testCaseDriver;
        CreditCardNumber testCaseCCN;

        public CreditCardNumberTest()
        {
            //setup the test
            //https://www.web-payment-software.com/test-credit-card-numbers/
            testCaseDriver = new Driver();
            testCaseDriver.Input = "4012 8888 8888 1887";
            testCaseDriver.Length = testCaseDriver.Input.Length;
            DataTemplate.SetDriver(testCaseDriver);
            testCaseCCN = new CreditCardNumber(testCaseDriver);
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        [Fact]
        public void TestCCNIsValid()
        {
            bool results = false;
            //Arrange
            //pass
            string expected = "  ## Additional interesting details here about the SSN go here. ##";

            //fail
            //string expected = "This seems to be an invalid Credit Card Number...";

            //Act
            string actual = testCaseDriver.Results += testCaseCCN.Process();

            //Assert
            Assert.Equal(expected, actual);
        }

        //[Theory]
        //[InlineData("4012 8888 8888 1881")]
        //[InlineData("4012 8888 8888 1882")]
        //[InlineData("4012 8888 8888 1883")]
        //public bool MyFirstTheory(string value)
        //{
        //    bool results = false;
        //    //Arrange
        //    //pass
        //    //string expected = "  ## Additional interesting details here about the SSN go here. ##";

        //    //fail
        //    string expected = "This seems to be an invalid Credit Card Number...";

        //    //Act
        //    string actual = testCaseDriver.Results += testCaseCCN.Process();

        //    //Assert
        //    Assert.Equal(expected, actual);
        //}
    }
}
