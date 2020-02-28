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

        public static IRestResponse GetRepositories()
        {
            var restApi = new RestApiHelper(GetRepos);
            var restClient = restApi.CreatRestClient();
            var restRequest = restApi.CreateGetRequest();
            return restApi.GetResponse(restClient, restRequest);
        }

        public static IRestResponse PostRepository(RepositoryData repositoryData)
        {
            var restApi = new RestApiHelper(PostRepo);
            var restClient = restApi.CreatRestClient();
            var serializeObject = JsonConvert.SerializeObject(repositoryData);
            var restRequest = restApi.CreatePostRequest(serializeObject);
            return restApi.GetResponse(restClient, restRequest);
        }

        public static IRestResponse DeleteRepository(string repositoryName)
        {
            var restApi = new RestApiHelper(DeleteRepo + repositoryName);
            var restClient = restApi.CreatRestClient();
            var restRequest = restApi.CreateDeleteRequest();
            return restApi.GetResponse(restClient, restRequest);
        }

        public static IRestResponse UpdateRepositoryName(string repositoryName,
            RepositoryData repositoryData)
        {
            var restApi = new RestApiHelper(DeleteRepo + repositoryName);
            var restClient = restApi.CreatRestClient();
            var serializeObject = JsonConvert.SerializeObject(repositoryData);
            var restRequest = restApi.CreatePatchRequest(serializeObject);
            return restApi.GetResponse(restClient, restRequest);
        }
    }
}