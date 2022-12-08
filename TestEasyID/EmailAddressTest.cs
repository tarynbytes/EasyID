using EasyID;
using EasyID.Data;


namespace TestEasyID
{
    public class EmailAddressTest : IDisposable
    {
        Driver testCaseDriver;
        EmailAddress testCaseEmail;

        public EmailAddressTest()
        {
            //setup the test
            testCaseDriver = new Driver();
            testCaseDriver.Input = "joe_8850@hotmail.com";
            testCaseDriver.Length = testCaseDriver.Input.Length;
            DataTemplate.SetDriver(testCaseDriver);
            testCaseEmail = new EmailAddress(testCaseDriver);
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        [Fact]
        public void TestEmailIsValid()
        {
            bool results = false;
            //Arrange
            //pass joe_8850@hotmail.com
            string expected = "  ## Additional interesting details here about the Email Address go here. ##";

            //fail joe_8850@hotmail.cmm
            //string expected = "This seems to be an invalid Email Address...  ## Additional interesting details here about the Email Address go here. ##";

            //Act
            string actual = testCaseDriver.Results += testCaseEmail.Process();

            //Assert
            Assert.Equal(expected, actual);
        }

    }
}