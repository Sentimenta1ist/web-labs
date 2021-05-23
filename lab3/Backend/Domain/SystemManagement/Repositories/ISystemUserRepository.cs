using Elfo.Round.WriteCycle;
using Elfo.POC.PdfGallery.Domain.SystemManagement.ValueObjects;
using System.Threading.Tasks;

namespace Elfo.POC.PdfGallery.Domain.SystemManagement
{
	public interface ISystemUserRepository : IRepository<SystemUser, SystemUserID>
	{
		Task<bool> ExistsWithUsername(string username);
	}
}
