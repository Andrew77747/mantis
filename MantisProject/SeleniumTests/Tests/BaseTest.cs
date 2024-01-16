using NUnit.Framework;
using SeleniumFramework;

namespace SeleniumTests.Tests
{
    [TestFixture]
    public abstract class BaseTest
    {
        [TearDown]
        protected void ScreenShotAfterFail()
        {
            ScreenshotHelper.ScreenshotAfterFail();
        }
    }
}