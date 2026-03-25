using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using FluentAssertions;
using NUnit.Framework;
using ParaBankTests.Selenium.Base;
using ParaBankTests.Selenium.Helpers;
using ParaBankTests.Selenium.Pages;

namespace ParaBankTests.Selenium.Tests
{
    [TestFixture]
    [Category("Registration")]
    public class RegisterTests : BaseTest
    {
        private RegisterPage _registerPage = null!;

        [SetUp]
        public void Init()
        {
            _registerPage = new RegisterPage(Driver);
            _registerPage.NavigateTo();
        }

        // ─────────────────────────────────────────
        // TC01 — Happy Path
        // ─────────────────────────────────────────
        [Test]
        [AllureTag("Smoke")]
        [AllureSeverity(SeverityLevel.critical)]
        [Description("TC01 - Valid details should register successfully")]
        public void Register_WithValidDetails_ShouldSucceed()
        {
            string uniqueUsername = TestData.GenerateUsername();

            _registerPage.RegisterUser(
                TestData.FirstName, TestData.LastName,
                TestData.Address, TestData.City,
                TestData.State, TestData.ZipCode,
                TestData.Phone, TestData.SSN,
                uniqueUsername, TestData.Password,
                TestData.Password
            );

            _registerPage.IsSuccessMessageDisplayed()
                .Should().BeTrue("success message should appear after valid registration");

            _registerPage.GetSuccessMessage()
                .Should().Contain("Your account was created successfully",
                    because: "valid details should create an account");
        }

        // ─────────────────────────────────────────
        // TC02 — All Fields Empty
        // ─────────────────────────────────────────
        [Test]
        [AllureTag("Negative", "Validation")]
        [AllureSeverity(SeverityLevel.normal)]
        [Description("TC02 - Empty form should show all required field errors")]
        public void Register_WithEmptyForm_ShouldShowAllErrors()
        {
            _registerPage.ClickRegister();

            _registerPage.GetFirstNameError()
                .Should().Be("First name is required.",
                    because: "first name is a required field");

            _registerPage.GetLastNameError()
                .Should().Be("Last name is required.",
                    because: "last name is a required field");

            _registerPage.GetAddressError()
                .Should().Be("Address is required.",
                    because: "address is a required field");

            _registerPage.GetCityError()
                .Should().Be("City is required.",
                    because: "city is a required field");

            _registerPage.GetStateError()
                .Should().Be("State is required.",
                    because: "state is a required field");

            _registerPage.GetZipCodeError()
                .Should().Be("Zip Code is required.",
                    because: "zip code is a required field");

            _registerPage.GetSSNError()
                .Should().Be("Social Security Number is required.",
                    because: "SSN is a required field");

            _registerPage.GetUsernameError()
                .Should().Be("Username is required.",
                    because: "username is a required field");

            _registerPage.GetPasswordError()
                .Should().Be("Password is required.",
                    because: "password is a required field");
        }

        // ─────────────────────────────────────────
        // TC03 — Password Mismatch
        // ─────────────────────────────────────────
        [Test]
        [AllureTag("Negative", "Validation")]
        [AllureSeverity(SeverityLevel.normal)]
        [Description("TC03 - Mismatched passwords should show confirm password error")]
        public void Register_WithMismatchedPasswords_ShouldShowError()
        {
            _registerPage.FillPersonalInfo(
                TestData.FirstName, TestData.LastName,
                TestData.Address, TestData.City,
                TestData.State, TestData.ZipCode,
                TestData.Phone, TestData.SSN
            );
            _registerPage.FillCredentials(
                TestData.GenerateUsername(),
                "Password@123",
                "DifferentPass@456"
            );
            _registerPage.ClickRegister();

            _registerPage.IsConfirmErrorDisplayed()
                .Should().BeTrue("confirm password error should be visible");

            _registerPage.GetConfirmPasswordError()
                .Should().Be("Passwords did not match.",
                    because: "confirm password must match the password field");
        }

