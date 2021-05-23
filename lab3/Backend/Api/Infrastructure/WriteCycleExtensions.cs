using Elfo.Round.WriteCycle;
using Elfo.POC.PdfGallery.Infrastructure.NHibernate;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace Elfo.POC.PdfGallery.Api.Infrastructure
{
    public static class WriteCycleExtensions
    {
        public static void AddRoundWriteCycle(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddRoundWriteCycle()
                    .WithNHibernate(o =>
                    {
                        o.BuildSessionFactory = () => NHibernateHelper.BuildSessionFactory(configuration);
                        o.ProfileConnection = true;
                    })
                    .WithMessages();

            services.AddRepositories();
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            var repositoryInterfaces = Assembly.Load("Elfo.POC.PdfGallery.Domain").DefinedTypes.Where(x => x.IsInterface && x.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRepository<,>))).ToList();
            var repositoryImplementations = Assembly.Load("Elfo.POC.PdfGallery.Infrastructure").DefinedTypes.Where(x => x.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRepository<,>))).ToList();

            foreach (var intefaceType in repositoryInterfaces)
                services.Add(new ServiceDescriptor(intefaceType, repositoryImplementations.FirstOrDefault(x => x.GetInterfaces().Contains(intefaceType)), ServiceLifetime.Scoped));
        }
    }
}
