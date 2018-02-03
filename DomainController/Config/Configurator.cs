using System.Collections.Specialized;
using System.Configuration;

namespace DomainController.Config
{
    public static class Configurator
    {
        private static NameValueCollection AppSettings => ConfigurationManager.AppSettings;

        public static string GetDomainPath => AppSettings["DomainAddress"];
        public static string GetDomainUser => AppSettings["DomainUser"];
        public static string GetDomainPassword => AppSettings["DomainPassword"];
    }
}
