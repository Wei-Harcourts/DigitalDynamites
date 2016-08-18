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
            Template = ConfigurationManager.AppSettings["Template"];
            FbNotificationHref = ConfigurationManager.AppSettings["FbNotificationHref"];
        }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string RedirectUri { get; set; }

        public string Template { get; set; }

        public string FbNotificationHref { get; set; }
    }
}