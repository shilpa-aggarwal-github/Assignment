using MediatR;
using Shilpa.Assignment.Database.DAL;
using Shilpa.Assignment.JewelleryStore.Features.Account;
using Shilpa.Assignment.JewelleryStore.Features.Account.Model;
using Shilpa.Assignment.JewelleryStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shilpa.Assignment.JewelleryStore.Services.Account
{
    public class LoginUserCommandHandler:IRequestHandler<LoginUserCommand, SuccessResponseDTO<LoginResponseModel>>
    {
        public  JewelleryContext _jewelleryContext { get; set; }

        public LoginUserCommandHandler(JewelleryContext jewelleryContext)
        {
            _jewelleryContext = jewelleryContext;
        }
        public async Task<SuccessResponseDTO<LoginResponseModel>> Handle(LoginUserCommand request,CancellationToken cancellationToken)
        {
            SuccessResponseDTO<LoginResponseModel> successResponseDTO;
            try
            {
                if (!string.IsNullOrEmpty(request.Password))
                {
                    //decrypt password
                    string password = EncryptionDecryptionUsingSymmetricKey.EncryptString(request.SceretKey, request.Password);
                    if (!string.IsNullOrEmpty(request.Email))
                    {
                        string email = request.Email?.Split("@")[0];
                        //get tenant id from db
                        Guid TenantId = _jewelleryContext.Tenants.Where(t => t.TenantName == request.TenantName).Select(t => t.TenantId).FirstOrDefault();
                        if (TenantId != null && TenantId != Guid.Empty)
                        {
                            //get user details from db
                            LoginResponseModel loginResponseModel = _jewelleryContext.Users.Where(u => u.TenantId == TenantId && u.IsActive && u.UserName == email && u.Password == password).Join(_jewelleryContext.Roles, user => user.RoleId, role => role.RoleId, (user, role) => new LoginResponseModel
                            {
                                TenantId = user.TenantId,
                                Email = user.UserName,
                                FirstName = user.FirstName,
                                LastName = user.LastName,
                                Role = role.RoleName,
                                UserId = user.UserId,
                                IsDiscount = role.RoleName == Constants.Privileged ? true : false
                            }).FirstOrDefault();
                            if (loginResponseModel != null)
                            {                       
                                //generate jwt token
                                string token = request.JWTAuth.GenerateToken(loginResponseModel, DateTime.UtcNow.AddMinutes(60));
                                if (!string.IsNullOrEmpty(token))
                                {
                                    var user = _jewelleryContext.Users.Where(u => u.UserId == loginResponseModel.UserId).First();
                                    user.Token = token;
                                    _jewelleryContext.SaveChanges();
                                    loginResponseModel.Token = token;
                                    successResponseDTO = new SuccessResponseDTO<LoginResponseModel>().CreateSuccessResponse(loginResponseModel, "200", Constants.Sucessfully_logged_in, Convert.ToString(loginResponseModel.TenantId), "", request.TenantName);
                                    return successResponseDTO;
                                }
                                else
                                {
                                    successResponseDTO = new SuccessResponseDTO<LoginResponseModel>().CreateSuccessResponse(loginResponseModel, "500", Constants.Token_is_not_generated, Convert.ToString(loginResponseModel.TenantId), "", request.TenantName);
                                    return successResponseDTO;
                                }
                            }
                            else
                            {
                                successResponseDTO = new SuccessResponseDTO<LoginResponseModel>().CreateSuccessResponse(new LoginResponseModel(), "500", Constants.Username_or_password_is_incorrect, null, "", request.TenantName);
                                return successResponseDTO;
                            }
                        }
                        else
                        {
                            successResponseDTO = new SuccessResponseDTO<LoginResponseModel>().CreateSuccessResponse(new LoginResponseModel(), "500", Constants.Client_doesn_not_exist, "", "", request.TenantName);
                            return successResponseDTO;
                        }
                    }
                    else
                    {
                        successResponseDTO = new SuccessResponseDTO<LoginResponseModel>().CreateSuccessResponse(new LoginResponseModel(), "500", Constants.Email_is_required, "", "", request.TenantName);
                        return successResponseDTO;
                    }
                }
                else
                {
                    successResponseDTO = new SuccessResponseDTO<LoginResponseModel>().CreateSuccessResponse(new LoginResponseModel(), "500", Constants.Password_is_required, "", "", request.TenantName);
                    return successResponseDTO;
                }

            }
            catch(Exception ex)
            {
                //log exception
                successResponseDTO = new SuccessResponseDTO<LoginResponseModel>().CreateSuccessResponse(new LoginResponseModel(), "500",ex.ToString(), "", "", request.TenantName);
                return successResponseDTO;
            }

        }
    }
}
