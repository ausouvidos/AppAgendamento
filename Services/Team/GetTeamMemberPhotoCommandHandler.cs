using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Data;
using MediatR;
using Microsoft.SharePoint.Client;
using Models;

namespace Services.Team
{
    public class GetTeamMemberPhotoCommandHandler : IRequestHandler<GetTeamMemberPhotoCommand, Image>
    {
        private readonly AusOuvidosContext _db;
        private readonly ClientContext _sharePointContext;

        public GetTeamMemberPhotoCommandHandler(AusOuvidosContext db, ClientContext sharePointContext)
        {
            this._db = db;
            this._sharePointContext = sharePointContext;
        }

        public Task<Image> Handle(GetTeamMemberPhotoCommand request, CancellationToken token)
        {
            var items = _sharePointContext.Web.Lists.GetByTitle("Fotos").GetItems(new CamlQuery()
            {
                ViewXml = $"<View><Query><Where><Eq><FieldRef Name='Profissional' LookupId='TRUE'/><Value Type='Lookup'>{request.MemberId}</Value></Eq></Where></Query></View>"
            });

            _sharePointContext.Load(items, a => a.Include(x => x.File));
            _sharePointContext.ExecuteQuery();

            var output = new Image();

            if (items.Count == 0)
            {
                items = _sharePointContext.Web.Lists.GetByTitle("Fotos").GetItems(new CamlQuery()
                {
                    ViewXml = "<View><Query><Where><IsNull><FieldRef Name='Profissional' /></IsNull></Where></Query></View>",
                });
                _sharePointContext.Load(items, a => a.Include(x => x.File));
                _sharePointContext.ExecuteQuery();

                foreach (var item in items)
                {
                    if (item.File.Name == "Person.png")
                    {
                        var file = _sharePointContext.Web.GetFileByServerRelativeUrl(item.File.ServerRelativeUrl);
                        _sharePointContext.Load(file);
                        var streamResult = file.OpenBinaryStream();
                        _sharePointContext.ExecuteQuery();

                        using var fileStream = new MemoryStream();
                        streamResult.Value.CopyTo(fileStream);

                        output.Contents = fileStream.ToArray();
                        output.Name = file.Name;
                        break;
                    }
                }

            } else {
                foreach (var item in items)
                {
                    var file = _sharePointContext.Web.GetFileByServerRelativeUrl(item.File.ServerRelativeUrl);
                    _sharePointContext.Load(file);
                    var streamResult = file.OpenBinaryStream();
                    _sharePointContext.ExecuteQuery();

                    using var fileStream = new MemoryStream();
                    streamResult.Value.CopyTo(fileStream);

                    output.Contents = fileStream.ToArray();
                    output.Name = file.Name;
                }
            }


            return Task.FromResult(output);
        }
    }
}
