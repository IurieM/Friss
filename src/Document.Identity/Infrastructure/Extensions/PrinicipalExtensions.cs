using Document.Common;
using Document.Common.Exceptions;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;

namespace Document.Identity.Infrastructure.Extensions
{
    public static class PrinicipalExtensions
    {
        public static int UserId(this IIdentity identity)
        {
            var id = identity as ClaimsIdentity;
            var claim = id.FindFirst(ClaimTypes.Sid);

            if (int.TryParse(claim.Value, out int userId))
            {
                return userId;
            }

            throw new AppException(Constants.ErrorCodes.InvalidCredentials, HttpStatusCode.Unauthorized);
        }
    }
}
