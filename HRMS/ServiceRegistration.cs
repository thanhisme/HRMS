using HRMS.Middlewares;
using Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text;
using Utils.Constants.Strings;
using Utils.Filters;
using Utils.HttpResponseModels;
using Utils.Swagger;

namespace HRMS
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterAppServices(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.RegisterInfrastructureServices(configuration);

            services.RegisterFilters();

            services.RegisterAuthentication(configuration);

            services.RegisterSwagger();

            services.RegisterClaimTransformation();

            services.RegisterAuthorization();

            return services;
        }

        private static IServiceCollection RegisterAuthentication(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(
                                configuration.GetSection("AppSetting:JwtSecretKey").Value!
                            )
                        )
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnChallenge = (context) =>
                        {
                            context.HandleResponse();

                            throw new AppException(HttpStatusCode.Unauthorized, HttpExceptionMessages.UNAUTHORIZED);
                        },
                        OnForbidden = (context) =>
                        {
                            throw new AppException(HttpStatusCode.Forbidden, HttpExceptionMessages.FORBIDDEN);
                        }
                    };
                });

            return services;
        }

        private static IServiceCollection RegisterClaimTransformation(this IServiceCollection services)
        {
            services.AddTransient<IClaimsTransformation, ClaimsTransformer>();

            return services;
        }

        private static IServiceCollection RegisterAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Permission:Admin", policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        var claims = context.User.Claims.ToList();
                        return context.User.Claims.Any(c => c.Type.StartsWith("Permission:Admin"));
                    });
                });
            });

            return services;
        }
    }
}
