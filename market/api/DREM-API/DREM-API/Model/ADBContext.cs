using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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

            modelBuilder.Entity<Model.DB.REC>(
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
                    e.HasIndex(c => new { c.UrlId }).IsUnique(true);
                    e.ToTable("Project");
                });

            modelBuilder.Entity<Model.DB.Opportunity>(
                e =>
                {
                    e.HasKey(c => new { c.Id });
                    e.ToTable("Opportunity");
                });
            modelBuilder.Entity<Model.DB.DailyPayout>(
                e =>
                {
                    e.HasKey(c => new { c.Id });
                    e.HasIndex(c => new { c.ProjectId });
                    e.HasIndex(c => new { c.Created });

                    e.Property(p => p.TxId)
                        .HasConversion(
                            v => JsonConvert.SerializeObject(v),
                            v => JsonConvert.DeserializeObject<List<string>>(v));

                    e.ToTable("DailyPayout");
                });

            modelBuilder.Entity<Model.DB.ImageMeta>(
                e =>
                {
                    e.HasKey(c => new { c.Id });
                    e.HasIndex(c => new { c.ProjectId });
                    e.HasIndex(c => new { c.Created });
                    e.ToTable("ImageMeta");
                });

            modelBuilder.Entity<Model.DB.Order>(
                e =>
                {
                    e.HasKey(c => new { c.Id });
                    e.HasIndex(c => new { c.AssetToReceiveId });
                    e.HasIndex(c => new { c.AssetToPayId });
                    e.HasIndex(c => new { c.Created });
                    e.ToTable("Order");
                });

            modelBuilder.Entity<Model.DB.Transfer>(
                e =>
                {
                    e.HasKey(c => new { c.Id });
                    e.HasIndex(c => new { c.ProjectAsset });
                    e.HasIndex(c => new { c.Created });
                    e.ToTable("Transfer");
                });

            modelBuilder.Entity<Model.DB.BufferTransfer>(
                e =>
                {
                    e.HasKey(c => new { c.Id });
                    e.HasIndex(c => new { c.ProjectId });
                    e.HasIndex(c => new { c.Created });
                    e.ToTable("BufferTransfer");
                });


            modelBuilder.Entity<Model.DB.Shareholder>(
                e =>
                {
                    e.HasKey(c => new { c.Id });
                    e.HasIndex(c => new { c.ProjectAsset });
                    e.HasIndex(c => new { c.Share });
                    e.ToTable("Shareholder");
                });

            modelBuilder.Entity<Model.DB.VotingResult>(
                e =>
                {
                    e.HasKey(c => new { c.Id });
                    e.HasIndex(c => new { c.QuestionTxId });

                    e.Property(p => p.Options)
                        .HasConversion(
                            v => JsonConvert.SerializeObject(v),
                            v => JsonConvert.DeserializeObject<Dictionary<string, decimal>>(v));

                    e.ToTable("VotingResult");
                });


            modelBuilder.Entity<Model.DB.VotingQuestion>(
                e =>
                {
                    e.HasKey(c => new { c.Id });
                    e.HasIndex(c => new { c.QuestionerAccount });
                    e.HasIndex(c => new { c.Created });

                    e.Property(p => p.Options)
                        .HasConversion(
                            v => JsonConvert.SerializeObject(v),
                            v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v));

                    e.ToTable("VotingQuestion");
                });


        }
        /// <summary>
        /// List of real estate companies
        /// </summary>
        public DbSet<Model.DB.REC> RECs { get; set; }
        /// <summary>
        /// List of projects to shown on the main screen
        /// </summary>
        public DbSet<Model.DB.Project> Projects { get; set; }
        /// <summary>
        /// List of value sets
        /// </summary>
        public DbSet<ValueSet> ValueSets { get; set; }
        /// <summary>
        /// List of value sets
        /// </summary>
        public DbSet<DB.Opportunity> Opportunities { get; set; }
        /// <summary>
        /// List of daily project payouts
        /// </summary>
        public DbSet<DB.DailyPayout> DailyPayouts { get; set; }
        /// <summary>
        /// List of images for project
        /// </summary>
        public DbSet<DB.ImageMeta> ImageMetas { get; set; }
        /// <summary>
        /// List of all transactions on the blockchain with the asset
        /// </summary>
        public DbSet<DB.Transfer> Transfers { get; set; }
        /// <summary>
        /// List of known orders for each nft
        /// </summary>
        public DbSet<DB.Order> Orders { get; set; }
        /// <summary>
        /// List of known orders for each nft
        /// </summary>
        public DbSet<DB.BufferTransfer> BufferTransfers { get; set; }
        /// <summary>
        /// List of all accounts who own specific asa
        /// </summary>
        public DbSet<DB.Shareholder> Shareholders { get; set; }
        /// <summary>
        /// List of all votings for specific asa
        /// </summary>
        public DbSet<DB.VotingQuestion> VotingQuestions { get; set; }
        /// <summary>
        /// List of all voting results for asked questions
        /// </summary>
        public DbSet<DB.VotingResult> VotingResults { get; set; }
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
