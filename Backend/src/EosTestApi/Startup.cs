using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Eos.Data;
using Eos.Data.EF;
using EosTestApi.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace EosTestApi
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            string connection = _configuration.GetConnectionString("common");   
            services.AddDbContext<EosContext>(options =>
                {
                    // options.UseSqlServer(connection);
                    options.UseNpgsql(connection, builder =>
                    {
                        Console.WriteLine("Use Postgres Database Provider");
                        builder.MigrationsAssembly("Eos.Data.EF.Postgres");
                    });
                });

            services.AddRepositories();
            services.AddBlServices();

            services.AddControllers()
                .AddNewtonsoftJson(options => 
                {
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                });;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // migrate
            try
            {
                var builder = new DbContextOptionsBuilder<EosContext>();
                var connectionString = _configuration.GetConnectionString("common");
                // builder.UseSqlServer(connectionString);
                builder.UseNpgsql(connectionString, builder =>
                {
                    Console.WriteLine("Use Postgres Database Provider");
                    builder.MigrationsAssembly("Eos.Data.EF.Postgres");
                });
                using var context = new EosContext(builder.Options);
                var migrations = context.Database.GetPendingMigrations().ToList();
                if (migrations.Any())
                    context.Database.Migrate();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            app.UseRouting();
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
