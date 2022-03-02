using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Model
{
    public class dbcontext:DbContext
    {

        public static IConfigurationRoot configuration;


        public DbSet<Afdeling> Afdelingen { get; set; }
        public DbSet<Medewerker> Medewerkers { get; set; }


        public dbcontext()
        {

        }

        public dbcontext(DbContextOptions<dbcontext> options) : base(options)
        {

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (optionsBuilder.IsConfigured == false)
            {
                configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                    .AddJsonFile("appsettings.json")
                    .Build();

                var connectionString = configuration.GetConnectionString("eftph");

                if (connectionString != null)
                {
                    optionsBuilder
                        .UseSqlServer(connectionString, op => op.MaxBatchSize(150))
                        .UseLazyLoadingProxies();

                }



            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // configure
            modelBuilder.Entity<Afdeling>().ToTable("Afdelingen");

            modelBuilder.Entity<Afdeling>().HasKey(r => r.AfdelingId);

            modelBuilder.Entity<Afdeling>().Property(r => r.AfdelingId).ValueGeneratedOnAdd();

            modelBuilder.Entity<Afdeling>().Property(r => r.AfdelingCode).IsRequired().HasMaxLength(4); ;
            modelBuilder.Entity<Afdeling>().HasIndex(r => r.AfdelingCode).IsUnique();

            modelBuilder.Entity<Afdeling>().Property(r => r.AfdelingNaam).IsRequired().HasMaxLength(50); ;
            modelBuilder.Entity<Afdeling>().HasIndex(r => r.AfdelingNaam).IsUnique();

            modelBuilder.Entity<Afdeling>().Property(r => r.AfdelingTekst).IsRequired(false).HasMaxLength(255);


            modelBuilder.Entity<Medewerker>().ToTable("Medewerkers");

            // migratie fout 

            modelBuilder.Entity<Medewerker>().Property(x => x.Afdeling).IsRequired();

            modelBuilder.Entity<Medewerker>().HasOne(r => r.Afdeling)
                .WithMany(r => r.Medewerkers)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(r => r.AfdelingId)
                     .HasConstraintName("FK_Medewerker_Afdeling");


            // seed 


            modelBuilder.Entity<Afdeling>().HasData(

                new Afdeling
                {
                    AfdelingId = 1,
                    AfdelingCode = "VERK",
                    AfdelingNaam = "Verkoop",
                    AfdelingTekst = null
                },
                  new Afdeling
                  {
                      AfdelingId = 2,
                      AfdelingCode = "BOEK",
                      AfdelingNaam = "Boekhouding",
                      AfdelingTekst = null
                  },
                    new Afdeling
                    {
                        AfdelingId = 3,
                        AfdelingCode = "AANK",
                        AfdelingNaam = "Aankoop",
                        AfdelingTekst = null
                    }

               );



            modelBuilder.Entity<Medewerker>().HasData(
                  new Medewerker
                  {
                       AfdelingId=1
                  },
                  new Medewerker
                  {
                      AfdelingId = 2
                  }

                );


        }





    }
}
