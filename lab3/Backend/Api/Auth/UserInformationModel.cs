using Elfo.Round.Identity;
using Elfo.Round.ReadCycle;

namespace Elfo.POC.PdfGallery.Api.Auth
{
    public class UserInformationModel : RoundUserModel
    {
        [Column()]
        public string TelephoneNumber { get; set; }
    }
}
