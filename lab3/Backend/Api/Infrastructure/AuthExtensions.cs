using Elfo.Round.Identity;
using Elfo.Round.Identity.Impersonation;
using Elfo.Round.Identity.JWT;
using Elfo.Round.Profiler;
using Elfo.POC.PdfGallery.Api.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elfo.POC.PdfGallery.Api.Infrastructure
{
	public static class AuthExtensions
	{
		public static void AddAuthentication(this IServiceCollection service, IConfiguration configuration)
		{

			switch (configuration["Authentication:Type"])
			{
				case "JWT":
					service.AddJWTAuthentication(configuration);
					break;
				case "Windows":
					service.AddWindowsAuthentication(configuration);
					break;
				default:
					break;
			}
		}

		private static void AddJWTAuthentication(this IServiceCollection service, IConfiguration configuration)
		{
            service
				.AddAuthentication(auth =>
				{
					auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
					auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				})
				.AddJwtBearer(x =>
				{
					x.RequireHttpsMetadata = false;
					x.TokenValidationParameters = new TokenValidationParameters
					{
						ClockSkew = TimeSpan.Zero,
						RequireExpirationTime = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(configuration["Authentication:AuthSecret"])),
						ValidateIssuer = false,
						ValidateAudience = false
					};
					x.Events = new JwtBearerEvents
					{
						OnAuthenticationFailed = context =>
						{
							if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
								context.Response.Headers.Add("Token-Expired", "true");

							return Task.CompletedTask;
						}
					};
				})
				.AddRoundIdentity(authOptions =>
				{
					authOptions.HowToObtainUsernameFromIdentityName = identityName => identityName;
				})
                .WithJWTGeneration(jwtCreation =>
				{
					jwtCreation.Secret = configuration["Authentication:AuthSecret"];
				})
                .WithImpersonation(o =>
                {
                    o.Secret = configuration["Authentication:AuthSecret"];
                    o.KeepTheseClaimsDuringImpersonation = (claims) =>
                    {
                        return claims.ToList().Where(t => t.Type == RoundIdentityClaimType.Permission &&
                                                          t.Value == ProfilerPermissionClaim.CanProfile);
                    };
                })
				.WithMsSqlServer<UserInformationModel>(msSqlOption =>
				{
					msSqlOption.ConnectionString = configuration.GetConnectionString("Db");
					msSqlOption.ProfileConnection = true;
				});
		}

		private static void AddWindowsAuthentication(this IServiceCollection service, IConfiguration configuration)
		{
			service
				.AddAuthentication(IISDefaults.AuthenticationScheme)
				.AddRoundIdentity(authOptions =>
				{
					authOptions.HowToObtainUsernameFromIdentityName = identityName => identityName;
				})
				.WithImpersonation(o =>
				{
					o.Secret = configuration["Authentication:AuthSecret"];
                    o.KeepTheseClaimsDuringImpersonation = (claims) =>
                    {
                        return claims.ToList().Where(t => t.Type == RoundIdentityClaimType.Permission && 
                                                          t.Value == ProfilerPermissionClaim.CanProfile);
                    };
				})
				.WithMsSqlServer<UserInformationModel>(msSqlOption =>
				{
					msSqlOption.ConnectionString = configuration.GetConnectionString("Db");
					msSqlOption.ProfileConnection = true;
				});
		}

		public static void AddSwaggerAuthentication(this SwaggerGenOptions swaggerGenOptions, IConfiguration configuration)
        {
            switch (configuration["Authentication:Type"])
            {
                case "JWT":
                    swaggerGenOptions.AddSwaggerJWTAuthentication();
                    break;
                default:
                    break;
            }
        }

        private static void AddSwaggerJWTAuthentication(this SwaggerGenOptions swaggerGenOptions)
		{
			swaggerGenOptions.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
			{
				In = ParameterLocation.Header,
				Description = "Please insert JWT with Bearer into field",
				Name = "Authorization",
				Type = SecuritySchemeType.ApiKey,
				Scheme = "Bearer"
			});
			swaggerGenOptions.AddSecurityRequirement(new OpenApiSecurityRequirement()
			{
				{
					new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference
						{
							Type = ReferenceType.SecurityScheme,
							Id = "Bearer"
						},
						Scheme = "oauth2",
						Name = "Bearer",
						In = ParameterLocation.Header
					},
					new List<string>()
				}
			});
		}
	}
}
