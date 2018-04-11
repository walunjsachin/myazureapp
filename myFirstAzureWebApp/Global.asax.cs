using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Azure.KeyVault;
using System.Web.Configuration;
using myFirstAzureWebApp.Utilities;

namespace myFirstAzureWebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var kv = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(EncryptionHelper.GetToken));
            var sec = kv.GetSecretAsync(ConfigReader.GetApplicationString("SecretUri"));

            //I put a variable in a Utils class to hold the secret for general  application use.
            EncryptionHelper.EncryptSecret = sec.Result.Value;

        }
    }
}
