using Elfo.POC.PdfGallery.Domain;
using Elfo.POC.PdfGallery.Domain.SystemManagement;
using Elfo.POC.PdfGallery.Domain.SystemManagement.ValueObjects;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Elfo.POC.PdfGallery.Api.SystemManagement.Commands
{
    public class EnableGroupCommandHandler : IRequestHandler<EnableGroupCommand, Unit>
    {
        readonly ISystemGroupRepository groups;
        public EnableGroupCommandHandler(ISystemGroupRepository groups) => this.groups = groups;

        public async Task<Unit> Handle(EnableGroupCommand command, CancellationToken cancellationToken)
        {
            var group = await groups.GetAsync(new SystemGroupID(command.IdGroup))
                ?? throw new ValidationException(nameof(SystemGroup), ValidationErrorCode.GroupDoesNotExist);

            group.Enable();

            return Unit.Value;
        }
    }
}
