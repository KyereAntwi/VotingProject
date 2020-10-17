using DTOs.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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
                builder => { builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin()
                    .AllowAnyHeader(); }));

            //// services for swagger
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "Application API", Version = "v1" });
                var scheme = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                };
                var security = new OpenApiSecurityRequirement()
                {
                    {scheme, new string[0] { } }
                };
                x.AddSecurityDefinition("Bearer", scheme);
                x.AddSecurityRequirement(security);
            });

            //services.AddSwaggerGen(options =>
            //{
            //    var apiinfo = new OpenApiInfo
            //    {
            //        Title = "Application API",
            //        Version = "v1",
            //        Description = "JWT Authorization header using the bearer scheme",
            //    };

            //        OpenApiSecurityScheme securityDefinition = new OpenApiSecurityScheme()
            //        {
            //            Name = "Bearer",
            //            BearerFormat = "JWT",
            //            Scheme = "bearer",
            //            Description = "Specify the authorization token.",
            //            In = ParameterLocation.Header,
            //            Type = SecuritySchemeType.Http,
            //        };

            //        OpenApiSecurityRequirement securityRequirements = new OpenApiSecurityRequirement()
            //        {
            //            { securityDefinition, new string [] { } }
            //        };

            //        options.SwaggerDoc("v1", apiinfo);
            //        options.AddSecurityDefinition("Bearer", securityDefinition);
            //        options.AddSecurityRequirement(securityRequirements);               
            //});
        }
    }
}
