using Elfo.Round.WriteCycle;
using Elfo.POC.PdfGallery.Domain.SystemManagement.ValueObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elfo.POC.PdfGallery.Domain.SystemManagement
{
	public interface ISystemUserPermissionAssociationRepository : IRepository<SystemUserPermissionAssociation, SystemUserPermissionAssociationID>
	{
		Task<List<SystemUserPermissionAssociation>> GetByIdUser(SystemUserID idUser);
	}
}
