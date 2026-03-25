using Allure.Net.Commons;
using Allure.NUnit;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using ParaBankTests.Selenium.Config;
using ParaBankTests.Selenium.Helpers;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace ParaBankTests.Selenium.Base
{

    [AllureNUnit]
    public class BaseTest
    {
        protected IWebDriver Driver { get; private set; } = null!;
        protected const string BaseUrl = "https://parabank.parasoft.com/";

        [SetUp]
        public void SetUp()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());

            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            // options.AddArgument("--headless=new"); // uncomment for CI

            Driver = new ChromeDriver(options);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(TestSettings.ImplicitWait);
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(TestSettings.PageLoadTimeout);
            Driver.Navigate().GoToUrl(TestSettings.BaseUrl);
        }

        [TearDown]
        public void TearDown()
        {
            // Capture screenshot on failure
            if (TestContext.CurrentContext.Result.Outcome.Status ==
                NUnit.Framework.Interfaces.TestStatus.Warning)
            {
                var screenshotPath = ScreenshotHelper.CaptureScreenshot(
                    Driver,
                    TestContext.CurrentContext.Test.Name
                );
                TestContext.AddTestAttachment(screenshotPath, "Failure Screenshot");
            }

            Driver?.Quit();
            Driver?.Dispose();
        }
    }
}