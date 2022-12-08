using EasyID;
using EasyID.Data;



namespace TestEasyID
{
    public class DateTest : IDisposable
    {

        Driver testCaseDriver;
        Date testCaseDate;

        public DateTest()
        {
            //setup the test
            testCaseDriver = new Driver();
            testCaseDriver.Input = "11/25/1945";
            testCaseDriver.Length = testCaseDriver.Input.Length;
            DataTemplate.SetDriver(testCaseDriver);
            testCaseDate = new Date(testCaseDriver);
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        [Fact]
        public void TestDateIsValid()
        {
            bool results = false;
            //Arrange
            //pass 11/25/1945
            string expected = "  ## Additional interesting details here about the Date go here. ##";

            //fail 19451125
            //string expected = "This seems to be an invalid Date...  ## Additional interesting details here about the Date go here. ##";

            //Act
            string actual = testCaseDriver.Results += testCaseDate.Process();

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
