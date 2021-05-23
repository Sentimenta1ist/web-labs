using Elfo.Round.WriteCycle;
using Elfo.POC.PdfGallery.Domain.SystemManagement.ValueObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elfo.POC.PdfGallery.Domain.SystemManagement
{
	public interface ISystemUserGroupAssociationRepository : IRepository<SystemUserGroupAssociation, SystemUserGroupAssociationID>
	{
		Task<List<SystemUserGroupAssociation>> GetByIdUser(SystemUserID idUser);
		Task<List<SystemUserGroupAssociation>> GetByIdGroup(SystemGroupID idGroup);
	}
}
