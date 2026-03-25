using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParaBankTests.Selenium.Helpers
{
    public static class WaitHelper
    {
        // ─────────────────────────────────────────
        // Core Generic Wait — all other methods use this
        // ─────────────────────────────────────────

        /// <summary>
        /// Generic wait — waits until the condition returns a non-null, non-false value
        /// </summary>
        public static TResult WaitUntil<TResult>(
            IWebDriver driver,
            Func<IWebDriver, TResult> condition,
            int seconds = 15,
            string? errorMessage = null)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));

            if (errorMessage != null)
                wait.Message = errorMessage;

            // Ignore common transient exceptions during wait
            wait.IgnoreExceptionTypes(
                typeof(NoSuchElementException),
                typeof(StaleElementReferenceException),
                typeof(ElementNotInteractableException)
            );

            return wait.Until(condition);
        }

        public static IWebElement WaitForVisible(IWebDriver driver, By locator, int seconds = 15)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            return wait.Until(d =>
            {
                var el = d.FindElement(locator);
                return el.Displayed ? el : null;
            });
        }

        // Wait until element is clickable
        public static IWebElement WaitForClickable(IWebDriver driver, By locator, int seconds = 15)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            return wait.Until(d =>
            {
                var el = d.FindElement(locator);
                return (el.Displayed && el.Enabled) ? el : null;
            });
        }

        // Wait until element text is present
        public static bool WaitForText(IWebDriver driver, By locator, string text, int seconds = 15)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            return wait.Until(d =>
                d.FindElement(locator).Text.Contains(text)
            );
        }

        // Wait until URL contains expected value
        public static bool WaitForUrl(IWebDriver driver, string urlPart, int seconds = 15)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            return wait.Until(d => d.Url.Contains(urlPart));
        }

        // Wait until element disappears
        public static bool WaitForInvisible(IWebDriver driver, By locator, int seconds = 15)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            return wait.Until(d =>
            {
                try { return !d.FindElement(locator).Displayed; }
                catch (NoSuchElementException) { return true; }
            });
        }

        // Wait for page to fully load
        public static void WaitForPageLoad(IWebDriver driver, int seconds = 30)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            wait.Until(d =>
            {
                var js = (IJavaScriptExecutor)d;
                var ready = js.ExecuteScript("return document.readyState");
                return ready != null && ready.ToString() == "complete";
            });
        }
    }
}
