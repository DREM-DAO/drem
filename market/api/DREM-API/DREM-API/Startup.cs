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
    public class Startup
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
        /// App exit catch event
        /// </summary>
        public static readonly CancellationTokenSource AppExitCancellationTokenSource = new CancellationTokenSource();
        /// <summary>
        /// Args for tasks processing
        /// </summary>
        public static string[] Args { get; internal set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
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
            services.AddTransient<RECBusinessController, RECBusinessController>();
            services.AddScoped<RECMsSQLRepository, RECMsSQLRepository>();

            services.AddDbContext<Model.ADBContext>(options =>
            {
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services
                .AddAuthentication(AlgorandAuthenticationHandler.ID)
                .AddAlgorand(o =>
                {
                    o.CheckExpiration = false;
                    o.AlgodServer = "";
                    o.AlgodServerToken = "";
                    o.Realm = "DREM-Authenticate";
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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            Model.ADBContext context
            )
        {
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
        }
    }
}