        // ─────────────────────────────────────────
        // TC04 — Duplicate Username
        // ─────────────────────────────────────────
        [Test]
        [AllureTag("Negative")]
        [AllureSeverity(SeverityLevel.normal)]
        [Description("TC04 - Already taken username should show error")]
        public void Register_WithDuplicateUsername_ShouldShowError()
        {
            string username = TestData.GenerateUsername();

            // First registration
            _registerPage.RegisterUser(
                TestData.FirstName, TestData.LastName,
                TestData.Address, TestData.City,
                TestData.State, TestData.ZipCode,
                TestData.Phone, TestData.SSN,
                username, TestData.Password, TestData.Password
            );

            // Second registration with same username
            _registerPage.NavigateTo();
            _registerPage.RegisterUser(
                TestData.FirstName, TestData.LastName,
                TestData.Address, TestData.City,
                TestData.State, TestData.ZipCode,
                TestData.Phone, TestData.SSN,
                username, TestData.Password, TestData.Password
            );

            _registerPage.IsUsernameErrorDisplayed()
                .Should().BeTrue("duplicate username error should be visible");

            _registerPage.GetUsernameError()
                .Should().Contain("already exists",
                    because: "duplicate usernames should not be allowed");
        }

        // ─────────────────────────────────────────
        // TC05 — Missing First Name
        // ─────────────────────────────────────────
        [Test]
        [AllureTag("Negative", "Boundary")]
        [AllureSeverity(SeverityLevel.minor)]
        [Description("TC05 - Missing first name should show first name error only")]
        public void Register_WithMissingFirstName_ShouldShowFirstNameError()
        {
            _registerPage.FillPersonalInfo(
                "",
                TestData.LastName,
                TestData.Address, TestData.City,
                TestData.State, TestData.ZipCode,
                TestData.Phone, TestData.SSN
            );
            _registerPage.FillCredentials(
                TestData.GenerateUsername(),
                TestData.Password,
                TestData.Password
            );
            _registerPage.ClickRegister();

            _registerPage.IsFirstNameErrorDisplayed()
                .Should().BeTrue("first name error should be visible");

            _registerPage.GetFirstNameError()
                .Should().Be("First name is required.",
                    because: "first name field was left empty");
        }

        // ─────────────────────────────────────────
        // TC06 — Missing Username
        // ─────────────────────────────────────────
        [Test]
        [AllureTag("Negative", "Boundary")]
        [AllureSeverity(SeverityLevel.minor)]
        [Description("TC06 - Missing username should show username required error")]
        public void Register_WithMissingUsername_ShouldShowUsernameError()
        {
            _registerPage.FillPersonalInfo(
                TestData.FirstName, TestData.LastName,
                TestData.Address, TestData.City,
                TestData.State, TestData.ZipCode,
                TestData.Phone, TestData.SSN
            );
            _registerPage.FillCredentials(
                "",
                TestData.Password,
                TestData.Password
            );
            _registerPage.ClickRegister();

            _registerPage.IsUsernameErrorDisplayed()
                .Should().BeTrue("username error should be visible");

            _registerPage.GetUsernameError()
                .Should().Be("Username is required.",
                    because: "username field was left empty");
        }

        // ─────────────────────────────────────────
        // TC07 — Missing Password
        // ─────────────────────────────────────────
        [Test]
        [AllureTag("Negative", "Boundary")]
        [AllureSeverity(SeverityLevel.minor)]
        [Description("TC07 - Missing password should show password required error")]
        public void Register_WithMissingPassword_ShouldShowPasswordError()
        {
            _registerPage.FillPersonalInfo(
                TestData.FirstName, TestData.LastName,
                TestData.Address, TestData.City,
                TestData.State, TestData.ZipCode,
                TestData.Phone, TestData.SSN
            );
            _registerPage.FillCredentials(
                TestData.GenerateUsername(),
                "",
                ""
            );
            _registerPage.ClickRegister();

            _registerPage.IsPasswordErrorDisplayed()
                .Should().BeTrue("password error should be visible");

            _registerPage.GetPasswordError()
                .Should().Be("Password is required.",
                    because: "password field was left empty");
        }

        // ─────────────────────────────────────────
        // TC08 — Page Title Validation
        // ─────────────────────────────────────────
        [Test]
        [AllureTag("Smoke")]
        [AllureSeverity(SeverityLevel.minor)]
        [Description("TC08 - Register page should have correct title and header")]
        public void RegisterPage_ShouldHaveCorrectTitleAndHeader()
        {
            Driver.Title
                .Should().Contain("ParaBank",
                    because: "page should belong to ParaBank");

            _registerPage.GetPageHeader()
                .Should().Contain("Signing up",
                    because: "page header should confirm this is the registration page");
        }
    }
}