using OpenQA.Selenium;
using ParaBankTests.Selenium.Base;

namespace ParaBankTests.Selenium.Pages
{
    public class LoginPage : BasePage
    {
        // Locators
        private static readonly By UsernameField = By.Name("username");
        private static readonly By PasswordField = By.Name("password");
        private static readonly By LoginButton = By.CssSelector("input[value='Log In']");
        private static readonly By ErrorMessage = By.CssSelector(".error");
        private static readonly By LogoutLink = By.LinkText("Log Out");

        public LoginPage(IWebDriver driver) : base(driver) { }

        public void EnterUsername(string username) => Type(UsernameField, username);
        public void EnterPassword(string password) => Type(PasswordField, password);
        public void ClickLogin() => Click(LoginButton);
        public string GetErrorMessage() => GetText(ErrorMessage);
        public bool IsLogoutVisible() => IsDisplayed(LogoutLink);

        public void Login(string username, string password, bool isEnter = false)
        {
            EnterUsername(username);
            EnterPassword(password);
            if (!isEnter)
                 ClickLogin();
            WaitForPageLoad();
        }
    }
}
