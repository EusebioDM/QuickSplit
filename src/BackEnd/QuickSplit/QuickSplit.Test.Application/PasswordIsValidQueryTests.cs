using System.Threading;
using QuickSplit.Application.Users.Queries.GetPassword;
using QuickSplit.Domain;
using Xunit;

namespace QuickSplit.Test.Application
{
    public class PasswordIsValidQueryTests : CommandsTestBase
    {

        [Fact]
        public void HashedPasswordTrueTest()
        {
            var password = "Password123";
            string hashed = PasswordHasher.Hash(password);
            var user = new User()
            {
                Id = 1,
                Name = "John",
                Mail = "mail@gmail.com",
                Password = hashed
            };
            Users.Add(user);
            
            var query = new PasswordIsValidQuery()
            {
                Id = 1,
                Password = password
            };
            var handler = new PasswordIsValidQueryHandler(Context, PasswordHasher);
            bool result =  handler.Handle(query, CancellationToken.None).Result;

            Assert.True(result);
        }
        
        [Fact]
        public void HashedPasswordFalseTest()
        {
            var password = "Password123";
            string hashed = PasswordHasher.Hash(password);
            var user = new User()
            {
                Id = 1,
                Name = "John",
                Mail = "mail@gmail.com",
                Password = hashed
            };
            Users.Add(user);
            
            var query = new PasswordIsValidQuery()
            {
                Id = 1,
                Password = "Password124"
            };
            var handler = new PasswordIsValidQueryHandler(Context, PasswordHasher);
            bool result =  handler.Handle(query, CancellationToken.None).Result;

            Assert.False(result);
        }
        
        [Fact]
        public void NonExistantUserTest()
        {
            var password = "Password123";
            string hashed = PasswordHasher.Hash(password);

            var query = new PasswordIsValidQuery()
            {
                Id = 1,
                Password = password
            };
            var handler = new PasswordIsValidQueryHandler(Context, PasswordHasher);
            bool result =  handler.Handle(query, CancellationToken.None).Result;

            Assert.False(result);
        }
    }
}