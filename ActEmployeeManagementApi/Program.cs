using Domain.Settings;
using API.Configuration;
using Data.Helpers.InitialDataBase;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
#region Adicionando serviços
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
var appsetings = builder.Configuration.Get<AppSettingsConfig>();
builder.Services.Configure<AppSettingsConfig>(builder.Configuration.GetSection("AppSettings"));
builder.Services.Configure<AppSettingsConfig>(builder.Configuration);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSetupDatabase(appsetings);
builder.Services.AddSetupDependenceInjection();
builder.Services.AddJwtAuthentication(appsetings);
builder.Services.AddSetupLog(appsetings);
builder.Services.AddSwaggerSetup(appsetings);
builder.Services.AddActionFilter();
#endregion

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<InitialDataBase>();
    initializer.Initialize();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerSetup(appsetings);
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
