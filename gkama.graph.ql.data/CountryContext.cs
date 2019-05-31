using Microsoft.EntityFrameworkCore;

namespace gkama.graph.ql.data
{
    public class CountryContext : DbContext
    {
        public CountryContext(DbContextOptions<CountryContext> options)
            : base(options)
        { }

        public virtual DbSet<Country> countries { get; set; }
        public virtual DbSet<CountryNeighbour> neighbour_countries { get; set; }
        public virtual DbSet<CountryPostalCode> postal_codes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>(e =>
            {
                e.HasKey(p => p.geoname_id);

                e.Property(p => p.geoname_id).IsRequired();
                e.Property(p => p.code).IsRequired();

                e.Property(p => p.continent).HasMaxLength(2);
                e.Property(p => p.currency_code).HasMaxLength(3);

                e.HasMany(p => p.neighbour_countries)
                    .WithOne()
                    .HasForeignKey(p => p.country_geoname_id)
                    .IsRequired();

                e.HasOne(p => p.postal_codes)
                    .WithOne()
                    .HasForeignKey<CountryPostalCode>(p => p.code)
                    .HasPrincipalKey<Country>(p => p.code)
                    .IsRequired();
            });

            modelBuilder.Entity<CountryNeighbour>(e =>
            {
                e.HasKey(p => p.geoname_id);
            });

            modelBuilder.Entity<CountryPostalCode>(e =>
            {
                e.HasKey(p => p.code);
            });
        }
    }
}
