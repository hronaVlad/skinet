using Microsoft.EntityFrameworkCore;
using API.Infrastucture.Extensions;
using EFModels;
using StackExchange.Redis;
using Microsoft.AspNetCore.Identity;
using EFModels.Entities.Identity;
using Microsoft.OpenApi.Models;
using API.Infrastucture.Middlewares;
using EFModels.Seed;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<StoreContext>(_ => _.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<AppUserContext>(_ => _.UseSqlite(builder.Configuration.GetConnectionString("IdentityConnection")));
builder.Services.AddSingleton<IConnectionMultiplexer>(_ =>
{
    var connectionString = builder.Configuration.GetConnectionString("Redis");

    var configuration = ConfigurationOptions.Parse(connectionString);

    var multiplexer = ConnectionMultiplexer.Connect(configuration);

    return multiplexer;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Shop API", Version = "v2" });

    var scheme = new OpenApiSecurityScheme
    {
        Description = "JWT Auth Scheme",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };

    options.AddSecurityDefinition("Bearer", scheme);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { scheme, new [] {"Bearer" } }
    });
});
builder.Services.AddMapper();
builder.Services.AddServices();
builder.Services.AddInvalidModelStateHandler();
builder.Services.AddIdentityServices();
builder.Services.AddAuth(builder.Configuration);
builder.Services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                });
            });
builder.Services.AddStripeKey();

var app = builder.Build();

using(var scope = app.Services.CreateAsyncScope()){
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();

    try {
        var context = services.GetRequiredService<StoreContext>();
        await context.Database.MigrateAsync();
        await StoreContextSeed.SeedAsync(context, loggerFactory);

        var identityContext = services.GetRequiredService<AppUserContext>();
        var userManager = services.GetRequiredService<UserManager<AppUser>>();
        await identityContext.Database.MigrateAsync();
        await AppUserContextSeed.SeedAsync(userManager, loggerFactory);
    }
    catch(Exception e) 
    {
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
