namespace backend.Helpers
{
    public class ConvertEnum
    {
        public static UserRole GetRole(string? role)
        {
            if (role == null)
                return UserRole.USER;
            switch (role.ToUpper())
            {
                case "ADMIN":
                    return UserRole.ADMIN;

                case "USER":
                default:
                    return UserRole.USER;

            }
        }
    }
}
