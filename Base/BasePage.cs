using OpenQA.Selenium;
using ParaBankTests.Selenium.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParaBankTests.Selenium.Base
{
    public class BasePage
    {
        protected readonly IWebDriver Driver;

        protected BasePage(IWebDriver driver)
        {
            Driver = driver;
        }
        protected TResult Wait<TResult>(Func<IWebDriver, TResult> condition) =>
        WaitHelper.WaitUntil(Driver, condition);

        // Safe find with explicit wait
        protected IWebElement Find(By locator) =>
            WaitHelper.WaitForVisible(Driver, locator);

        // Safe click with wait
        protected void Click(By locator) =>
            WaitHelper.WaitForClickable(Driver, locator).Click();

        // Safe type — clears first
        protected void Type(By locator, string text)
        {
            var el = WaitHelper.WaitForVisible(Driver, locator);
            el.Clear();
            el.SendKeys(text);
        }

        // Safe get text
        protected string GetText(By locator) =>
            Find(locator).Text;

        // Check element exists safely
        protected bool IsDisplayed(By locator)
        {
            try { return Driver.FindElement(locator).Displayed; }
            catch (NoSuchElementException) { return false; }
        }

        // Wait for page load
        protected void WaitForPageLoad() =>
            WaitHelper.WaitForPageLoad(Driver);
    }
}
