using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace DREM_API.Model
{

    /// <summary>
    /// Database context
    /// </summary>
    public class ADBContext : DbContext
    {
        private readonly ILogger<ADBContext> logger;
        private readonly IConfiguration configuration;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        /// <param name="logger"></param>
        /// <param name="configuration"></param>
        public ADBContext(DbContextOptions<ADBContext> options, ILogger<ADBContext> logger, IConfiguration configuration)
            : base(options)
        {
            this.logger = logger;
            this.configuration = configuration;
        }



        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                if (optionsBuilder == null) throw new Exception("optionsBuilder is null");

                configuration.GetConnectionString("DremDB");

                switch (configuration.GetValue<string>("DBType"))
                {
                    case "RAM":
                        var rand = new Random();
                        var randnum = rand.Next(1000000000, 2000000000);
                        //Constants.DBType.RAM
                        optionsBuilder.UseInMemoryDatabase($"DremDB-{randnum}");
                        break;
                    default:
                        var cs = configuration.GetConnectionString("DremDB");

                        if (string.IsNullOrEmpty(cs))
                        {
                            optionsBuilder.UseInMemoryDatabase("DremDB");
                        }
                        else
                        {
                            optionsBuilder.UseSqlServer(cs, x => x.MigrationsHistoryTable("__EFMigrationsHistory", "drem"));
                        }
                        break;
                }
#if DEBUG
                optionsBuilder.EnableSensitiveDataLogging(true);
#endif
            }
            catch (Exception exc)
            {
                logger.LogError("Error while setting up database", exc);
                throw;
            }
        }
        /// <summary>
        /// DB Schema
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            if (modelBuilder is null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            modelBuilder.HasDefaultSchema("drem");

            modelBuilder.Entity<RECWithId>(
                e =>
                {
                    e.HasKey(c => new { c.Id });


                    e.ToTable("REC");
                });
            modelBuilder.Entity<ValueSet>(
                e =>
                {
                    e.HasKey(c => new { c.Id });
                    e.HasIndex(c => new { c.ItemCode });
                    e.HasIndex(c => new { c.Language });
                    e.HasIndex(c => new { c.ValueSetCode });
                    e.ToTable("ValueSet");
                });

            modelBuilder.Entity<Model.DB.Project>(
                e =>
                {
                    e.HasKey(c => new { c.Id });
                    e.HasIndex(c => new { c.ShowToPublic });
                    e.ToTable("Project");
                });


        }
        /// <summary>
        /// List of real estate companies
        /// </summary>
        public DbSet<RECWithId> RECs { get; set; }
        /// <summary>
        /// List of projects to shown on the main screen
        /// </summary>
        public DbSet<Model.DB.Project> Projects { get; set; }
        /// <summary>
        /// List of value sets
        /// </summary>
        public DbSet<ValueSet> ValueSets { get; set; }
        internal void EnsureCreated()
        {
            try
            {
                if (Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
                {
                    Database.Migrate();
                }
            }
            catch (Exception exc)
            {
                logger.LogError(exc.Message, exc);
                throw;
            }
        }
    }
}
