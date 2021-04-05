using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BreakableLime", Version = "v1" });
            });
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

            app.UseRouting();

            app.UseSerilogRequestLogging();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
