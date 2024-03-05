using BusinessPublicacion.Interface;
using BusinessPublicacion.Services;
using DataAccess.Implementations;
using DataAccess.Interface;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    builder.Services.AddDbContext<PublicacionContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Conexion")));
    builder.Services.AddScoped<IPublicacionService, PublicacionService>();
    builder.Services.AddScoped<IPublicacionData, PublicacionData>();
    builder.Services.AddTransient<IAlmacenadorArchivos, AlmacenadorArchivosLocal>();
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowSpecificOrigin", builder =>
        {
            builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });
    });
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseCors("AllowSpecificOrigin");
    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}