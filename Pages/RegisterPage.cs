using OpenQA.Selenium;
using ParaBankTests.Selenium.Base;
using ParaBankTests.Selenium.Config;

namespace ParaBankTests.Selenium.Pages
{
    public class RegisterPage : BasePage
    {
        // ─────────────────────────────────────────
        // Locators — Personal Info
        // ─────────────────────────────────────────
        private static readonly By FirstNameField = By.Id("customer.firstName");
        private static readonly By LastNameField = By.Id("customer.lastName");
        private static readonly By AddressField = By.Id("customer.address.street");
        private static readonly By CityField = By.Id("customer.address.city");
        private static readonly By StateField = By.Id("customer.address.state");
        private static readonly By ZipCodeField = By.Id("customer.address.zipCode");
        private static readonly By PhoneField = By.Id("customer.phoneNumber");
        private static readonly By SSNField = By.Id("customer.ssn");

        // ─────────────────────────────────────────
        // Locators — Credentials
        // ─────────────────────────────────────────
        private static readonly By UsernameField = By.Id("customer.username");
        private static readonly By PasswordField = By.Id("customer.password");
        private static readonly By ConfirmField = By.Id("repeatedPassword");
        private static readonly By RegisterButton = By.CssSelector("input[value='Register']");

        // ─────────────────────────────────────────
        // Locators — Validation Errors
        // ─────────────────────────────────────────
        private static readonly By FirstNameError = By.Id("customer.firstName.errors");
        private static readonly By LastNameError = By.Id("customer.lastName.errors");
        private static readonly By AddressError = By.Id("customer.address.street.errors");
        private static readonly By CityError = By.Id("customer.address.city.errors");
        private static readonly By StateError = By.Id("customer.address.state.errors");
        private static readonly By ZipCodeError = By.Id("customer.address.zipCode.errors");
        private static readonly By SSNError = By.Id("customer.ssn.errors");
        private static readonly By UsernameError = By.Id("customer.username.errors");
        private static readonly By PasswordError = By.Id("customer.password.errors");
        private static readonly By ConfirmError = By.Id("repeatedPassword.errors");

        // ─────────────────────────────────────────
        // Locators — Success
        // ─────────────────────────────────────────
        private static readonly By SuccessMessage = By.CssSelector("#rightPanel p");
        private static readonly By PageHeader = By.CssSelector("#rightPanel h1");

        public RegisterPage(IWebDriver driver) : base(driver) { }

        // ─────────────────────────────────────────
        // Actions
        // ─────────────────────────────────────────
        public void NavigateTo()
        {
            Driver.Navigate().GoToUrl(TestSettings.RegisterUrl);
            WaitForPageLoad();
        }

        public void FillPersonalInfo(
            string firstName, string lastName,
            string address, string city,
            string state, string zip,
            string phone, string ssn)
        {
            Type(FirstNameField, firstName);
            Type(LastNameField, lastName);
            Type(AddressField, address);
            Type(CityField, city);
            Type(StateField, state);
            Type(ZipCodeField, zip);
            Type(PhoneField, phone);
            Type(SSNField, ssn);
        }

        public void FillCredentials(string username, string password, string confirm)
        {
            Type(UsernameField, username);
            Type(PasswordField, password);
            Type(ConfirmField, confirm);
        }

        public void ClickRegister() => Click(RegisterButton);

        public void RegisterUser(
            string firstName, string lastName,
            string address, string city,
            string state, string zip,
            string phone, string ssn,
            string username, string password,
            string confirm)
        {
            FillPersonalInfo(firstName, lastName, address, city, state, zip, phone, ssn);
            FillCredentials(username, password, confirm);
            ClickRegister();
            WaitForPageLoad();
        }

        // ─────────────────────────────────────────
        // Getters — Success
        // ─────────────────────────────────────────
        public string GetSuccessMessage() => GetText(SuccessMessage);
        public string GetPageHeader() => GetText(PageHeader);

        // ─────────────────────────────────────────
        // Getters — Validation Errors
        // ─────────────────────────────────────────
        public string GetFirstNameError() => GetText(FirstNameError);
        public string GetLastNameError() => GetText(LastNameError);
        public string GetAddressError() => GetText(AddressError);
        public string GetCityError() => GetText(CityError);
        public string GetStateError() => GetText(StateError);
        public string GetZipCodeError() => GetText(ZipCodeError);
        public string GetSSNError() => GetText(SSNError);
        public string GetUsernameError() => GetText(UsernameError);
        public string GetPasswordError() => GetText(PasswordError);
        public string GetConfirmPasswordError() => GetText(ConfirmError);

        // ─────────────────────────────────────────
        // State Checks
        // ─────────────────────────────────────────
        public bool IsSuccessMessageDisplayed() => IsDisplayed(SuccessMessage);
        public bool IsFirstNameErrorDisplayed() => IsDisplayed(FirstNameError);
        public bool IsUsernameErrorDisplayed() => IsDisplayed(UsernameError);
        public bool IsPasswordErrorDisplayed() => IsDisplayed(PasswordError);
        public bool IsConfirmErrorDisplayed() => IsDisplayed(ConfirmError);
    }
}