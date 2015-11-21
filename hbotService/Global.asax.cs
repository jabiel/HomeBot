using Autofac;
using hbotService.Services;
using Microsoft.WindowsAzure.Mobile.Service;
using Microsoft.WindowsAzure.Mobile.Service.Config;
using System.Web.Http;
using System.Web.Routing;

namespace hbotService
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            WebApiConfig.Register();
            
        }

        
    }
}