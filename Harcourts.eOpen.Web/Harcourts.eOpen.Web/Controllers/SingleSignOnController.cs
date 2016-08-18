using System;
using System.Web.Http;
using System.Web.Mvc;
using Facebook;
using Harcourts.eOpen.Web.Models;
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
        public SingleSignOnController()
        {
            _appSettings = new AppSettings();
            _facebookClient = new FacebookClient();
        }

        // GET: SingleSignOn
        [System.Web.Mvc.Route("facebook")]
        public ActionResult Index()
        {
            var faceBookUri = string.Format(
                "https://www.facebook.com/dialog/oauth?client_id={0}&redirect_uri={1}",
                _appSettings.ClientId,
                _appSettings.RedirectUri + "login/complete");
            return Redirect(faceBookUri);
        }

        [System.Web.Mvc.Route("complete")]
        public ViewResult GetAccessTokenFromExternal([FromUri]string code)
        {
            var restClient = new RestClient {BaseUrl = new Uri("https://graph.facebook.com/v2.3/oauth/access_token")};
            var restRequest = new RestRequest(Method.GET);
            var clientIdParameter = new Parameter {Name = "client_id", Type = ParameterType.QueryString, Value = _appSettings.ClientId};
            var clientSecretParameter = new Parameter { Name = "client_secret", Type = ParameterType.QueryString, Value = _appSettings.ClientSecret };
            var redirectUriParameter = new Parameter { Name = "redirect_uri", Type = ParameterType.QueryString, Value = _appSettings.RedirectUri+"login/complete" };
            var accessTokenParameter = new Parameter { Name = "code", Type = ParameterType.QueryString, Value = code };

            restRequest.Parameters.Add(clientIdParameter);
            restRequest.Parameters.Add(clientSecretParameter);
            restRequest.Parameters.Add(redirectUriParameter);
            restRequest.Parameters.Add(accessTokenParameter);

            var restResponse = restClient.ExecuteAsGet<FacebookOAuthResponseModel>(restRequest, "Get");
            var data = restResponse.Data;

            _facebookClient.AccessToken = data.access_token;
            var response = _facebookClient.Get<FacebookUserModel>("/me", null);
            response.AuthResponseModel = data;
            Session[response.email] = response;

            return View("Index", response);
        }
    }
}