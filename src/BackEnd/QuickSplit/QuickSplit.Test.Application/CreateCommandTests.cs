using System;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using QuickSplit.Application.Exceptions;
using QuickSplit.Application.Interfaces;
using QuickSplit.Application.Users.Commands.CreateUser;
using QuickSplit.Application.Users.Models;
using QuickSplit.Domain;
using QuickSplit.Persistence;
using Xunit;

namespace QuickSplit.Test.Application
{
    public class CreateCommandTests : CommandsTestBase
    {
        [Fact]
        public async void CreateValidUserTest()
        {
            var command = new CreateUserCommand()
            {
                Name = "John",
                LastName = "Doe",
                Password = "Password123",
                Mail = "jonny@gmail.com"
            };
            var handler = new CreateUserCommandHandler(_context);

            UserModel user = await handler.Handle(command, CancellationToken.None);

            Assert.Contains(_users, u => u.Id == user.Id);
        }
        
        [Fact]
        public async void CreateUserWithoutNameTest()
        {
            var command = new CreateUserCommand()
            {
                LastName = "Doe",
                Password = "Password123",
                Mail = "jonny@gmail.com"
            };
            var handler = new CreateUserCommandHandler(_context);

            Assert.ThrowsAny<Exception>(() => handler.Handle(command, CancellationToken.None).Result);
        }
        
        [Fact]
        public async void CreateUserWithoutLastNameTest()
        {
            var command = new CreateUserCommand()
            {
                Name = "John",
                Password = "Password123",
                Mail = "jonny@gmail.com"
            };
            var handler = new CreateUserCommandHandler(_context);

            UserModel user = await handler.Handle(command, CancellationToken.None);

            Assert.Contains(_users, u => u.Id == user.Id);
        }
        
        [Fact]
        public async void CreateUserWithoutPasswordTest()
        {
            var command = new CreateUserCommand()
            {
                Name = "John",
                LastName = "Doe",
                Mail = "jonny@gmail.com"
            };
            var handler = new CreateUserCommandHandler(_context);

            Assert.ThrowsAny<Exception>(() => handler.Handle(command, CancellationToken.None).Result);
        }
        
        [Fact]
        public async void CreateUserWithoutMailTest()
        {
            var command = new CreateUserCommand()
            {
                Name = "John",
                LastName = "Doe",
                Password = "123123"
            };
            var handler = new CreateUserCommandHandler(_context);

            Assert.ThrowsAny<Exception>(() => handler.Handle(command, CancellationToken.None).Result);
        }
    }
}