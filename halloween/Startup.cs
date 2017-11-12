using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using halloween.Models;
using Microsoft.EntityFrameworkCore;

namespace halloween
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            //Add new DbContext as a service
            services.AddDbContext<Database>(options =>
                       options.UseSqlite(Configuration["DB"]));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            //HEY! make sure the db is created! use IServiceScopeFactory 
            using (var serviceScope = app
                .ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                serviceScope
                    .ServiceProvider
                    .GetService<Database>()
                    .Database
                    .EnsureCreated();
            }

        }
    }
}
