using Elfo.Round.WriteCycle;

namespace Elfo.POC.PdfGallery.Api.SystemManagement.Commands
{
    public class EnableUserCommand : ICommand<string>
    {
        public int IdUser { get; set; }
    }
}
