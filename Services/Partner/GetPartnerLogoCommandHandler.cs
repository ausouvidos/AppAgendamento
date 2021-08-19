using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Data;
using MediatR;
using Microsoft.SharePoint.Client;
using Models;

namespace Services.Partner
{
    public class GetPartnerLogoCommandHandler : IRequestHandler<GetPartnerLogoCommand, Image>
    {
        private readonly AusOuvidosContext _db;
        private readonly ClientContext _sharePointContext;

        public GetPartnerLogoCommandHandler(AusOuvidosContext db, ClientContext sharePointContext)
        {
            this._db = db;
            this._sharePointContext = sharePointContext;
        }

        public Task<Image> Handle(GetPartnerLogoCommand request, CancellationToken cancellationToken)
        {
            var items = _sharePointContext.Web.Lists.GetByTitle("FotosParceiros").GetItems(new CamlQuery()
            {
                ViewXml = $"<View><Query><Where><Eq><FieldRef Name='ID' LookupId='TRUE'/><Value Type='Lookup'>{request.Id}</Value></Eq></Where></Query><</View>",
            });

            _sharePointContext.Load(items, a => a.Include(x => x.File));
            _sharePointContext.ExecuteQuery();

            var output = new Image();

            if (items.Count == 0)
            {
                byte[] imageBytes = Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP89x8AAwEB/9nmgRwAAAAASUVORK5CYII=");
                using var fileStream = new MemoryStream(imageBytes, 0, imageBytes.Length);
                output.Contents = fileStream.ToArray();
                output.Name = "empty.png";

            }
            else
            {
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
