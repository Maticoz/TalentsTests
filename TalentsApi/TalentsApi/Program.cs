using NLog.Web;
using Microsoft.Extensions.Options;
using TalentsApi.Installers;
using TalentsApi.Middleware;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder();

// NLog: Setup NLog for Dependency injection
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();

/******************* Start Services *********************/
var serviceInstallers = typeof(Program).Assembly.ExportedTypes.Where(x => typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
    .Select(Activator.CreateInstance)
    .Cast<IInstaller>()
    .ToList();

serviceInstallers.ForEach(installer => installer.InstallServices(builder.Services, builder.Configuration));
/******************* Start Services*********************/

/******************* OTHER *********************/
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen();
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
/******************* OTHER *********************/

var app     = builder.Build();
SeederInstaller.Seed(app);

app.UseResponseCaching(); // caching files EX. file.cs
app.UseStaticFiles(); // for static files folder wwwroot
app.UseCors("FrontEndClient");
app.UseRequestLocalization(app.Services.GetService<IOptions<RequestLocalizationOptions>>().Value);

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "4Talents API");
    });
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<RequestTimeMiddleware>();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

public partial class Program { }
