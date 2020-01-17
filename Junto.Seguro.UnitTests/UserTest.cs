using System;
using System.Security.Cryptography.X509Certificates;
using Junto.Seguros.Domain.Users;
using Junto.Seguros.Infra.Encrypters;
using Junto.Seguros.Infra.Jwt;
using Junto.Seguros.Services.Commons;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace Junto.Seguro.UnitTests
{
    public class UserTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase("fail")]
        [TestCase("corret_login")]
        [TestCase("a_big_login_with_more_20_caracteres")]
        public void Login_Caracter_Minimal_Test(string login)
        {
            User user = new User(login,"name123","email@email.com.br");

            if(login.Length < 5)
                Assert.False(user.IsValid(), "invalid lenght was accepted");

            if(login.Length >= 5 && login.Length <= 20)
                Assert.True(user.IsValid(), "invalid lenght was accepted");

            if (login.Length > 20)
                Assert.False(user.IsValid(), "invalid lenght was accepted");

        }

        [Test]
        public void New_User_generate_Salt()
        {
            var user = new User("my_login", "my_test", "email@email.com.br");

            Assert.IsNotEmpty(user.Salt, "salt was not generated");
        }

        [Test]
        [TestCase("email#email.com.br")]
        [TestCase("email")]
        public void Invalid_email_will_be_rejected(string email)
        {
            var user = new User("my_login","my_test",email);

            Assert.False(user.IsValid(), "invalid email was accepted");
        }

        [Test]
        [TestCase("email@email.com")]
        [TestCase("email@email.io")]
        public void Valid_email_will_be_accepted(string email)
        {
            var user = new User("my_login", "my_test", email);

            Assert.True(user.IsValid(),"invalid email was accepted");

        }

        [Test]
        public void New_User_Create_Password()
        {
            var  user = new User("my_login", "my_test", "my_email");

            user.CreatePassword("Senha@123", new EncrypterService());

            Assert.True(!string.IsNullOrEmpty(user.HashPassword), "password was not encrypted");
        }

        [Test]
        [TestCase("@@@123Test")]
        public void User_Change_Password_Will_Change_Salt_And_New_HashPassword(string newPassword)
        {
            var encrypterService= new EncrypterService();
            var user = new User("my_login", "my_test", "my_email");

            user.CreatePassword("Senha@123", encrypterService);

            var oldHash = user.HashPassword;
            var oldSalt = user.Salt;

            user.ChangePassword(newPassword, "Senha@123", encrypterService);

            Assert.AreNotEqual(user.Salt,oldSalt);
            Assert.AreNotEqual(user.HashPassword,oldHash);
        }

        [Test]
        public void Token_Service_Generate_Tokens()
        {
            var secret = Guid.NewGuid().ToString();
            var options = Options.Create<JwtOptions>(new JwtOptions{ExpiryMinutes = 2,Issuer = "JuntoSeguros", SecretKey = secret});

            var jwtProvider = new JwtHandler(options);

            var token = jwtProvider.Create("my_login", "my_name");
            Assert.NotNull(token);
            Assert.IsNotEmpty(token.Token);

            var decodedToken = jwtProvider.Decode(token.Token);

            Assert.NotNull(decodedToken);
            Assert.AreEqual(decodedToken.Login,"my_login");
            Assert.AreEqual(decodedToken.Name,"my_name");

        }

        [Test]
        public void User_Change_Name()
        {
            var user = new User("my_login", "my_name", "my_email@email.com.br");

            user.ChangeName("my_new_name");

            Assert.AreNotEqual("my_name",user.Name);

            user.ChangeName("");

            Assert.AreEqual("my_new_name",user.Name);

        }

        [Test]
        public void User_was_deleted()
        {
            var user = new User("my_login", "my_name", "my_email@email.com.br");

            user.Delete();

            Assert.True(user.IsDeleted);
        }
    }
}