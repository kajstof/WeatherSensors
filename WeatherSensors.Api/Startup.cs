using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WeatherSensors.Api.Services;
using WeatherSensors.Api.Services.Interfaces;

namespace WeatherSensors.Api
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
            var blobStorageConnectionString =
                Configuration.GetConnectionString("SigmaIoTAzureBlobStorageConnectionString");
            var blobContainerName = Configuration.GetValue<string>("AzureContainerName");

            services.AddControllers();
            services.AddMediatR(typeof(Startup));
            services.AddSingleton(x => new BlobContainerClient(blobStorageConnectionString, blobContainerName));
            services.AddSingleton<IBlobService, BlobService>();
            services.AddScoped<IMeasurementManagerService, MeasurementManagerService>();
            services.AddScoped<IMeasurementFileNameGenerator, MeasurementFileNameGenerator>();
            services.AddScoped<IFileCacheService, FileCacheService>();
            services.AddScoped<IBlobDownloaderService, BlobDownloaderService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
