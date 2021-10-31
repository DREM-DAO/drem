using AlgorandAuthentication;
using AutoMapper;
using DREM_API.BusinessController;
using DREM_API.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DREM_API
{
    /// <summary>
    /// Startup class
    /// </summary>
    public class Startup : IDisposable
    {
        /// <summary>
        /// Identifies specific run of the application.
        /// </summary>
        public static readonly string InstanceId = Guid.NewGuid().ToString();
        /// <summary>
        /// Identifies specific run of the application
        /// </summary>
        public static readonly DateTimeOffset Started = DateTimeOffset.Now;
        /// <summary>
        /// Nlog logger
        /// </summary>
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        /// <summary>
        /// App exit catch event
        /// </summary>
        public static readonly CancellationTokenSource AppExitCancellationTokenSource = new CancellationTokenSource();
        /// <summary>
        /// Configuration
        /// </summary>
        public IConfiguration Configuration { get; }
        private bool disposedValue;

        /// <summary>
        /// Args for tasks processing
        /// </summary>
        public static string[] Args { get; internal set; }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(
            IServiceCollection services)
        {
            services
                .AddControllers()
                .AddNewtonsoftJson();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DREM_API", Version = "v1" });

                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); //This line

                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Algo signed auth tx",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.OperationFilter<Swashbuckle.AspNetCore.Filters.SecurityRequirementsOperationFilter>();

                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"doc/documentation.xml"));
            });
            services.AddTransient<BufferTransferBusinessController>();
            services.AddTransient<DailyPayoutBusinessController>();
            services.AddTransient<ImageMetaBusinessController>();
            services.AddTransient<OrderBusinessController>();
            services.AddTransient<ProjectBusinessController>();
            services.AddTransient<RECBusinessController>();
            services.AddTransient<ShareholderBusinessController>();
            services.AddTransient<TransferBusinessController>();
            services.AddTransient<ValueSetBusinessController>();
            services.AddTransient<VotingBusinessController>();

            services.AddScoped<BufferTransferRepository>();
            services.AddScoped<DailyPayoutRepository>();
            services.AddScoped<ImageMetaRepository>();
            services.AddScoped<OpportunityRepository>();
            services.AddScoped<OrderRepository>();
            services.AddScoped<ProjectRepository>();
            services.AddScoped<RECRepository>();
            services.AddScoped<ShareholderRepository>();
            services.AddScoped<TransferRepository>();
            services.AddScoped<ValueSetRepository>();
            services.AddScoped<VotingQuestionRepository>();
            services.AddScoped<VotingResultRepository>();

            services.AddDbContext<Model.ADBContext>(options =>
            {
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services
                .AddAuthentication(AlgorandAuthenticationHandler.ID)
                .AddAlgorand(o =>
                {
                    o.CheckExpiration = false;
                    o.AlgodServer = Configuration["algod:server"];
                    o.AlgodServerToken = Configuration["algod:token"];
                    o.Realm = Configuration["algod:realm"]; //"DREM-Authenticate";
                    o.NetworkGenesisHash = Configuration["algod:networkGenesisHash"];
                });


            var corsConfig = Configuration.GetSection("Cors").AsEnumerable().Select(k => k.Value).Where(k => !string.IsNullOrEmpty(k)).ToArray();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                builder =>
                {
                    builder.WithOrigins(corsConfig)
                                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                                        .AllowAnyMethod()
                                        .AllowAnyHeader()
                                        .AllowCredentials();
                });
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="context"></param>
        /// <param name="projectRepository"></param>
        /// <param name="recRepository"></param>
        /// <param name="valueSetRepository"></param>
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            Model.ADBContext context,
            Repository.ProjectRepository projectRepository,
            Repository.RECRepository recRepository,
            Repository.ValueSetRepository valueSetRepository
            )
        {
            logger.Info($"App start {InstanceId}");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            context.EnsureCreated();

            app.UseCors();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DREM_API v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            var mockFolder = Configuration["MockDataFolder"];
            if (!string.IsNullOrEmpty(mockFolder))
            {
                var file = $"{mockFolder}/valueset.json";
                if (File.Exists(file))
                {
                    var data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Model.ValueSet>>(File.ReadAllText(file));
                    valueSetRepository.AddRange(data);
                }
                file = $"{mockFolder}/projects.json";
                if (File.Exists(file))
                {
                    var data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Model.DB.Project>>(File.ReadAllText(file));
                    projectRepository.AddRange(data);
                }
                file = $"{mockFolder}/rec.json";
                if (File.Exists(file))
                {
                    var data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Model.DB.REC>>(File.ReadAllText(file));
                    recRepository.AddRange(data);
                }
            }

            GC.Collect();
            try
            {
                if (Directory.Exists("/app"))
                {
                    System.IO.File.WriteAllText("/app/ready.txt", DateTimeOffset.Now.ToString("o"));
                }
                else
                {
                    logger.Info("/app does not exists");
                }
            }
            catch (Exception exc)
            {
                logger.Error(exc.Message);
            }
            logger.Info($"Application has been started {InstanceId}");
        }
        /// <summary>
        /// protected dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    AppExitCancellationTokenSource?.Cancel();
                    AppExitCancellationTokenSource?.Dispose();
                }

                disposedValue = true;
            }
        }
        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
