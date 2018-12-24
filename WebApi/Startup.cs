using DAL;
using DAL.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Model.Data;
using Model.Services;
using Model.Specifications.Factories;
using Swashbuckle.AspNetCore.Swagger;
using WebApi.Helpers;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(config => {
                config.Filters.Add(typeof(CustomExceptionFilter));
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            if (!string.IsNullOrEmpty(Configuration.GetConnectionString("CinemaDatabase")))
            {
                var connection = Configuration.GetConnectionString("CinemaDatabase");
                services.AddDbContext<CinemaContext>(options => options.UseSqlServer(connection));
            }
            else
            {
                var connection = Configuration.GetConnectionString("CinemaDatabaseSqLite");
                services.AddDbContext<CinemaContext>(options => {
                    options.UseSqlite(connection);
                    options.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.QueryClientEvaluationWarning));
                });
            }

            services.AddTransient<IMovieService, MovieService>();
            services.AddTransient<IUserService, UserService>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IMovieRepository, MovieRepository>();

            services.AddTransient<MovieSpecificationFactory, MovieSpecificationFactory>();
            services.AddTransient<RoundRate, RoundRate>();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Cinema API", Version = "v1" });
                c.DescribeAllEnumsAsStrings();
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<CinemaContext>().Database.Migrate();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cinema API V1");
            });

            app.UseMvc();
        }
    }
}
