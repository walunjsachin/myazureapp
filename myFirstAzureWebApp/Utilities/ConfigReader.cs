using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace myFirstAzureWebApp.Utilities
{
    public static class ConfigReader
    {
        public static string GetApplicationString(string keyName)
        {
            return ConfigurationManager.AppSettings[keyName];
        }
    }
}