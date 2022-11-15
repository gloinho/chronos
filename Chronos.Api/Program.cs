using Chronos.Api.Filters;
using Chronos.Api.Handlers;
using Chronos.Domain.Settings;
using Chronos.IoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();

var connectionString = builder.Configuration.GetConnectionString("ChronosDb");

#region Filters

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ExceptionFilter));
});
#endregion

#region HttpContext
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
#endregion

#region Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description =
                "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer'[space] and then your token in the text input below. \r\n\r\nExample: \"Bearer {token}\"",
        }
    );
    c.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        }
    );
});

#endregion

#region Bootstraper
NativeInjectorBootStrapper.RegisterAppDependenciesContext(builder.Services, connectionString);
NativeInjectorBootStrapper.RegisterAppDependencies(builder.Services);
#endregion

#region AppSettings
var appSetting = builder.Configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();
builder.Services.AddSingleton(appSetting);
#endregion

#region MailSettings
var mailSetting = builder.Configuration.GetSection(nameof(EmailSettings)).Get<EmailSettings>();
builder.Services.AddSingleton(mailSetting);
#endregion

#region Jwt
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    {
        option.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(appSetting.SecurityKey)
            ),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
#endregion

#region Authorization

builder.Services
    .AddAuthorization(options =>
    {
        options.InvokeHandlersAfterFailure = true;
    })
    .AddSingleton<IAuthorizationMiddlewareResultHandler, AuthorizationHandler>();

#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
