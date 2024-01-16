using NUnit.Framework;
using SeleniumFramework;

namespace SeleniumTests.Tests
{
    [SetUpFixture]
    public class SetupFixture
    {
        [OneTimeTearDown]
        public void DisposeDriver()
        {
            Browser.CloseDriver();
        }
    }
}