using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Diagnostics;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using Microsoft.Framework.Logging.Console;
using Microsoft.Framework.Runtime;
using MVC6Experiment.Repository;
using Serilog;
using System.IO;
using AutoMapper;
using MVC6Experiment.Model;
using MVC6Experiment.ViewModel;

namespace MVC6Experiment
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            var builder = new ConfigurationBuilder(appEnv.ApplicationBasePath)
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            Environment = appEnv;
        }

        public IApplicationEnvironment Environment { get; set; }
        public Microsoft.Framework.Configuration.IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddLogging();

            services.AddSingleton(_ => Configuration);
            services.AddSingleton<IRepository>((IServiceProvider pr) =>
            {
                var tmplDirectory = Path.Combine(Environment.ApplicationBasePath, 
                    Configuration.Get("Data:TemplateDirectory"));

                var tmplName = Path.Combine(Environment.ApplicationBasePath,
                    Configuration.Get("Data:ClientFile"));

                return new SimpleRepository(tmplDirectory, tmplName);
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
#if DNXCORE50
                            .WriteTo.TextWriter(Console.Out)
#else
                            .WriteTo.Trace()
                .WriteTo.RollingFile("log-{Date}.txt")
#endif
            .CreateLogger();

            loggerFactory.MinimumLevel = LogLevel.Information;
            loggerFactory.AddConsole();
            loggerFactory.AddSerilog();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseErrorPage(ErrorPageOptions.ShowAll);
            }
            else
            {
                app.UseErrorHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
