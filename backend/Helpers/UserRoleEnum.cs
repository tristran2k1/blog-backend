namespace backend.Helpers
{
    public enum UserRole
    {
        USER,
        ADMIN
    }

    static class UserRoleExtensions
    {
        public static string ToString(this UserRole role)
        {
            switch (role)
            {
                case UserRole.ADMIN:
                    return "Admin";

                case UserRole.USER:
                default:
                    return "User";
            }
        }
    }
}
