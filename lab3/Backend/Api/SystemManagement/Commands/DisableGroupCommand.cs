using Elfo.Round.WriteCycle;

namespace Elfo.POC.PdfGallery.Api.SystemManagement.Commands
{
    public class DisableGroupCommand : ICommand
    {
        public short IdGroup { get; set; }
    }
}
