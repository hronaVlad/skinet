using API.Infrastucture.Errors.Middlewares;
using EFModels.Data;
using Microsoft.EntityFrameworkCore;
using API.Infrastucture.Extensions;
using API.Mappers;
using EFModels;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<StoreContext>(_ => _.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSingleton<IConnectionMultiplexer>(_ =>
{
    var connectionString = builder.Configuration.GetConnectionString("Redis");

    var configuration = ConfigurationOptions.Parse(connectionString);

    var multiplexer = ConnectionMultiplexer.Connect(configuration);

    return multiplexer;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfiles));
builder.Services.AddApplicationServices();
builder.Services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                });
            });

var app = builder.Build();

using(var scope = app.Services.CreateAsyncScope()){
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();

    try {
        var context = services.GetRequiredService<StoreContext>();
        await context.Database.MigrateAsync();
        await StoreContextSeed.SeedAsync(context, loggerFactory);
    }
    catch(Exception e) {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(e, "An error occured during migration");
    }
}


// Configure the HTTP request pipeline.

app.UseMiddleware<ErrorMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
