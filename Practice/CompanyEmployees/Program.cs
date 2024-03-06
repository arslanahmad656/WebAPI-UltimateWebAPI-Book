using CompanyEmployees.Extensions;
using Microsoft.AspNetCore.HttpOverrides;
using NLog;
using NLogLevel = NLog.LogLevel;

var builder = WebApplication.CreateBuilder(args);

LogManager.Setup().LoadConfiguration(builder =>
{
    var logFilePath = Path.Combine(Environment.CurrentDirectory, "logs", "logs.log");
    builder.ForLogger().FilterMinLevel(NLogLevel.Debug).WriteToFile(fileName: logFilePath);
});


builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureRepositoryManager();

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseForwardedHeaders(new()
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
