using EasyID;
using EasyID.Data;


namespace TestEasyID
{
    public class SocialSecurityNumberTest : IDisposable
    {
        Driver testCaseDriver;
        SocialSecurityNumber testCaseSSN;
        
        public SocialSecurityNumberTest()
        {
            //setup the test
            testCaseDriver = new Driver();
            testCaseDriver.Input = "331-72-7788";
            testCaseDriver.Length = testCaseDriver.Input.Length;
            DataTemplate.SetDriver(testCaseDriver);
            testCaseSSN = new SocialSecurityNumber(testCaseDriver);
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        [Fact]
        public void TestSSNIsValid()
        {
            bool results = false;
            //Arrange
            //pass
            string expected = "  ## Additional interesting details here about the SSN go here. ##";

            //fail
            //string expected = "This seems to be an invalid SSN...";

            //Act
            string actual = testCaseDriver.Results += testCaseSSN.Process();

            //Assert
            Assert.Equal(expected, actual);
        }
        
    }
}