namespace Harcourts.eOpen.Web.Models
{
    public class FacebookUserModel
    {
        public string id { get; set; }

        public string email { get; set; }

        public string first_name { get; set; }

        public string last_name { get; set; }

        public FacebookOAuthResponseModel AuthResponseModel { get; set; }

    }
}