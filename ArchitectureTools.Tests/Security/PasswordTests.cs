using ArchitectureTools.Enums;
using ArchitectureTools.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureTools.Tests.Security
{
    public class PasswordTests
    {
        private readonly string privateKey;

        public PasswordTests()
        {
            privateKey = Guid.NewGuid().ToString();
        }

        [Fact]
        public void ShouldReturnSuccess_WhenCreateNewPassword()
        {
            string passwordContent = "Password!123";

            var password = Password.Build(passwordContent, privateKey);

            bool samePassword = password.Compare("Test!456", privateKey);

            Assert.False(samePassword);

            samePassword = password.Compare("Password!123", privateKey);

            Assert.True(samePassword);
        }

        [Fact]
        public void ShouldReturnSuccess_ForLowercase()
        {
            var lowerCaseRule = new PasswordRules();
            lowerCaseRule.Change(PasswordStrengthRule.LowercaseLetters, true);

            var failedPassword = Password.Build("TESTE", privateKey);
            var response = lowerCaseRule.Validate(failedPassword, privateKey);
            Assert.False(response.Valid);

            var successPassword = Password.Build("teste", privateKey);
            response = lowerCaseRule.Validate(successPassword, privateKey);
            Assert.True(response.Valid);
        }

        [Fact]
        public void ShouldReturnSuccess_ForUppercase()
        {
            var upperCaseRule = new PasswordRules();
            upperCaseRule.Change(PasswordStrengthRule.UppercaseLetters, true);

            var failedPassword = Password.Build("teste", privateKey);
            var response = upperCaseRule.Validate(failedPassword, privateKey);
            Assert.False(response.Valid);

            var successPassword = Password.Build("Teste", privateKey);
            response = upperCaseRule.Validate(successPassword, privateKey);
            Assert.True(response.Valid);
        }

        [Fact]
        public void ShouldReturnSuccess_ForNumber()
        {
            var numberRule = new PasswordRules();
            numberRule.Change(PasswordStrengthRule.Numbers, true);

            var failedPassword = Password.Build("teste", privateKey);
            var response = numberRule.Validate(failedPassword, privateKey);
            Assert.False(response.Valid);

            var successPassword = Password.Build("Teste236fd", privateKey);
            response = numberRule.Validate(successPassword, privateKey);
            Assert.True(response.Valid);
        }

        [Fact]
        public void ShouldReturnSuccess_ForSpecialCharacters()
        {
            var specialCharRule = new PasswordRules();
            specialCharRule.Change(PasswordStrengthRule.SpecialCharacters, true);

            var failedPassword = Password.Build("teste", privateKey);
            var response = specialCharRule.Validate(failedPassword, privateKey);
            Assert.False(response.Valid);

            var successPassword = Password.Build("Teste@!236fd", privateKey);
            response = specialCharRule.Validate(successPassword, privateKey);
            Assert.True(response.Valid);
        }

        [Fact]
        public void ShouldReturnSuccess_ForMinimumLength()
        {
            var minimumLengthRule = new PasswordRules();
            minimumLengthRule.Change(PasswordStrengthRule.MinimumLength, 10);

            var failedPassword = Password.Build("teste", privateKey);
            var response = minimumLengthRule.Validate(failedPassword, privateKey);
            Assert.False(response.Valid);

            var successPassword = Password.Build("Teste@!236fd", privateKey);
            response = minimumLengthRule.Validate(successPassword, privateKey);
            Assert.True(response.Valid);
        }

        [Fact]
        public void ShouldReturnSuccess_ForMaximumLength()
        {
            var maximumLengthRule = new PasswordRules();
            maximumLengthRule.Change(PasswordStrengthRule.MaximumLength, 5);

            var failedPassword = Password.Build("Teste@!236fd", privateKey);
            var response = maximumLengthRule.Validate(failedPassword, privateKey);
            Assert.False(response.Valid);

            var successPassword = Password.Build("teste", privateKey);
            response = maximumLengthRule.Validate(successPassword, privateKey);
            Assert.True(response.Valid);
        }

        [Fact]
        public void ShouldReturnSuccess_ForVeryWeak()
        {
            var veryWeakRule = PasswordRules.BuildVeryWeak();
            
            var failedPassword = Password.Build("562332", privateKey);
            var response = veryWeakRule.Validate(failedPassword, privateKey);
            Assert.False(response.Valid);

            var successPassword = Password.Build("Teste", privateKey);
            response = veryWeakRule.Validate(successPassword, privateKey);
            Assert.True(response.Valid);
        }

        [Fact]
        public void ShouldReturnSuccess_ForWeak()
        {
            var weakRule = PasswordRules.BuildWeak();

            var failedPassword = Password.Build("teste", privateKey);
            var response = weakRule.Validate(failedPassword, privateKey);
            Assert.False(response.Valid);

            var successPassword = Password.Build("Teste123", privateKey);
            response = weakRule.Validate(successPassword, privateKey);
            Assert.True(response.Valid);
        }

        [Fact]
        public void ShouldReturnSuccess_ForMedium()
        {
            var mediumRule = PasswordRules.BuildMedium();

            var failedPassword = Password.Build("test", privateKey);
            var response = mediumRule.Validate(failedPassword, privateKey);
            Assert.False(response.Valid);

            var successPassword = Password.Build("Teste123", privateKey);
            response = mediumRule.Validate(successPassword, privateKey);
            Assert.True(response.Valid);
        }

        [Fact]
        public void ShouldReturnSuccess_ForStrong()
        {
            var strongRule = PasswordRules.BuildStrong();

            var failedPassword = Password.Build("teste123", privateKey);
            var response = strongRule.Validate(failedPassword, privateKey);
            Assert.False(response.Valid);

            var successPassword = Password.Build("Teste123", privateKey);
            response = strongRule.Validate(successPassword, privateKey);
            Assert.True(response.Valid);
        }

        [Fact]
        public void ShouldReturnSuccess_ForVeryStrong()
        {
            var veryStrongRule = PasswordRules.BuildVeryStrong();

            var failedPassword = Password.Build("Teste123", privateKey);
            var response = veryStrongRule.Validate(failedPassword, privateKey);
            Assert.False(response.Valid);

            var successPassword = Password.Build("@Teste143@", privateKey);
            response = veryStrongRule.Validate(successPassword, privateKey);
            Assert.True(response.Valid);
        }
    }
}
