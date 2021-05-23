using Elfo.Round.WriteCycle;
using Elfo.POC.PdfGallery.Domain.SystemManagement;
using Elfo.POC.PdfGallery.Domain.SystemManagement.ValueObjects;
using NHibernate;
using NHibernate.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elfo.POC.PdfGallery.Infrastructure.NHibernate.SystemManagement
{
	public class SystemUserPermissionAssociationRepository : BaseRepository<SystemUserPermissionAssociation, SystemUserPermissionAssociationID>, ISystemUserPermissionAssociationRepository
	{
		public SystemUserPermissionAssociationRepository(ISession session) : base(session) { }

		public Task<List<SystemUserPermissionAssociation>> GetByIdUser(SystemUserID idUser) =>
			session.Query<SystemUserPermissionAssociation>().Where(a => a.Id.IdUser == idUser).ToListAsync();
	}
}
