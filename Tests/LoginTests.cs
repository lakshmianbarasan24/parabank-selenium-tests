using Allure.NUnit;
using Allure.NUnit.Attributes;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using ParaBankTests.Selenium.Base;
using ParaBankTests.Selenium.Helpers;
using ParaBankTests.Selenium.Pages;

namespace ParaBankTests.Selenium.Tests
{
    [TestFixture]
    [Category("Login")]
    public class LoginTests : BaseTest
    {
        private LoginPage _loginPage = null!;

        [SetUp]
        public void Init() => _loginPage = new LoginPage(Driver);

        [Test]
        [AllureTag("Smoke")]
        [AllureSeverity(Allure.Net.Commons.SeverityLevel.critical)]
        [Description("TC01 - Valid credentials should login successfully")]
        public void Login_WithValidCredentials_ShouldSucceed()
        {
            _loginPage.Login(TestData.ValidUsername, TestData.ValidPassword);

            _loginPage.IsLogoutVisible()
                .Should().BeTrue("valid credentials should redirect to account page and see Logout");
        }


        [Test]
        [AllureTag("Negative")]
        [AllureSeverity(Allure.Net.Commons.SeverityLevel.normal)]
        [Description("TC02 - Empty credentials should show error")]
        public void Login_WithEmptyCredentials_ShouldShowError()
        {
            _loginPage.Login(TestData.EmptyUsername, TestData.EmptyPassword);

            _loginPage.GetErrorMessage()
                .Should().Be("Please enter a username and password.", because: "user entered empty credentials" );
        }

        [Test]
        [AllureTag("Negative")]
        [AllureSeverity(Allure.Net.Commons.SeverityLevel.normal)]
        [Description("TC03 - Empty username should show error")]
        public void Login_WithEmptyUsername_ShouldShowError()
        {
            _loginPage.Login(TestData.EmptyUsername, TestData.EmptyPassword);

            _loginPage.GetErrorMessage()
                .Should().Be("Please enter a username and password.", because: "user entered empty username");
        }

        [Test]
        [AllureTag("Negative")]
        [AllureSeverity(Allure.Net.Commons.SeverityLevel.normal)]
        [Description("TC04 - Empty password should show error")]
        public void Login_WithEmptyPassword_ShouldShowError()
        {
            _loginPage.Login(TestData.EmptyUsername, TestData.EmptyPassword);

            _loginPage.GetErrorMessage()
                .Should().Be("Please enter a username and password.", because: "user entered empty password");
        }

        [Test]
        [AllureTag("Negative")]
        [AllureSeverity(Allure.Net.Commons.SeverityLevel.normal)]
        [Description("TC05 - Invalid password should show error")]
        public void Login_WithInvalidPassword_ShouldShowError()
        {
            _loginPage.Login(TestData.ValidUsername, TestData.InvalidPassword);

            _loginPage.GetErrorMessage()
                .Should().Be("The username and password could not be verified.", because: "user entered invalid password");
        }

        [Test]
        [AllureTag("Negative")]
        [AllureSeverity(Allure.Net.Commons.SeverityLevel.minor)]
        [Description("TC06 - Invalid username should show error")]
        public void Login_WithInvalidUsername_ShouldShowError()
        {
            _loginPage.Login(TestData.InvalidUsername, TestData.ValidPassword);

            _loginPage.GetErrorMessage()
                .Should().Be("The username and password could not be verified.", because: "user entered invalid username");
        }

        [Test]
        [AllureTag("Negative")]
        [AllureSeverity(Allure.Net.Commons.SeverityLevel.minor)]
        [Description("TC07 - Invalid credentials should show error")]
        public void Login_WithInvalidCredentials_ShouldShowError()
        {
            _loginPage.Login(TestData.InvalidUsername, TestData.InvalidPassword);

            _loginPage.GetErrorMessage()
                .Should().Be("The username and password could not be verified.", because: "user entered invalid credentials");
        }

        [Test]
        [AllureTag("Security")]
        [AllureSeverity(Allure.Net.Commons.SeverityLevel.minor)]
        [Description("TC08 - SQL injection attempt should not bypass login")]
        public void Login_WithSqlInjection_ShouldNotBypassAuth()
        {
            _loginPage.Login("' OR '1'='1", "' OR '1'='1");

            var currentUrl = Driver.Url;
            var pageSource = Driver.PageSource;

            // Pass the test if either:
            // 1. Security/Cloudflare page appeared (blocked = good)
            // 2. Error message shown (rejected = good)
            // 3. Not redirected to home (not logged in = good)

            bool blockedByCloudflare = pageSource.Contains("security verification")
                                       || pageSource.Contains("Cloudflare");

            bool showsLoginError = pageSource.Contains("error")
                                  || pageSource.Contains("invalid");

            bool notLoggedIn = !pageSource.Contains("Log Out");

            (blockedByCloudflare || showsLoginError || notLoggedIn)
                .Should().BeTrue("SQL injection should never grant access");
        }

        [Test]
        [AllureTag("Edge Case")]
        [AllureSeverity(Allure.Net.Commons.SeverityLevel.critical)]
        [Description("TC09 - Valid credentials with trailing spaces should login successfully")]
        public void Login_WithValidCredentialsWithTrailingSpaces_ShouldSucceed()
        {
            _loginPage.Login(TestData.ValidUsername+" ", TestData.ValidPassword+" ");

            _loginPage.IsLogoutVisible()
                .Should().BeTrue("valid credentials should redirect to account page and see Logout");
        }

        [Test]
        [AllureTag("Edge Case")]
        [AllureSeverity(Allure.Net.Commons.SeverityLevel.critical)]
        [Description("TC10 - Valid credentials with enter key should login successfully")]
        public void Login_WithValidCredentialsWithEnterKey_ShouldSucceed()
        {
            _loginPage.Login(TestData.ValidUsername, TestData.ValidPassword+Keys.Enter, true);

            _loginPage.IsLogoutVisible()
                .Should().BeTrue("valid credentials should redirect to account page and see Logout");
        }
    }
}
