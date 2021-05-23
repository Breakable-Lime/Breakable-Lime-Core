using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BreakableLime.Repository;
using BreakableLime.Repository.services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

namespace BreakableLime.Host
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private SecurityKey GetAuthenticationKey()
        {
            var authenticationSecret = Configuration["jwt:authenticationKey"];
            var bytes = Encoding.UTF8.GetBytes(authenticationSecret);
            var key = new SymmetricSecurityKey(bytes);

            return key;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            var key = GetAuthenticationKey();

            services.AddAuthentication("jwt").AddJwtBearer("jwt", c =>
            {
                c.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidAudience = Configuration["jwt:audience"],
                    ValidIssuer = Configuration["jwt:issuer"],
                    IssuerSigningKey = key
                };
            });


            services.AddControllers();

            //added spa
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "BreakableLime",
                    Version = "v1",
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("https://tldrlegal.com/license/mit-license")
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            //add db context to DI container
            services.AddDbContext<ApplicationDbContext>(c => { c.UseInMemoryDatabase("Testing-db"); });

            //add services
            services.AddTransient<IImageService, ImageService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BreakableLime.Host v1"));
            }

            app.UseSpaStaticFiles(new StaticFileOptions
            {
                ContentTypeProvider = new FileExtensionContentTypeProvider()
            });

            app.UseRouting();

            app.UseSerilogRequestLogging();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseSpa(spa =>
            {

                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }

            });
        }
    }
}
