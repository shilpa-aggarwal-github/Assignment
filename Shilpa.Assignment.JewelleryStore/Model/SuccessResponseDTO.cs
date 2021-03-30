using Shilpa.Assignment.JewelleryStore.Features.Account.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shilpa.Assignment.JewelleryStore.Model
{
    public class SuccessResponseDTO<T>
    {
        /// <summary>
        /// Satatus Info for Success Action
        /// </summary>
        public StatusInfoDTO StatusInfo { set; get; }
        /// <summary>
        /// Data object of API
        /// </summary>
        public T Response { get; set; }

        public SuccessResponseDTO<T> CreateSuccessResponse(T response, string Status,string responseMessage, string tenantId, string requestId = "", string tenantName = "")
        {
            this.StatusInfo = new StatusInfoDTO()
            {
                RequestId = requestId,
                TenantId = tenantId,
                Status = responseMessage,
                MessageCode = Status,
                TenantName = tenantName
            };

            this.Response = response;
            return this;
        }

        public static implicit operator SuccessResponseDTO<T>(SuccessResponseDTO<LoginRequestModel> v)
        {
            throw new NotImplementedException();
        }
    }


    public class ErrorResponseDTO
    {
        /// <summary>
        /// Gets or Sets RequestId
        /// </summary>
        public string RequestId { get; set; }

        public string Status { get; set; }

        /// <summary>
        /// Gets or Sets ClientName
        /// </summary>
        public string TenantName { get; set; }

        /// <summary>
        /// Gets or Sets ErrorType
        /// </summary>
        public string ErrorType { get; set; }

        /// <summary>
        /// Gets or Sets Message
        /// </summary>
        public string MessageCode { get; set; }

        /// <summary>
        /// Gets or Sets Timestamp
        /// </summary>
        public DateTime? Timestamp { get; set; }

        public ErrorResponseDTO()
        {

            Timestamp = DateTime.UtcNow;
            //this.RequestId = Guid.NewGuid().ToString();
        }

    }

}
