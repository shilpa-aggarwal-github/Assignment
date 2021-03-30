using MediatR;
using Shilpa.Assignment.JewelleryStore.Features.Print.Model;
using Shilpa.Assignment.JewelleryStore.Model;

namespace Shilpa.Assignment.JewelleryStore.Services.Print
{
    public class PrintPDFCommand : IRequest<SuccessResponseDTO<PrintModel>>
    {
        public string GoldPrice { get; set; }
        public string Weight { get; set; }
        public string Discount { get; set; }
        public string TenantName { get; set; }
        public string TotalPrice { get; set; }
    }
}
