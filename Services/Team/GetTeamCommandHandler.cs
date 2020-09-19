using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data;
using MediatR;
using Microsoft.SharePoint.Client;
using Models;

namespace Services.Team
{
    public class GetTeamCommandHandler : IRequestHandler<GetTeamCommand, IEnumerable<Professional>>
    {
        private readonly AusOuvidosContext _db;
        private readonly ClientContext _sharePointContext;

        public GetTeamCommandHandler(AusOuvidosContext db, ClientContext sharePointContext)
        {
            this._db = db;
            this._sharePointContext = sharePointContext;
        }

        public Task<IEnumerable<Professional>> Handle(GetTeamCommand request, CancellationToken token)
        {
            var items = _sharePointContext.Web.Lists.GetByTitle("Profissionais").GetItems(new CamlQuery()
            {
                ViewXml = "<View><Query><Where><Eq><FieldRef Name='Situacao'/><Value Type='Text'>Ativo</Value></Eq></Where></Query><</View>"
            });

            _sharePointContext.Load(items);
            _sharePointContext.ExecuteQuery();

            var output = new List<Professional>();

            foreach (var item in items)
            {
                var profissional = new Professional();

                profissional.Nome = item.FieldValues["Title"]?.ToString();
                profissional.UltimoNome = item.FieldValues["UltimoNome"]?.ToString();
                profissional.Funcao = item.FieldValues["Funcao"]?.ToString();
                profissional.Grupo = item.FieldValues["Grupo"]?.ToString();
                profissional.ID = item.Id;
                profissional.Idealizador = Convert.ToBoolean(item.FieldValues["Idealizador"] ?? false);
                profissional.LinkedIn = item.FieldValues["LinkedIn"]?.ToString();
                profissional.Registro = item.FieldValues["Registro"]?.ToString();
                profissional.Email = item.FieldValues["Email"]?.ToString(); ;

                output.Add(profissional);
            }

            return Task.FromResult(output.OrderBy(a => $"{a.Nome} {a.UltimoNome}"));
        }
    }
}
