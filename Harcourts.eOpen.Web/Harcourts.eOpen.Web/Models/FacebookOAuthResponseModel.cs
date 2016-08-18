namespace Harcourts.eOpen.Web.Models
{
    public class FacebookOAuthResponseModel
    {
        public string access_token { get; set; }

        public string token_type { get; set; }

        public string expires_in { get; set; }
    }
}