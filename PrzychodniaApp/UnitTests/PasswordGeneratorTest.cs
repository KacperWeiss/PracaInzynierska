using System;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrzychodniaApp.Logics;

namespace UnitTests
{
    [TestClass]
    public class PasswordGeneratorTest
    {
        [TestMethod]
        public void ReturnsStringPassword()
        {
            // Act
            var testPassword = PasswordGenerator.GetRandomPassword();

            // Assert
            Assert.IsInstanceOfType(testPassword, typeof(string), "Zwrócone hasło nie jest typu string.");
        }

        [TestMethod]
        public void ReturnsPasswordOfSpecifiedLength()
        {
            // Arrange
            int requiredLength = 20;

            // Act
            var testPassword = PasswordGenerator.GetRandomPassword();
            
            // Assert
            Assert.AreEqual(testPassword.Length, requiredLength);
        }
        // (...)
    }
}
