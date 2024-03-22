namespace backend.Helpers.ConfigurationExtensions
{
    internal static class ConfigurationExtensions
    {
        internal static IServiceCollection AddPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdministratorRequirement", p => p.RequireRole(UserRole.ADMIN.ToString(), UserRole.USER.ToString()));
                options.AddPolicy("NormalUser", p => p.RequireRole(UserRole.USER.ToString()));
            });
            return services;
        }
    }
}
