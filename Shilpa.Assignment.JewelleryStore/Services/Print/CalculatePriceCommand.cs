using MediatR;
using Shilpa.Assignment.JewelleryStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shilpa.Assignment.JewelleryStore.Services.Print
{
    public class CalculatePriceCommand : IRequest<SuccessResponseDTO<string>>
    {
        public string GoldPrice { get; set; }
        public string Weight { get; set; }
        public string Discount { get; set; }
        public string TenantName { get; set; }
    }
}

