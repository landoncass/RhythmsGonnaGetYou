using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace RhythmsGonnaGetYou
{
    public class RhythmsGonnaGetYouContext : DbContext
    {
        public DbSet<Band> Bands { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Song> Songs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("server=localhost;database=Rhythmdb");
            // var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            // optionsBuilder.UseLoggerFactory(loggerFactory);
        }

        public Band FindOneBand(string nameToFind)
        {
            Band foundBand = Bands.FirstOrDefault(Band => Band.Name.ToUpper().Contains(nameToFind.ToUpper()));
            // .Include(band => band.Albums)
            return foundBand;
        }
    }
}