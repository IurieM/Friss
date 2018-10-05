using Document.Data.Entities;
using System.Collections.Generic;

namespace Document.Data.Seed
{
    public class IdentitySeed
    {
        public static IEnumerable<User> Users => new List<User>()
        {
            new User()
            {
              Username = "Admin",
              Password = "Admin",
              Role = "Admin"
            },
            new User()
            {
              Username = "User",
              Password = "User",
              Role = "User"
            }
        };
    }
}
