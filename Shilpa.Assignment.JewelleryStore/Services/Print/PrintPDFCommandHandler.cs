using MediatR;
using Shilpa.Assignment.JewelleryStore.Features.Print.Model;
using Shilpa.Assignment.JewelleryStore.Model;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Grid;
using System;
using System.Data;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Shilpa.Assignment.JewelleryStore.Services.Print
{
    public class PrintPDFCommandHandler : IRequestHandler<PrintPDFCommand, SuccessResponseDTO<PrintModel>>
    {
        public async Task<SuccessResponseDTO<PrintModel>> Handle(PrintPDFCommand request, CancellationToken cancellationToken)
        {
            SuccessResponseDTO<PrintModel> successResponseDTO = new SuccessResponseDTO<PrintModel>();
            try
            {
                //Create a new PDF document
                PdfDocument doc = new PdfDocument();
                //Add a page
                PdfPage page = doc.Pages.Add();
                //Create a PdfGrid
                PdfGrid pdfGrid = new PdfGrid();
                //Create a DataTable
                DataTable dataTable = new DataTable();
                //Add columns to the DataTable
                dataTable.Columns.Add("GoldPrice");
                dataTable.Columns.Add("Weight");
                dataTable.Columns.Add("Discount");
                dataTable.Columns.Add("Total Price");
                //Add rows to the DataTable
                dataTable.Rows.Add(new object[] { request.GoldPrice,request.Weight,request.Discount,request.TotalPrice});
                //Assign data source
                pdfGrid.DataSource = dataTable;
                //Draw grid to the page of PDF document
                pdfGrid.Draw(page, 10,10,40);
                //Save the PDF document to stream
                MemoryStream stream = new MemoryStream();
                doc.Save(stream);
                //If the position is not set to '0' then the PDF will be empty.
                stream.Position = 0;
                //Close the document.
                doc.Close(true);
                PrintModel printModel = new PrintModel() { stream=stream };
                successResponseDTO = new SuccessResponseDTO<PrintModel>().CreateSuccessResponse(printModel, "500", "document created", "", "", request.TenantName);
            }
            catch (Exception ex)
            {
                successResponseDTO = new SuccessResponseDTO<PrintModel>().CreateSuccessResponse(null, "500", ex.ToString(), "", "", request.TenantName);

            }
            return successResponseDTO;
        }
    }
}
