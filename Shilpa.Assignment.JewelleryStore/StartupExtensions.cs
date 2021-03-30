using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Shilpa.Assignment.Database.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Shilpa.Assignment.JewelleryStore
{
    public static class StartupExtensions
    {
        public static DbContextOptionsBuilder EnableSensitiveDataLoggingPerEnviroment(this DbContextOptionsBuilder builder)
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIROMENT")?.ToLower() == "development")
            {
                builder.EnableSensitiveDataLogging();
            }
            else
            {
                builder.EnableSensitiveDataLogging(false);
            }
            return builder;
        }
        public static IServiceCollection AddEFContexts(this IServiceCollection services,IConfiguration configuration)
        {
            var config = configuration.GetSection("Connnectionstrings:JewelleryDBConnection").Value;
            services.AddDbContext<JewelleryContext>(options => options.UseSqlServer(config, opt => opt.MigrationsAssembly("Shilpa.Assignment.Database").EnableRetryOnFailure())
               .EnableSensitiveDataLoggingPerEnviroment());
            return services;
        }
        public static IServiceCollection AddTokenAuthentication(this IServiceCollection services, IConfiguration config)
        {
           
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new RsaSecurityKey(new RSACryptoServiceProvider(2048).ExportParameters(true)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "localhost",
                    ValidAudience = "localhost"
                };
            });

            return services;
        }
    }
}
