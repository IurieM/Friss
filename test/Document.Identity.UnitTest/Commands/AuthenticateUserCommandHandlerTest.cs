using Document.Common;
using Document.Common.Exceptions;
using Document.Data.DbContexts;
using Document.Data.Entities;
using Document.Identity.Commands;
using Document.Identity.Test.Infrastructure;
using Moq;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Document.Identity.Test.Commands
{
    public class AuthenticateUserCommandHandlerTest
    {
        private AuthenticateUserCommandHandler authenticateUserCommandHandler;
        private Mock<IDocumentDbContext> dbContextMock;

        public AuthenticateUserCommandHandlerTest()
        {
            dbContextMock = new Mock<IDocumentDbContext>();
            authenticateUserCommandHandler = new AuthenticateUserCommandHandler(dbContextMock.Object);
        }

        [Fact]
        public async Task WhenUserCredentialsAreWrong_ThenUnauthorizedExceptionIsThrown()
        {
            //Arrange
            var users = new List<User>() { new User() { Username = "Bob", Password = "Bob" } };
            dbContextMock.Setup(x => x.Users).Returns(users.ToDbSet());

            //Act
            var command = new AuthenticateUserCommand() { Username = "Ana", Password = "Ana" };
            var exception = await Assert.ThrowsAsync<AppException>(() => authenticateUserCommandHandler.Handle(command, new CancellationToken()));

            //Assert
            Assert.Equal(HttpStatusCode.Unauthorized, exception.StatusCode);
            Assert.Equal(Constants.ErrorCodes.InvalidCredentials, exception.Message);
        }

        [Fact]
        public async Task WhenUserCredentialsAreCorrect_ThenUserModelIsReturned()
        {
            //Arrange
            var user = new User() { Id = 1, Username = "Bob", Password = "Bob", Role = "Admin" };
            var users = new List<User>() { user };
            dbContextMock.Setup(x => x.Users).Returns(users.ToDbSet());

            //Act
            var command = new AuthenticateUserCommand() { Username = "Bob", Password = "Bob" };
            var userModel = await authenticateUserCommandHandler.Handle(command, new CancellationToken());

            //Assert
            Assert.Equal(user.Id, userModel.Id);
            Assert.Equal(user.Role, userModel.Role);
            Assert.Equal(user.Username, userModel.Username);
        }
    }
}
