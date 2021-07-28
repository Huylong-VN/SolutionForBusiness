using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace SolutionForBusiness.Data.EF
{
    public class SolutionDBContextFactory : IDesignTimeDbContextFactory<SolutionDbContext>
    {
        public SolutionDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsetting.json")
                 .Build();
            var connectionString = configuration.GetConnectionString("SolutionForBusinessDb");
            var optionsBuider = new DbContextOptionsBuilder<SolutionDbContext>();
            optionsBuider.UseSqlServer(connectionString);
            return new SolutionDbContext(optionsBuider.Options);
        }
    }
}