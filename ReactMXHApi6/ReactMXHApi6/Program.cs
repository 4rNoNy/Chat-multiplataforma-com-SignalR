using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReactMXHApi6;
using ReactMXHApi6.Core.Entities;
using ReactMXHApi6.Extensions;
using ReactMXHApi6.Infrastructure.Data;
using ReactMXHApi6.Middleware;
using ReactMXHApi6.SignalR;

// Define uma pol�tica de CORS permitindo solicita��es apenas a partir de "http://localhost:3000"
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:3000")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod()
                                      .AllowCredentials();
                      });
});

builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddSignalR();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else if (app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

// Mapeia os controladores da API
app.MapControllers();

// Realiza migra��es e popula o banco de dados com usu�rios iniciais
using (var scope = app.Services.CreateScope())
{
    try
    {
        var context = scope.ServiceProvider.GetRequiredService<DataContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

        // Executa as migra��es pendentes no banco de dados
        await context.Database.MigrateAsync();

        // Popula o banco de dados com usu�rios iniciais
        await Seed.SeedUsers(userManager, context);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "Ocorreu um erro durante a migra��o");
    }
}

// Mapeia o hub de presen�a
app.MapHub<PresenceHub>("hubs/presence");

// Mapeia o hub de mensagens
app.MapHub<MessageHub>("hubs/message");

// Inicia a aplica��o
await app.RunAsync();
