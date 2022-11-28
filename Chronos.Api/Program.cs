using Chronos.Api.Filters;
using Chronos.Api.Handlers;
using Chronos.Domain.Settings;
using Chronos.IoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();

var connectionString = builder.Configuration.GetConnectionString("ChronosDb");

#region Filters

builder.Services
    .AddControllers(options =>
    {
        options.Filters.Add(typeof(ExceptionFilter));
    })
    .AddJsonOptions(
        options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
    );
;
#endregion

#region HttpContext
builder.Services.AddHttpContextAccessor();
#endregion

#region Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(
        "v1",
        new OpenApiInfo()
        {
            Title = "⌛ Chronos ⌛",
            Version = "v1",
            Description = "Solução para gestão de horas em projetos Raro Labs.",
            Contact = new OpenApiContact()
            {
                Name = "Repositório",
                Url = new Uri("https://gitlab.com/gloinho/chronos")
            },
        }
    );

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
<<<<<<< HEAD

    var xmlFile = "Chronos.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    c.IncludeXmlComments(xmlPath);
=======
>>>>>>> origin/main
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

builder.Services.AddAuthorization(options =>
{
    options.InvokeHandlersAfterFailure = true;
}).AddSingleton<IAuthorizationMiddlewareResultHandler, AuthorizationHandler>();

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
