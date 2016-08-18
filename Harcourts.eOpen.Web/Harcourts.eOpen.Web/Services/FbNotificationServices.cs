using System;
using Harcourts.eOpen.Web.Constants;
using Harcourts.eOpen.Web.Settings;
using RestSharp;

namespace Harcourts.eOpen.Web.Services
{
    /// <summary>
    /// Responsible for pushing notifications to facebook
    /// </summary>
    public class FbNotificationServices
    {
        private readonly FbLoginServices _fbLoginServices;
        private readonly AppSettings _appSettings;

        public FbNotificationServices()
        {
            _fbLoginServices = new FbLoginServices();
            _appSettings = new AppSettings();
        }

        public void PushFbNotification(string userId)
        {
            var responseModel = _fbLoginServices.AuthenticateClient();
            var restClient = new RestClient(new Uri(
                $"https://graph.facebook.com/v2.3/{userId}/notifications"));

            var restRequest = new RestRequest(Method.POST);
            restRequest.Parameters.Add(new Parameter
            {
                Name = StringConstants.Access_Token,
                Type = ParameterType.QueryString,
                Value = responseModel.access_token
            });
            restRequest.Parameters.Add(new Parameter
            {
                Name = StringConstants.Href,
                Type = ParameterType.QueryString,
                Value = _appSettings.FbNotificationHref
            });

            restRequest.Parameters.Add(
                new Parameter
                {
                    Name = StringConstants.Template,
                    Type = ParameterType.QueryString,
                    Value = string.Format(_appSettings.Template, userId)
                });
            var response = restClient.ExecuteAsPost(restRequest, StringConstants.Post);
        }
    }
}