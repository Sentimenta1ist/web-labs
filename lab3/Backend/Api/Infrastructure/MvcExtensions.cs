using Elfo.Round;
using Elfo.Round.Mvc;
using Elfo.POC.PdfGallery.Api.Warmup;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Elfo.POC.PdfGallery.Api.Infrastructure
{
    public static class MvcExtensions
    {
        public static void AddRoundMvc(this IServiceCollection services)
        {
            services.AddScoped<IOperationContext, OperationContext>();
            services.AddTaskToRunBeforeStartup<NHibernateRunBeforeStartupTask>();
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }

        public static void AddSwaggerOperationContext(this SwaggerGenOptions swaggerGenOptions, IConfiguration configuration)
        {
            swaggerGenOptions.OperationFilter<OperationIdFilter>();
        }
    }

    /// <summary>
    /// Operation filter to add the requirement of the custom header
    /// </summary>
    public class OperationIdFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = OperationIdHeaderKey.Value,
                In = ParameterLocation.Header,
                Description = $"the {OperationIdHeaderKey.Value} associated to this request",
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = "string",
                    Default = new OpenApiString(""),
                }
            });
        }
    }
}
