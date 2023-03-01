using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace MediaManager.Data.Factories
{
    public class MediaManagerContextFactory : IDesignTimeDbContextFactory<MediaManagerContext>
    {
        public MediaManagerContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            return new MediaManagerContext(new DbContextOptionsBuilder<MediaManagerContext>().Options, config);
        }
    }
}
