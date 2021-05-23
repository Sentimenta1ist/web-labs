using Elfo.Round.WriteCycle;

namespace Elfo.POC.PdfGallery.Api.SystemManagement.Commands
{
    public class EnableGroupCommand : ICommand
    {
        public short IdGroup { get; set; }
    }
}
