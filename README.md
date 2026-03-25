# ParaBank Selenium Test Automation

Automated UI test suite for [ParaBank](https://parabank.parasoft.com/) using Selenium WebDriver with C# and NUnit.

---

##  Tech Stack

| Tool | Purpose |
|---|---|
| C# (.NET 8) | Programming Language |
| Selenium WebDriver | UI Automation |
| NUnit | Test Framework |
| FluentAssertions | Assertion Library |
| WebDriverManager | Auto ChromeDriver Management |
| Allure Reports | Test Reporting |
| Git + GitHub | Version Control |

---

## Project Structure
```
ParaBankTests.Selenium/
├── Base/
│   └── BaseTest.cs              # Driver setup and teardown
│ 	├── BasePage.cs              # Common page actions
├── Config/
│   └── TestSettings.cs          # Central configuration
├── Helpers/
│   ├── TestData.cs              # Test data constants
│   ├── WaitHelper.cs            # Explicit wait utilities
│   └── ScreenshotHelper.cs      # Screenshot on failure
├── Pages/
│   ├── LoginPage.cs             # Login page object
│   └── RegisterPage.cs          # Register page object
├── Tests/
│   ├── LoginTests.cs            # Login test scenarios
│   └── RegisterTests.cs         # Registration test scenarios
├── allureConfig.json            # Allure configuration
└── ParaBankTests.Selenium.csproj
```

---

##  Test Scenarios

### Login Tests
| Test | Description | Type |
|---|---|---|
| TC01 | Valid credentials should login successfully | Smoke |
| TC02 | Empty credentials should show error | Negative |
| TC03 | Empty username should show error | Negative |
| TC04 | Empty password should show error | Negative |
| TC05 | Invalid passsword should show error | Negative |
| TC06 | Invalid username should show error | Negative |
| TC07 | Invalid credentials should show error | Negative |
| TC08 | SQL injection attempt should not bypass login | Security |

### Registration Tests
| Test | Description | Type |
|---|---|---|
| TC01 | Valid details should register successfully | Smoke |
| TC02 | Empty form should show all required errors | Negative |
| TC03 | Mismatched passwords should show error | Negative |
| TC04 | Duplicate username should show error | Negative |
| TC05 | Missing first name should show error | Negative |
| TC06 | Missing username should show error | Negative |
| TC07 | Missing password should show error | Negative |
| TC08 | Page title and header validation | Smoke |

---

##  Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Google Chrome](https://www.google.com/chrome/) (latest)
- [Git](https://git-scm.com/)
- [Allure CLI](https://allurereport.org/docs/install/)

---

##  Getting Started

### 1. Clone the Repository
```bash
git clone https://github.com/lakshmianbarasan24/parabank-selenium-tests.git
cd parabank-selenium-tests
```

### 2. Restore Dependencies
```bash
dotnet restore
```

### 3. Build the Project
```bash
dotnet build
```

### 4. Run All Tests
```bash
dotnet test
```

### 5. Run Specific Category
```bash
# Login tests only
dotnet test --filter Category=Login

# Registration tests only
dotnet test --filter Category=Registration
```

---

##  Allure Reports

### Install Allure CLI
```bash
# Using Scoop (Windows)
scoop install allure
```

### Generate & View Report
```bash
# Run tests first
dotnet test

# Serve report
allure serve allure-results
```

### Report Sections
- **Overview** — Pass/Fail summary with charts
- **Suites** — Tests grouped by class
- **Graphs** — Pass rate and duration trends
- **Timeline** — Test execution timeline
- **Categories** — Failures grouped by type

---

##  Automation Design

### Page Object Model
Each page has its own class with locators and actions:
```
BasePage       ← shared actions (Click, Type, Find)
  ├── LoginPage
  └── RegisterPage
```

### Synchronization Strategy
- **Implicit Wait** — 10 seconds (global)
- **Explicit Wait** — 15 seconds (element level)
- **Page Load Wait** — 30 seconds
- **Generic WaitHelper** — custom conditions

### Test Independence
- Fresh browser instance per test via `[SetUp]`
- Unique usernames generated per run
- No shared state between tests
- Auto-registers user before Login tests via `[OneTimeSetUp]`

### Screenshot on Failure
- Automatically captured on test failure
- Stored in `Screenshots/` folder

---

##  Configuration

### `Config/TestSettings.cs`
```csharp
public static class TestSettings
{
    public const string BaseUrl         = "https://parabank.parasoft.com/";
    public const string RegisterUrl     = "https://parabank.parasoft.com/parabank/register.htm";
    public const string LoginUrl        = "https://parabank.parasoft.com/parabank/login.htm";
    public const int    ImplicitWait    = 10;
    public const int    ExplicitWait    = 15;
    public const int    PageLoadTimeout = 30;
}
```

### Run in Headless Mode (CI)
Uncomment in `BaseTest.cs`:
```csharp
options.AddArgument("--headless=new");
```

---

##  Key Dependencies
```xml
<PackageReference Include="Selenium.WebDriver"       Version="4.27.0" />
<PackageReference Include="WebDriverManager"         Version="2.17.4" />
<PackageReference Include="NUnit"                    Version="4.2.2"  />
<PackageReference Include="NUnit3TestAdapter"        Version="4.6.0"  />
<PackageReference Include="Microsoft.NET.Test.Sdk"  Version="17.12.0"/>
<PackageReference Include="FluentAssertions"         Version="6.12.1" />
<PackageReference Include="Allure.NUnit"             Version="2.12.1" />
```

---

##  Author

**Lakshmi Anbarasan**
- GitHub: [@lakshmianbarasan24](https://github.com/lakshmianbarasan24)

---

##  License

This project is for assessment and educational purposes only.
