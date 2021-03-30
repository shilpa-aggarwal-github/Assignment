using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shilpa.Assignment.Database.DAL;
using Shilpa.Assignment.JewelleryStore.Features.Account.Model;
using Shilpa.Assignment.JewelleryStore.Model;
using Shilpa.Assignment.JewelleryStore.Services.Account;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Shilpa.Assignment.JewelleryStore.Features.Account.Controller
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IJWTAuth _jwtAuth;
        private readonly JewelleryContext _jewelleryContext;
        private readonly AppSettings _appSettings;
        public AccountController(IMediator mediator,IJWTAuth jwtAuth, JewelleryContext jewelleryContext, IOptions<AppSettings> appSettings)
        {
            _mediator = mediator;
            _jwtAuth = jwtAuth;
            _jewelleryContext = jewelleryContext;
            _appSettings = appSettings.Value;
        }
        

        /// <summary>
        /// Login in application and create jwt token
        /// </summary>
        /// <param name="loginRequestModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]        
        public async Task<IActionResult> LoginAsync(LoginRequestModel loginRequestModel)
        {
            SuccessResponseDTO<LoginResponseModel> loginResponseModel = new SuccessResponseDTO<LoginResponseModel>();
            if (ModelState.IsValid)
            {               
                loginResponseModel = await _mediator.Send(new LoginUserCommand()
                {
                    Email = loginRequestModel.Email,
                    Password = loginRequestModel.Password,
                    TenantName = loginRequestModel.TenantName,
                    SceretKey = _appSettings.EncryptionKey,
                    JWTAuth = _jwtAuth
                });
            }
            else
            {
                ErrorResponseDTO err = new ErrorResponseDTO
                {
                    TenantName =loginRequestModel.TenantName,
                    ErrorType="required input is missing",
                    MessageCode="500",
                    RequestId=Convert.ToString(Guid.NewGuid())
                };
                return Ok(err);
            }
            return Ok(loginResponseModel);
        }

     
    }
}
