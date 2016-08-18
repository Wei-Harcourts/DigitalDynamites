using System;
using System.Collections.Generic;
using Harcourts.eOpen.Web.Constants;
using Harcourts.eOpen.Web.Models;
using Harcourts.eOpen.Web.Settings;
using RestSharp;

namespace Harcourts.eOpen.Web.Services
{
    /// <summary>
    /// Responsible for completing oauth process with facebook.
    /// </summary>
    public class FbLoginServices
    {
        private readonly AppSettings _appSettings;

        public FbLoginServices()
        {
            _appSettings = new AppSettings();
        }

        /// <summary>
        /// Completing user oauth process by sharing client secret and the code.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public FacebookOAuthResponseModel CompleteOAuth(string code)
        {
            var restClient = new RestClient { BaseUrl = new Uri("https://graph.facebook.com/v2.3/oauth/access_token") };
            var restRequest = new RestRequest(Method.GET);
            var parameters = GetClientLoginParameters();
            var accessTokenParameter = new Parameter { Name = StringConstants.Code, Type = ParameterType.QueryString, Value = code };
            foreach (var parameter in parameters)
            {
                restRequest.Parameters.Add(parameter);
            }
            restRequest.Parameters.Add(accessTokenParameter);

            var restResponse = restClient.ExecuteAsGet<FacebookOAuthResponseModel>(restRequest, StringConstants.Get);
            var data = restResponse.Data;
            return data;
        }

        /// <summary>
        /// Getting access token for the client by authenticating using client secret.
        /// </summary>
        /// <returns></returns>
        public FacebookOAuthResponseModel AuthenticateClient()
        {
            var restClient = new RestClient { BaseUrl = new Uri("https://graph.facebook.com/v2.3/oauth/access_token") };
            var restRequest = new RestRequest(Method.GET);
            var parameters = GetClientLoginParameters();
            foreach (var parameter in parameters)
            {
                restRequest.Parameters.Add(parameter);
            }
            restRequest.Parameters.Add(new Parameter
            {
                Name = StringConstants.Grant_Type,
                Type = ParameterType.QueryString,
                Value = StringConstants.Client_Credentials
            });
            var restResponse = restClient.ExecuteAsGet<FacebookOAuthResponseModel>(restRequest,StringConstants.Get);
            var data = restResponse.Data;
            return data;
        }

        private IEnumerable<Parameter> GetClientLoginParameters()
        {
            var clientIdParameter = new Parameter
            {
                Name = StringConstants.Client_Id,
                Type = ParameterType.QueryString,
                Value = _appSettings.ClientId
            };
            var clientSecretParameter = new Parameter
            {
                Name = StringConstants.Client_Secret,
                Type = ParameterType.QueryString,
                Value = _appSettings.ClientSecret
            };
            var redirectUriParameter = new Parameter
            {
                Name = StringConstants.Redirect_Uri,
                Type = ParameterType.QueryString,
                Value = _appSettings.RedirectUri + "login/complete"
            };
            return new List<Parameter> {clientIdParameter, clientSecretParameter, redirectUriParameter};
        }
    }
}