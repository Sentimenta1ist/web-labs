using Elfo.Round.WriteCycle;
using Elfo.POC.PdfGallery.Domain.SystemManagement;
using Elfo.POC.PdfGallery.Domain.SystemManagement.ValueObjects;
using NHibernate;
using NHibernate.Linq;
using System.Threading.Tasks;

namespace Elfo.POC.PdfGallery.Infrastructure.NHibernate.SystemManagement
{
	public class SystemUserRepository : BaseRepository<SystemUser, SystemUserID>, ISystemUserRepository
	{
		public SystemUserRepository(ISession session) : base(session) { }

		public async Task<bool> ExistsWithUsername(string username) =>
			await session.Query<SystemUser>()
				.CountAsync(u => u.Username == username && u.IsEnabled) > 0;

		public override async Task<bool> ExistsAsync(SystemUserID id) =>
			await session.Query<SystemUser>().CountAsync(u => u.Id == id && u.IsEnabled) > 0;
	}
}
