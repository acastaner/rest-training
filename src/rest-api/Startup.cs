using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.SwaggerGen;
using Folke.Elm.Sqlite;
using Folke.Elm;
using PriceList.Services.Interfaces;
using PriceList.Services.Services;
using PriceList.Lib;

namespace rest_api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            builder.AddCommandLine(new string[] { });

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddSwaggerGen();

            // Elm service
            services.AddElm<SqliteDriver>(options => options.ConnectionString = Configuration["Data:IdentityConnection:ConnectionString"]);

            // Price list service
            services.AddScoped<IProductService, ProductService>();

            // Swagger service configuration
            services.ConfigureSwaggerDocument(options =>
            {
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "PriceList Web API",
                    Description = "A simple REST API to search the Spirent pricelist",
                    TermsOfService = "None"
                });
            });


            services.ConfigureSwaggerSchema(options =>
            {
                options.DescribeAllEnumsAsStrings = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IProductService productService, IFolkeConnection session)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseIISPlatformHandler();
            app.UseStaticFiles();
            app.UseMvc();

            session.UpdateSchema(typeof(Product).Assembly);

            app.UseSwaggerGen();
            app.UseSwaggerUi();

            //productService.PopulateData().Wait();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
