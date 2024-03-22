using Microsoft.AspNetCore.Authorization;

namespace backend.Helpers.AuthorizationHelpers
{

    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params UserRole[] roles) : base()
        {
            List<string> rolesList = [];

            foreach (var i in roles)
            {
                rolesList.Append(i.ToString());
            }

            Roles = string.Join(',', rolesList);
        }
    }
}
