using System;

namespace Elfo.POC.PdfGallery.Api.PdfGallery
{
	public class GetItemTimerQuery
	{
		public string? Name { get; set; }
		public string Time { get; set; }
		public DateTime? Begin { get; set; }
		public DateTime? End { get; set; }
		
	}
}
