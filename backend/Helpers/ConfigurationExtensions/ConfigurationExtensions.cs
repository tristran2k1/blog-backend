using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

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

        internal static IServiceCollection AddConfigAuthentication(
            this IServiceCollection services,
            string? Authority, string? ValidIssuer, string? ValidAudience, string SigningKey
            )
        {

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                options.DefaultChallengeScheme =
                options.DefaultForbidScheme =
                options.DefaultScheme =
                options.DefaultSignInScheme =
                options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.Authority = Authority;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = ValidIssuer,
                    ValidateAudience = true,
                    ValidAudience = ValidAudience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        System.Text.Encoding.UTF8.GetBytes(SigningKey)
                    )
                };
            });
            return services;
        }

        internal static IServiceCollection AddConfigSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });
            return services;
        }

        internal static IServiceCollection AddIdentities(this IServiceCollection services)
        {
            services.AddIdentity<Users, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            })
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<DataContext>();
           
            return services;
        }
    }
}
