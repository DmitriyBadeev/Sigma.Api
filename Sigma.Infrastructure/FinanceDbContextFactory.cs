using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Sigma.Infrastructure
{
    public class FinanceDbContextFactory : IDesignTimeDbContextFactory<FinanceDbContext>
    {
        public FinanceDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<FinanceDbContext>();
            var connectionString = "User ID=postgres;Password=123;Host=localhost;Port=5432;Database=investin;Pooling=true;Min Pool Size=0;Max Pool Size=100;Connection Lifetime=0;";

            builder.UseNpgsql(connectionString);

            return new FinanceDbContext(builder.Options);
        }
    }
}