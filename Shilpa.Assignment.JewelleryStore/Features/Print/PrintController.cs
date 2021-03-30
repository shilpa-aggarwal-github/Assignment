using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Shilpa.Assignment.JewelleryStore.Features.Print.Model;
using Shilpa.Assignment.JewelleryStore.Model;
using Shilpa.Assignment.JewelleryStore.Services.Print;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Shilpa.Assignment.JewelleryStore.Features.Print
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class PrintController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PrintController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/<PrintController>
        [HttpGet]
        [Route("DownloadFile")]
        public async Task<FileStreamResult> DownloadFileAsync(string goldPrice,string weight,string discount,string totalPrice,string tenantName)
        {
            SuccessResponseDTO<PrintModel> response = await _mediator.Send(new PrintPDFCommand()
            {
                Discount = discount,
                GoldPrice = goldPrice,
                Weight = weight,
                TenantName = tenantName,
                TotalPrice=totalPrice
            });
            //Defining the ContentType for pdf file.
            string contentType = "application/pdf";
            //Define the file name.
            string fileName = $"{tenantName}{DateTime.UtcNow}.pdf";
            return File(response?.Response?.stream, contentType, fileName);

        }
        /// <summary>
        /// calculate price of gold
        /// </summary>
        /// <param name="goldPrice"></param>
        /// <param name="weight"></param>
        /// <param name="discount"></param>
        /// <param name="tenantName"></param>
        /// <returns></returns>
        [HttpGet]
        [ServiceFilter(typeof(AuthrizationFilter))]
        [Route("CalculatePrice")]
        public async Task<IActionResult> CalculatePriceAsync(string goldPrice, string weight, string discount,string tenantName)
        {
            SuccessResponseDTO<string> totalPrice = await _mediator.Send(new CalculatePriceCommand()
            {
                Discount=discount,
                GoldPrice=goldPrice,
                Weight=weight,
                TenantName=tenantName
            });
            return Ok(totalPrice);
        }
    }
}
