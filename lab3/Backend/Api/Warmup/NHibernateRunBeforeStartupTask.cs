using Elfo.Round.Mvc;
using Microsoft.Extensions.Configuration;
using NHibernate;
using Serilog;
using System.Threading;
using System.Threading.Tasks;

namespace Elfo.POC.PdfGallery.Api.Warmup
{
    public class NHibernateRunBeforeStartupTask : IRunBeforeStartup
    {
        private readonly ISessionFactory sessionFactory;
        private readonly ILogger logger;

        public NHibernateRunBeforeStartupTask(ISessionFactory sessionFactory, ILogger logger)
        {
            this.sessionFactory = sessionFactory;
            this.logger = logger;
        }

        public Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            logger.Information($"Starting {nameof(NHibernateRunBeforeStartupTask)}");
            WarmupNHibernate();
            logger.Information($"Endend {nameof(NHibernateRunBeforeStartupTask)}");

            return Task.CompletedTask;
        }

        private void WarmupNHibernate()
        {
            using (var session = sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                transaction.Commit();
            }
        }
    }
}
