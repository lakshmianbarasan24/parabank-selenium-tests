using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParaBankTests.Selenium.Helpers
{
    public static class TestData
    {
        // Valid user
        public const string ValidUsername = "john";
        public const string ValidPassword = "demo";

        // Invalid credentials
        public const string InvalidUsername = "wronguser";
        public const string InvalidPassword = "wrongpass";

        //Empty credentials
        public const string EmptyUsername = "";
        public const string EmptyPassword = "";

        // Registration data
        public const string FirstName = "Test";
        public const string LastName = "User";
        public const string Address = "123 Main St";
        public const string City = "Boston";
        public const string State = "MA";
        public const string ZipCode = "02101";
        public const string Phone = "6171234567";
        public const string SSN = "123-45-6789";
        public const string Password = "Test@1234";

        // Generate unique username to avoid duplicate conflicts
        public static string GenerateUsername() =>
            $"user_{DateTime.Now:yyyyMMddHHmmss}";
    }
}
