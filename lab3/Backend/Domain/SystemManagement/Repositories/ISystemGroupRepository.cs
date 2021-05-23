using Elfo.Round.WriteCycle;
using Elfo.POC.PdfGallery.Domain.SystemManagement.ValueObjects;
using System.Threading.Tasks;

namespace Elfo.POC.PdfGallery.Domain.SystemManagement
{
    public interface ISystemGroupRepository : IRepository<SystemGroup, SystemGroupID>
    {
        Task<bool> GroupsWithNameExists(string name);
    }
}
