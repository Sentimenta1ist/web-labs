using Elfo.Round.Identity;
using Elfo.Round.Profiler;
using Elfo.Round.ReadCycle;
using Elfo.Round.WriteCycle;
using Elfo.Round.Mvc;
using Elfo.POC.PdfGallery.Api.Infrastructure;
using Elfo.POC.PdfGallery.Api.Round.Logger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Microsoft.Extensions.Hosting;
using MediatR;
using Autofac;

namespace Elfo.POC.PdfGallery.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();

            services.AddAuthentication(Configuration);

            services.AddSwagger(Configuration);

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins", builder =>
                {
                    builder
                    .WithOrigins(Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? new string[] { })
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .WithExposedHeaders(Configuration.GetSection("Cors:ExposedHeaders").Get<string[]>() ?? new string[] { });
                });
            });

            services.AddRoundSettings(Configuration);

            services.AddRoundReadCycle(Configuration);

            services.AddRoundWriteCycle(Configuration);

            services.AddRoundProfiler(Configuration);

            services.AddRoundMvc();

            services
                .AddSingleton(Configuration)
                .AddSingleton(Serilog.Log.Logger);

            services.AddControllers()
                .AddNewtonsoftJson(o => o.SerializerSettings.NullValueHandling = NullValueHandling.Ignore)
                .AddRoundExceptionHandling();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterGenericDecorator(typeof(DispatchDecorator<>), typeof(INotificationHandler<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            // Uncomment this part if your webapi runs under https
            //else
            //{
            //    app.UseHttpsRedirection();
            //    app.UseHsts();
            //}

            app.UseCors("AllowSpecificOrigins");

            app.UseAuthentication();

            app.UseRoundIdentity();

            app.UseLoggingScope();

            app.UseRoundProfiler();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger(Configuration);
        }
    }
}
