using System.Configuration;

namespace Harcourts.eOpen.Web.Settings
{
    public class AppSettings
    {
        public AppSettings()
        {
            ClientId = ConfigurationManager.AppSettings["AppClientId"];
            ClientSecret = ConfigurationManager.AppSettings["AppClientSecret"];
            RedirectUri = ConfigurationManager.AppSettings["AppRedirectUri"];
        }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string RedirectUri { get; set; }
    }
}