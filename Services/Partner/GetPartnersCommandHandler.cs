using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data;
using MediatR;
using Microsoft.SharePoint.Client;

namespace Services.Partner
{
    public class GetPartnersCommandHandler : IRequestHandler<GetPartnersCommand, IEnumerable<Models.Partner>>
    {
        private readonly AusOuvidosContext _db;
        private readonly ClientContext _sharePointContext;

        public GetPartnersCommandHandler(AusOuvidosContext db, ClientContext sharePointContext)
        {
            this._db = db;
            this._sharePointContext = sharePointContext;
        }

        public Task<IEnumerable<Models.Partner>> Handle(GetPartnersCommand request, CancellationToken cancellationToken)
        {
            var partners = _sharePointContext.Web.Lists.GetByTitle("Parceiros").GetItems(new CamlQuery() { });
            _sharePointContext.Load(partners);
            _sharePointContext.ExecuteQuery();

            var output = new List<Models.Partner>();
            foreach (var partner in partners)
            {
                var link = partner.FieldValues["Site"]?.ToString() ?? "";
                output.Add(new Models.Partner()
                {
                    ID = partner.Id,
                    Name = partner.FieldValues["Title"]?.ToString(),
                    Website = new UriBuilder(link).Uri?.ToString(),
                    Order = Convert.ToInt32(partner.FieldValues["Order"] ?? "0"),
                });
            }

            return Task.FromResult(output.OrderBy(a => $"{a.Order} {a.Name}").AsEnumerable());
        }
    }
}
