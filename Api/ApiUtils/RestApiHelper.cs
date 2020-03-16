using RestSharp;

namespace Api.ApiUtils
{
    public class RestApiHelper
    {
        private RestClient _restClient;
        private readonly RestRequest _restRequest;
        private const string BaseUrl = "https://api.github.com/";
        private const string BearerToken = "";
        
        public RestApiHelper(string resourceUrl)
        {
            _restRequest = new RestRequest {Resource = resourceUrl};
            _restRequest.AddParameter("Accept", "application/json", ParameterType.HttpHeader);
        }

        public RestClient CreatRestClient()
        {
            _restClient = new RestClient(BaseUrl);
            _restClient.AddDefaultHeader("Authorization", $"Bearer {BearerToken}");
            return _restClient;
        }

        public RestRequest CreateGetRequest()
        {
            _restRequest.Method = Method.GET;
            return _restRequest;
        }

        public RestRequest CreatePostRequest(object jsonObject)
        {
            _restRequest.Method = Method.POST;
            _restRequest.RequestFormat = DataFormat.Json;
            _restRequest.AddJsonBody(jsonObject);
            return _restRequest;
        }

        public RestRequest CreatePatchRequest(object jsonObject)
        {
            _restRequest.Method = Method.PATCH;
            _restRequest.RequestFormat = DataFormat.Json;
            _restRequest.AddJsonBody(jsonObject);
            return _restRequest;
        }

        public RestRequest CreatePutRequest(string jsonString)
        {
            _restRequest.Method = Method.PUT;
            _restRequest.AddParameter("application/json", jsonString, ParameterType.RequestBody);
            return _restRequest;
        }

        public RestRequest CreateDeleteRequest()
        {
            _restRequest.Method = Method.DELETE;
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