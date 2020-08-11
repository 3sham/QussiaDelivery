using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading;
using System.Security.Principal;

namespace QussiaDelivery.Models
{
    public class BasicAuthentication : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            HandelError<ItemModel> handelError = new HandelError<ItemModel>();
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                
            }
            else
            {
                string AuthToken = actionContext.Request.Headers.Authorization.Parameter;
                string deCodeAuthToken = Encoding.UTF8.GetString(Convert.FromBase64String(AuthToken));
                string [] usernamepasswoordArray =  deCodeAuthToken.Split(':');
                string username = usernamepasswoordArray[0];
                string password = usernamepasswoordArray[1];

                if(UsersSecurity.Login(username,password))
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username), null);
                }
                else
                {
                    handelError.data = null;
                    var message = string.Format("User Unauthorized");
                    handelError.status = false;
                    handelError.message = message;
                   
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized,handelError);
                }
            }
        }
    }
}