using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Elfo.POC.PdfGallery.Domain.Test
{
    public class ExpectedDomainExceptionAttribute : ExpectedExceptionBaseAttribute
    {
        public ExpectedDomainExceptionAttribute(DomainExceptionCode domainExceptionCode) =>
            this.DomainExceptionCode = domainExceptionCode;

        public DomainExceptionCode DomainExceptionCode { get; protected set; }

        protected override void Verify(Exception exception)
        {
            Assert.IsInstanceOfType(exception, typeof(DomainException));
            Assert.AreEqual(((DomainException)exception).ExceptionCode, DomainExceptionCode);
        }
    }
}
