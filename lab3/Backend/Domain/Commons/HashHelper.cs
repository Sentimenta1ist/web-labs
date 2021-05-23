namespace Elfo.POC.PdfGallery.Domain
{
	public static class HashHelper
	{
		public static string Hash<T>(T value) =>
			value?.GetHashCode().ToString();
	}
}
