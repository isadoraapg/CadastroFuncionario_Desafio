using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SistemasFuncionarios.Data.Context
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            
            var host = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
            var database = Environment.GetEnvironmentVariable("DB_NAME") ?? "postgres";
            var username = Environment.GetEnvironmentVariable("DB_USER") ?? "postgres";
            var password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "";
            var port = Environment.GetEnvironmentVariable("DB_PORT") ?? "5432";
            
            var connectionString = $"Host={host};Database={database};Username={username};Password={password};Port={port}";
            
            optionsBuilder.UseNpgsql(connectionString);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}