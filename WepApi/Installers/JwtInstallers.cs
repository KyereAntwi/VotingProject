using DTOs.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Text;

namespace WepApi.Installers
{
    public class JwtInstallers : IInstaller
    {
        public void InstallServices(IConfiguration configuration, IServiceCollection services)
        {
            var jwtSettings = new JwtSettings();
            configuration.Bind(nameof(jwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);

            var tokenValidationParameter = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = true
            };

            services.AddSingleton(tokenValidationParameter);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x => 
                {
                    x.SaveToken = true;
                    x.TokenValidationParameters = tokenValidationParameter;
                });

            services.AddCors(
                options => options
                .AddPolicy("CorsPolicy",
                builder => { builder.AllowAnyMethod().AllowAnyHeader().WithOrigins("*").AllowCredentials(); }));

            //// services for swagger
            //services.AddSwaggerGen(x =>
            //{
            //    x.SwaggerDoc("V1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Application APi", Version = "V1" });
            //    var security = new Dictionary<string, IEnumerable<string>>
            //    {
            //        { "Bearer", new string[0] }
            //    };
            //    x.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme 
            //    {
            //        Description = "JWT Authorization",
            //        Name = "Authorization",
            //        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            //        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
            //    });
            //    x.AddSecurityRequirement(security);
            //});
        }
    }
}
