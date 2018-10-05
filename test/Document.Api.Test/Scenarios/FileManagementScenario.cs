using Document.Identity.Commands;
using Document.Identity.Models;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Document.Api.Test.Features
{
    public class FileManagementScenario : BaseScenario
    {

        public FileManagementScenario(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Theory]
        [InlineData("Admin", "Admin", HttpStatusCode.NotFound)]
        [InlineData("User", "User", HttpStatusCode.Forbidden)]
        public async Task WhenUserTryToDeleteUnexistingFile_ThenCorrespondingStatusIsReturned(string username, string password, HttpStatusCode statusCode)
        {
            //Arrange Login
            var authCommand = new AuthenticateUserCommand() { Username = username, Password = password };

            //Act Login
            var userModel = await client.PostAsJsonAsync<UserModel>($"api/authentication/login", authCommand);

            //Assert Login
            Assert.NotNull(userModel);
            Assert.NotEmpty(userModel.Token);

            //Arrange Delete file
            var authorizationHeader = new Dictionary<string, string>()
            {
                {"Authorization",$"Bearer {userModel.Token}" },
            };
            var missingFileId = 0;
            //Act Delete File
            var deleteFileResponse = await client.DeleteAsync($"api/File/{missingFileId}", authorizationHeader);

            //Assert Delete File
            Assert.Equal(statusCode, deleteFileResponse.StatusCode);
        }
    }
}
