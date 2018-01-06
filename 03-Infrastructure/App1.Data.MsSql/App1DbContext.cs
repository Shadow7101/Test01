using App1.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App1.Data.MsSql
{
    public class App1DbContext : DbContext
    {
/*
dotnet ef migrations add Initialize_database --startup-project ..\..\01-FrontEnd\App1.WebApi\App1.WebApi.csproj

dotnet ef database update --startup-project ..\..\01-FrontEnd\App1.WebApi\App1.WebApi.csproj

dotnet ef database drop  --startup-project ..\..\01-FrontEnd\App1.WebApi\App1.WebApi.csproj

dotnet ef migrations remove  --startup-project ..\..\01-FrontEnd\App1.WebApi\App1.WebApi.csproj
 */

        public App1DbContext(DbContextOptions options) : base(options) { }

        public DbSet<Gender> Gender { get; set; }
        public DbSet<Log> Log { get; set; }
        public DbSet<LogType> LogType { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Token> Token { get; set; }
        public DbSet<TokenType> TokenType { get; set; }
        public DbSet<User> User { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.ApplyConfiguration(new Map.GenderMap());
            builder.ApplyConfiguration(new Map.LogMap());
            builder.ApplyConfiguration(new Map.LogTypeMap());
            builder.ApplyConfiguration(new Map.RoleMap());
            builder.ApplyConfiguration(new Map.TokenMap());
            builder.ApplyConfiguration(new Map.TokenTypeMap());
            builder.ApplyConfiguration(new Map.UserMap());
        }
    }
}
