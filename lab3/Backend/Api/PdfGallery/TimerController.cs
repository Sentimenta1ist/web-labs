using Elfo.POC.PdfGallery.Api.PdfGallery;
using Elfo.Round.ReadCycle;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Elfo.POC.Timer.Api.PdfGallery
{
	[Route("api/timer")]
	public class TimerController : ControllerBase
	{
		private readonly IMediator mediator;
		private readonly IWebHostEnvironment webHostEnvironment;
		IList<GetItemTimerQuery> list;

		public TimerController(IMediator mediator, IWebHostEnvironment webHostEnvironment, IList<GetItemTimerQuery> list)
		{

			this.mediator = mediator;
			this.webHostEnvironment = webHostEnvironment;
			this.list = list;
		}

		[HttpPost("saveTime")]
		public ActionResult SaveTime([FromBody] GetItemTimerQuery query)
		{
			list.Add(query);
			return this.Ok();
		}

		[HttpPost("getTimer")]
		public ActionResult<ListResult<GetItemTimerQuery>> GetTime()
		{
			list.Add(new GetItemTimerQuery { Name = "test", Time = "00:30" });
			return this.Ok(new ListResult<GetItemTimerQuery>
			{
				Records = list,
				RecordsCount = list.Count,
				ModelType = typeof(GetItemTimerQuery).AssemblyQualifiedName
			});
		}
	}
}
