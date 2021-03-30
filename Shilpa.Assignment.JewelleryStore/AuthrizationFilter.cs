using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shilpa.Assignment.Database.DAL;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Shilpa.Assignment.JewelleryStore
{
    public class AuthrizationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            
            base.OnActionExecuted(context);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var bearerToken = (context.HttpContext.Request.Headers["Authorization"]).ToString();
            var token = bearerToken.Split(" ")[1];
            var handler = new JwtSecurityTokenHandler();

            var jsonToken = handler.ReadJwtToken(token);

            string userName = jsonToken.Payload.First().Value?.ToString()?.Split("@")[0];
            using(var db=new JewelleryContext())
            {
                if (db.Users.Any(x => x.UserName == userName &&x.Token==token))
                {
                    base.OnActionExecuting(context);
                }
                else
                {
                    context.Result = new JsonResult(new { HttpStatusCode.Unauthorized });
                }
            }
           
        }
    }
}
