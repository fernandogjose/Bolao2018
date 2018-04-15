using System;
using Bogus;
using Core.Data.Repositories;
using Core.Domain.Exceptions;
using Core.Domain.Interfaces.Repositories;
using Core.Domain.Models;
using Core.Domain.Services;
using Core.Domain.Validations;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace Core.Xunit.Core.Domain.Services
{
    public class UserServiceTest
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;

        private readonly UserService _userService;

        private readonly IServiceCollection _serviceCollection;

        private readonly Faker _faker;

        private int _id;

        private string _email;

        private string _password;

        public UserServiceTest()
        {
            //--- configuração do DI
            _serviceCollection = new ServiceCollection();
            _userRepositoryMock = new Mock<IUserRepository>();
            _serviceCollection.AddSingleton<IUserRepository>(_userRepositoryMock.Object);
            _serviceCollection.AddSingleton<UserValidation>();
            _serviceCollection.AddSingleton<UserService>();

            var services = _serviceCollection.BuildServiceProvider();
            _userService = services.GetService<UserService>();

            //--- Dados Fake
            _faker = new Faker();
            _id = _faker.Random.Number(1, 100);
            _email = _faker.Person.Email;
            _password = _faker.Random.AlphaNumeric(8);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void DeveRetornarUmArgumentExceptionNoLoginQuandoEmailForInvalido(string email)
        {
            const string messageExpected = "e-mail é obrigatório";

            var ex = Assert.Throws<ArgumentException>(() => _userService.Login(email, _password));

            Assert.Equal(ex.Message, messageExpected);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void DeveRetornarUmArgumentExceptionNoLoginQuandoPasswordForInvalido(string password)
        {
            const string messageExpected = "senha é obrigatório";

            var ex = Assert.Throws<ArgumentException>(() => _userService.Login(_email, password));
            
            Assert.Equal(ex.Message, messageExpected);
        }

        [Fact]
        public void DeveRealizarOLoginERetornarOUsuarioLogado()
        {
            _userRepositoryMock
                .Setup(r => r.Login(_email, _password))
                .Returns(new UserModel
                {
                    Id = _faker.Random.Number(1, 1000),
                    Email = _email,
                    Password = _password
                });

            var response = _userService.Login(_email, _password);

            _userRepositoryMock.Verify(r => r.Login(
               It.Is<string>(u => u == _email), 
               It.Is<string>(u => u == _password)));

            Assert.True(response.Id > 0);
        }

        [Fact]
        public void DeveDarErroDeUsuarioOuSenha()
        {
            const string messageExpected = "e-mail ou senha inválido";

            _userRepositoryMock.Setup(r => r.Login(_email, _password));

            var response = Assert.Throws<AuthException>(() => _userService.Login(_email, _password));
            Assert.Equal(response.Message, messageExpected);
        }
    }
}