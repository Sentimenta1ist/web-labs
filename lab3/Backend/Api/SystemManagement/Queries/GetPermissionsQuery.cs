using MediatR;
using System.Collections.Generic;

namespace Elfo.POC.PdfGallery.Api.SystemManagement.Queries
{
    public class GetPermissionsQuery : IRequest<List<string>> { }
}
