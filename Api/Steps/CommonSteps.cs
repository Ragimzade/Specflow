using Api.ApiUtils;
using Api.Model;
using Newtonsoft.Json;
using RestSharp;

namespace Api.Steps
{
    public static class CommonSteps
    {
        private const string GetRepos = "/user/repos";
        private const string PostRepo = "/user/repos";
        private const string DeleteRepo = "/repos/Ragimzade/";

        private static RestApiHelper CreateRestApiHelper(string resourceUrl, out RestClient restClient)
        {
            var restApi = new RestApiHelper(resourceUrl);
            restClient = restApi.CreatRestClient();
            return restApi;
        }

        public static IRestResponse GetRepositories()
        {
            var restApi = CreateRestApiHelper(GetRepos, out var restClient);
            var restRequest = restApi.CreateGetRequest();
            return restApi.GetResponse(restClient, restRequest);
        }

        public static IRestResponse CreateRepository(RepositoryData repositoryData)
        {
            var restApi = CreateRestApiHelper(PostRepo, out var restClient);
            var serializeObject = JsonConvert.SerializeObject(repositoryData);
            var restRequest = restApi.CreatePostRequest(serializeObject);
            return restApi.GetResponse(restClient, restRequest);
        }

        public static IRestResponse DeleteRepository(string repositoryName)
        {
            var restApi = CreateRestApiHelper(DeleteRepo + repositoryName, out var restClient);
            var restRequest = restApi.CreateDeleteRequest();
            return restApi.GetResponse(restClient, restRequest);
        }
        
        public static IRestResponse UpdateRepositoryName(string repositoryName,
            RepositoryData repositoryData)
        {
            var restApi = CreateRestApiHelper(DeleteRepo + repositoryName, out var restClient);
            var serializeObject = JsonConvert.SerializeObject(repositoryData);
            var restRequest = restApi.CreatePatchRequest(serializeObject);
            return restApi.GetResponse(restClient, restRequest);
        }
    }
}