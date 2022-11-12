using Chronos.Domain.Settings;
using Chronos.IoC;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("ChronosDb");

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

NativeInjectorBootStrapper.RegisterAppDependenciesContext(builder.Services, connectionString);

var appSetting = builder.Configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();
builder.Services.AddSingleton(appSetting);

var mailSetting = builder.Configuration.GetSection(nameof(EmailSettings)).Get<EmailSettings>();
builder.Services.AddSingleton(mailSetting);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
