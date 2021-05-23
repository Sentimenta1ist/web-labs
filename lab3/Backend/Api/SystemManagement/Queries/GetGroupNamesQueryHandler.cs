using Elfo.Round.ReadCycle;
using Elfo.POC.PdfGallery.Api.Infrastructure;
using MediatR;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Elfo.POC.PdfGallery.Api.SystemManagement.Queries
{
    public class GetGroupNamesQueryHandler : IRequestHandler<GetGroupNames.Query, List<GetGroupNames.Result>>
    {
        readonly IDbConnection connection;
        public GetGroupNamesQueryHandler(IDbConnection connection) => this.connection = connection;

        public Task<List<GetGroupNames.Result>> Handle(GetGroupNames.Query query, CancellationToken cancellationToken) =>
            connection.ReadAsync(q => q
                .Where("isEnabled", RuleOperator.IsEqual, true)
                .ToMultiSelectionResultAsync<GetGroupNames.Result>(query, "idGroup", "name"));
    }
}
