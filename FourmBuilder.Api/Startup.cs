using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FourmBuilder.Api.Core.Configuration;
using FourmBuilder.Api.Installer;
using FourmBuilder.Common.Helper.Environment;
using FourmBuilder.Common.Middleware;
using FourmBuilder.Common.Mongo;
using FourmBuilder.Common.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;

namespace FourmBuilder.Api
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
            services.InstallServicesAssembly(Configuration);
            services.AddSwaggerDocs(Configuration);

            services.AddMongo(Configuration);

            services.AddMongoCollection();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            IApplicationBootstrapper applicationBootstrapper)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FourmBuilder.Api v1"));

            applicationBootstrapper.Initial();

            #region Static files Setting

            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(env.ContentRootPath, ApplicationStaticPath.Documents)),
                RequestPath = ApplicationStaticPath.Clients.Document
            });

            #endregion Static files Setting

            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("MyPolicy");
            app.UseMiddleware<ApplicationMetaMiddleware>();
            app.UseMiddleware<MemberShipMiddleware>();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}