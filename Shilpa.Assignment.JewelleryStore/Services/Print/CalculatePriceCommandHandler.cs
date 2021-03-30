using MediatR;
using Shilpa.Assignment.JewelleryStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shilpa.Assignment.JewelleryStore.Services.Print
{
    public class CalculatePriceCommandHandler : IRequestHandler<CalculatePriceCommand, SuccessResponseDTO<string>>
    {

        public async Task<SuccessResponseDTO<string>> Handle(CalculatePriceCommand request, CancellationToken cancellationToken)
        {
            SuccessResponseDTO<string> successResponseDTO = new SuccessResponseDTO<string>();
            try
            {
                if (string.IsNullOrEmpty(request.Discount))
                {
                    successResponseDTO = new SuccessResponseDTO<string>().CreateSuccessResponse(Convert.ToString(Convert.ToInt32(request.GoldPrice)*Convert.ToInt32(request.Weight)), "200", "price calculated sucessfully", "", "", request.TenantName);
                }
                else
                {
                    string price = Convert.ToString((Convert.ToInt32(request.GoldPrice) * Convert.ToInt32(request.Weight)/Convert.ToInt32(request.Discount)));
                    successResponseDTO = new SuccessResponseDTO<string>().CreateSuccessResponse(price, "200", "price calculated sucessfully", "", "", request.TenantName);

                }
            }
            catch(Exception ex)
            {
                successResponseDTO = new SuccessResponseDTO<string>().CreateSuccessResponse("", "500", ex.ToString(), "", "", request.TenantName);
               
            }
            return successResponseDTO;
        }
    }
}
