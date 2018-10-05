using MediatR;

namespace Document.Identity.Commands
{
    public class GenerateTokenCommand:IRequest<string>
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string UserRole { get; set; }

        public GenerateTokenCommand(int userId, string username,string userRole)
        {
            UserId = userId;
            Username = username;
            UserRole = userRole;
        }
    }
}
