using OpenQA.Selenium;

namespace ParaBankTests.Selenium.Helpers
{
    public static class ScreenshotHelper
    {
        public static string CaptureScreenshot(IWebDriver driver, string testName)
        {
            var screenshotDir = Path.GetFullPath(
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Screenshots"));

            Directory.CreateDirectory(screenshotDir);

            var fileName = Path.Combine(screenshotDir,
                $"{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.png");

            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(fileName);

            return fileName;
        }
    }
}