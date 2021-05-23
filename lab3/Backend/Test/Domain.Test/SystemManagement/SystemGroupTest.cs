using Elfo.POC.PdfGallery.Domain.SystemManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Elfo.POC.PdfGallery.Domain.Test
{
	[TestClass]
	public class SystemGroupTest
	{
		[TestMethod]
		[DataRow("Admin")]
		public void Create_Default_Ok(string name)
		{
			var group = SystemGroup.Create(name);

			Assert.AreEqual(name, group.Name);
		}

		[TestMethod]
		[DataRow("")]
		[DataRow("  ")]
		[DataRow(null)]
		[ExpectedValidationExceptionAttribute(ValidationErrorCode.EmptySystemGroupName)]
		public void Create_NameIsEmpty_Error(string name)
		{
			SystemGroup.Create(name);
		}

		[TestMethod]
		[DataRow("Admin")]
		public void Update_Default_Ok(string name)
		{
			var group = SystemGroup.Create("Name");

			Assert.IsNotNull(group);

			group.Update(name);

			Assert.AreEqual(name, group.Name);
		}

		[TestMethod]
		[DataRow("")]
		[DataRow("  ")]
		[DataRow(null)]
		[ExpectedValidationExceptionAttribute(ValidationErrorCode.EmptySystemGroupName)]
		public void Update_NameIsEmpty_Error(string name)
		{
			var group = SystemGroup.Create("Name");

			Assert.IsNotNull(group);

			group.Update(name);
		}
	}
}
