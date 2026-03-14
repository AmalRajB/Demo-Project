using demoWebAPI.API.model.domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace demoWebAPI.API.data
{
    /*!
     * \class ApplicationDbContext
     * \brief Represents the database context for the application.
     *
     * This class manages entity objects during runtime, includes DbSet
     * properties for each domain model, and handles database connections
     * using Entity Framework Core.
     */
    public class ApplicationDbContext : DbContext
    {
        /*!
         * \brief Constructor for ApplicationDbContext.
         * \param options The options to configure the DbContext.
         *
         * This constructor passes configuration settings
         * to the base DbContext class.
         */
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        /*!
         * \brief Represents the Countries table in the database.
         *
         * This DbSet allows querying and saving instances of the Country entity.
         */
        public DbSet<Country> Countries { get; set; }

        /*!
         * \brief Represents the States table in the database.
         *
         * This DbSet allows querying and saving instances of the State entity.
         */
        public DbSet<State> States { get; set; }


        public DbSet<FileModel> fileModels { get; set; }

        public DbSet<productCategory> productCategories { get; set; }

        public DbSet<Products> products { get; set; }

        public DbSet<ProductFile> productFiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductFile>()
            .HasOne(pf => pf.Product)
            .WithMany(p => p.productFiles)
            .HasForeignKey(pf => pf.ProductId)
            .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
