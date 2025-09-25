using Microsoft.EntityFrameworkCore;
using SistemasFuncionarios.Data.Context;
using SistemasFuncionarios.Data.Repositories;
using SistemasFuncionarios.Domain.Interfaces;
using DotNetEnv;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

var connectionString = $"Host={Environment.GetEnvironmentVariable("DB_HOST")};Database={Environment.GetEnvironmentVariable("DB_NAME")};Username={Environment.GetEnvironmentVariable("DB_USER")};Password={Environment.GetEnvironmentVariable("DB_PASSWORD")};Port={Environment.GetEnvironmentVariable("DB_PORT")}";


builder.Services.AddRazorPages();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();
builder.Services.AddScoped<IProfissaoRepository, ProfissaoRepository>();
builder.Services.AddScoped<ICursoRepository, CursoRepository>();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();


/*if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Erro ao aplicar migrações do banco de dados");
    }
}*/

app.Run();