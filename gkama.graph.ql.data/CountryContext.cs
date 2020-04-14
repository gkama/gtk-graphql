using System;
using System.Text;

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
                e.HasKey(x => x.geoname_id);

                e.Property(x => x.geoname_id).HasColumnName("geoname_id").IsRequired();
                e.Property(x => x.code).HasColumnName("code").HasMaxLength(2).IsRequired();
                e.Property(x => x.name).HasColumnName("name").HasMaxLength(100).IsRequired();
                e.Property(x => x.iso_numeric).HasColumnName("iso_numeric");
                e.Property(x => x.continent).HasColumnName("continent").HasMaxLength(2).IsRequired();
                e.Property(x => x.continent_name).HasColumnName("continent_name").HasMaxLength(30).IsRequired();
                e.Property(x => x.capital).HasColumnName("capital").HasMaxLength(50).IsRequired();
                e.Property(x => x.population).HasColumnName("population").IsRequired();
                e.Property(x => x.currency_code).HasColumnName("currency_code").HasMaxLength(3).IsRequired();

                e.HasMany(x => x.neighbour_countries)
                    .WithOne()
                    .HasForeignKey(x => x.country_geoname_id)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                e.HasOne(x => x.postal_codes)
                    .WithOne()
                    .HasForeignKey<CountryPostalCode>(x => x.code)
                    .HasPrincipalKey<Country>(x => x.code)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity<CountryNeighbour>(e =>
            {
                e.HasKey(x => x.geoname_id);

                e.HasIndex(x => x.country_geoname_id);

                e.Property(x => x.geoname_id).HasColumnName("geoname_id").IsRequired();
                e.Property(x => x.code).HasColumnName("code").HasMaxLength(2).IsRequired();
                e.Property(x => x.name).HasColumnName("name").HasMaxLength(50).IsRequired();
                e.Property(x => x.country_geoname_id).HasColumnName("country_geoname_id").IsRequired();

                e.HasOne(x => x.postal_codes)
                    .WithOne()
                    .HasForeignKey<CountryPostalCode>(x => x.code)
                    .HasPrincipalKey<CountryNeighbour>(x => x.code)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity<CountryPostalCode>(e =>
            {
                e.HasKey(x => x.code);

                e.Property(x => x.code).HasColumnName("code").HasMaxLength(2).IsRequired();
                e.Property(x => x.num_postal_codes).HasColumnName("num_postal_codes").IsRequired();
                e.Property(x => x.min_postal_code).HasColumnName("min_postal_code").HasMaxLength(20).IsRequired();
                e.Property(x => x.max_postal_code).HasColumnName("max_postal_code").HasMaxLength(20).IsRequired();
            });
        }
    }
}
