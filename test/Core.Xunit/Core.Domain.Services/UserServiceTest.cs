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
            var request = new UserModel
            {
                Email = email,
                Password = _password
            };

            var ex = Assert.Throws<ArgumentException>(() => _userService.Login(request));

            Assert.Equal(ex.Message, messageExpected);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void DeveRetornarUmArgumentExceptionNoLoginQuandoPasswordForInvalido(string password)
        {
            const string messageExpected = "senha é obrigatório";
            var request = new UserModel
            {
                Email = _email,
                Password = password
            };

            var ex = Assert.Throws<ArgumentException>(() => _userService.Login(request));
            
            Assert.Equal(ex.Message, messageExpected);
        }

        [Fact]
        public void DeveRealizarOLoginERetornarOUsuarioLogado()
        {
            var request = new UserModel
            {
                Email = _email,
                Password = _password
            };

            _userRepositoryMock
                .Setup(r => r.Login(request))
                .Returns(new UserModel
                {
                    Id = _faker.Random.Number(1, 1000),
                    Email = _email,
                    Password = _password
                });

            var response = _userService.Login(request);

            _userRepositoryMock.Verify(r => r.Login(
               It.Is<UserModel>(
                   u => u.Email == _email &&
                       u.Password == _password
                   )));

            Assert.True(response.Id > 0);
        }

        [Fact]
        public void DeveDarErroDeUsuarioOuSenha()
        {
            const string messageExpected = "e-mail ou senha inválido";
            var request = new UserModel
            {
                Email = _email,
                Password = _password
            };

            _userRepositoryMock.Setup(r => r.Login(request));

            var response = Assert.Throws<AuthException>(() => _userService.Login(request));
            Assert.Equal(response.Message, messageExpected);
        }
    }
}