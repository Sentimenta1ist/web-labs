using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elfo.Round.WriteCycle;
using Elfo.POC.PdfGallery.Domain.SystemManagement;
using Elfo.POC.PdfGallery.Domain.SystemManagement.ValueObjects;
using NHibernate;
using NHibernate.Linq;

namespace Elfo.POC.PdfGallery.Infrastructure.NHibernate.SystemManagement
{
	public class SystemUserGroupAssociationRepository : BaseRepository<SystemUserGroupAssociation, SystemUserGroupAssociationID>, ISystemUserGroupAssociationRepository
	{
		public SystemUserGroupAssociationRepository(ISession session) : base(session) { }

		public Task<List<SystemUserGroupAssociation>> GetByIdUser(SystemUserID idUser) =>
			session.Query<SystemUserGroupAssociation>().Where(a => a.Id.IdUser == idUser).ToListAsync();
		
		public Task<List<SystemUserGroupAssociation>> GetByIdGroup(SystemGroupID idGroup) =>
			session.Query<SystemUserGroupAssociation>().Where(a => a.Id.IdGroup == idGroup).ToListAsync();
	}
}
