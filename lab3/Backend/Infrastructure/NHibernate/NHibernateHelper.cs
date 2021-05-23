using Microsoft.Extensions.Configuration;
using NHibernate;
using NHibernate.Cfg;
using System.Collections.Generic;

namespace Elfo.POC.PdfGallery.Infrastructure.NHibernate
{
	public static class NHibernateHelper
	{
		public static ISessionFactory BuildSessionFactory(IConfiguration configuration)
		{
			var configurationSettings = new Dictionary<string, string>
			{
				{ "connection.provider", "NHibernate.Connection.DriverConnectionProvider" },
				{ "connection.driver_class", "NHibernate.Driver.SqlClientDriver" },
				{ "dialect", "NHibernate.Dialect.MsSql2008Dialect" },
				{ "connection.connection_string", configuration.GetConnectionString("Db") },
				{ "connection.release_mode", "auto" },
				{ "default_schema", "dbo" },
				{ "cache.use_query_cache", "false" },
				{ "flushmode", "auto" },
				{ "show_sql", "false" },
				{ "generate_statistics", "true" },
				{ "cache.provider_class", "NHibernate.Cache.HashtableCacheProvider" },
				{ "cache.use_second_level_cache", "false" },
				{ "adonet.batch_size", "10" },
				{ "prepare_sql", "true" }
			};

			var cfg = new Configuration();
			cfg.SetProperties(configurationSettings);
			cfg.AddAssembly("Elfo.POC.PdfGallery.Infrastructure");

			return cfg.BuildSessionFactory();
		}
	}
}
