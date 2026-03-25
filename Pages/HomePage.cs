using OpenQA.Selenium;

namespace ParaBankTests.Selenium.Pages
{
    public class HomePage
    {
        private readonly IWebDriver _driver;

        private IWebElement WelcomeMessage => _driver.FindElement(By.CssSelector(".smallText"));
        private IWebElement LogoutLink => _driver.FindElement(By.LinkText("Log Out"));

        public HomePage(IWebDriver driver) => _driver = driver;

        public string GetWelcomeMessage() => WelcomeMessage.Text;
        public bool IsLogoutVisible() => LogoutLink.Displayed;
        public void Logout() => LogoutLink.Click();
    }
}