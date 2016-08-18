using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Facebook;
using Harcourts.eOpen.Web.Models;
using Harcourts.eOpen.Web.Services;
using Harcourts.eOpen.Web.Settings;
using RestSharp;

namespace Harcourts.eOpen.Web.Controllers
{
    /// <summary>
    /// Responsible for single signing in the user using third party auth server(facebook, google etc).
    /// </summary>
    [System.Web.Mvc.RoutePrefix("login")]
    public class SingleSignOnController : Controller
    {
        private readonly AppSettings _appSettings;
        private readonly FacebookClient _facebookClient;
        private readonly FbLoginServices _fbLoginServices;
        private readonly FbNotificationServices _fbNotificationServices;
        public SingleSignOnController()
        {
            _appSettings = new AppSettings();
            _facebookClient = new FacebookClient();
            _fbLoginServices = new FbLoginServices();
            _fbNotificationServices = new FbNotificationServices();
        }

        // GET: SingleSignOn
        [System.Web.Mvc.Route("facebook")]
        public ActionResult Index()
        {
            var faceBookUri =
                $"https://www.facebook.com/dialog/oauth?client_id={_appSettings.ClientId}&redirect_uri={_appSettings.RedirectUri + "login/complete"}";
            return Redirect(faceBookUri);
        }

        [System.Web.Mvc.Route("complete")]
        public ActionResult GetAccessTokenFromExternal([FromUri] string code)
        {
            var model = _fbLoginServices.CompleteOAuth(code);
            _facebookClient.AccessToken = model.access_token;
            var response = _facebookClient.Get<FacebookUserModel>("/me", null);
            response.AuthResponseModel = model;
            Session[response.email] = response;
            Response.Cookies.Add(new System.Web.HttpCookie("Email", response.email));
            var url = Url.RouteUrl("AddNewVisitor") + "?from=" + response.email;
            return Redirect(url);
        }

        [System.Web.Mvc.Route("push")]
        public void PushFbNotification()
        {
            var email = Request.Cookies["Email"].Value;
            var model = (FacebookUserModel)Session[email];
            var userId = model.id;
            _fbNotificationServices.PushFbNotification(userId);
        }
    }
}