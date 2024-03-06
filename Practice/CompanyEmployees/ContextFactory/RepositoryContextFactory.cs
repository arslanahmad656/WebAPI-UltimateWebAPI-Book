using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Repository;

namespace CompanyEmployees.ContextFactory
{
    public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
    {
        public RepositoryContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("sqlConnection");

            var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>()
                .UseSqlServer(connectionString, b => b.MigrationsAssembly("CompanyEmployees"));

            var repoContext = new RepositoryContext(optionsBuilder.Options);
            return repoContext;
        }
    }
}
