﻿using RestSharp;

namespace Api.ApiUtils
{
    public class RestApiHelper
    {
        private RestClient _restClient;
        private RestRequest _restRequest;
        private const string BaseUrl = "https://api.github.com/";
        private const string BearerToken = "";

        private string ResourceUrl { get; }

        public RestApiHelper(string resourceUrl)
        {
            ResourceUrl = resourceUrl;
            _restRequest = new RestRequest();
            _restRequest.AddHeader("Accept", "application/json");
        }

        public RestClient CreatRestClient()
        {
            _restClient = new RestClient(BaseUrl);
            _restClient.AddDefaultHeader("Authorization", $"Bearer {BearerToken}");
            return _restClient;
        }

        public RestRequest CreateGetRequest()
        {
            _restRequest = new RestRequest(ResourceUrl, Method.GET);
            return _restRequest;
        }

        public RestRequest CreatePostRequest(object jsonObject)
        {
            _restRequest = new RestRequest(ResourceUrl, Method.POST) {RequestFormat = DataFormat.Json};
            _restRequest.AddJsonBody(jsonObject);
            return _restRequest;
        }

        public RestRequest CreatePatchRequest(object jsonObject)
        {
            _restRequest = new RestRequest(ResourceUrl, Method.PATCH) {RequestFormat = DataFormat.Json};
            _restRequest.AddJsonBody(jsonObject);
            return _restRequest;
        }

        public RestRequest CreatePutRequest(string jsonString)
        {
            _restRequest = new RestRequest(Method.PUT);
            _restRequest.AddParameter("application/json", jsonString, ParameterType.RequestBody);
            return _restRequest;
        }

        public RestRequest CreateDeleteRequest()
        {
            _restRequest = new RestRequest(ResourceUrl, Method.DELETE);
            return _restRequest;
        }

        public IRestResponse GetResponse(RestClient restClient, RestRequest restRequest)
        {
            return restClient.Execute(restRequest);
        }
    }

    public static class RestApiHelperExtensions
    {
        public static TX GetContent<TX>(this IRestResponse response)
        {
            var content = response.Content;
            var deserializeObject = Newtonsoft.Json.JsonConvert.DeserializeObject<TX>(content);
            return deserializeObject;
        }
    }
}