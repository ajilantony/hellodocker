using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json.Serialization;
using Swashbuckle.SwaggerGen;
using Swashbuckle.SwaggerGen.XmlComments;
using System.IO;

namespace HelloDocker
{
    public class Startup
    {
        private string _assemblyPath;
        private string _appBasePath;

        public Startup(IApplicationEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ApplicationBasePath)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            _assemblyPath = GetBinPath(env);
            _appBasePath = env.ApplicationBasePath;
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var pathToDoc = _assemblyPath;
            // Add framework services.
            services.AddMvc()
              .AddJsonOptions(opts =>
              {
                  opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
              });

            services.AddSwaggerGen();
            services.ConfigureSwaggerDocument(options =>
            {
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "UserProfile API",
                    Description = "UserProfile API hosted in Docker Container",
                    TermsOfService = "None"
                });
                //options.OperationFilter(new Swashbuckle.SwaggerGen.XmlComments.ApplyXmlActionComments(pathToDoc));
            });

            //services.ConfigureSwaggerSchema(options =>
            //{
            //    options.DescribeAllEnumsAsStrings = true;
            //    options.ModelFilter(new Swashbuckle.SwaggerGen.XmlComments.ApplyXmlTypeComments(pathToDoc));
            //});

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseStaticFiles();
            app.UseMvc();
            app.UseSwaggerGen();
            app.UseSwaggerUi();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);

        private string GetBinPath(IApplicationEnvironment env)
        {
            string rootPath = Path.GetFullPath(Path.Combine(env.ApplicationBasePath, @"..\..\"));
            string binFolder = @"artifacts\bin\" + env.ApplicationName;
            string binPath = Path.Combine(rootPath, binFolder);
            return binPath;
        }
    }
}
