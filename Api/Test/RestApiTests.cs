using System.Collections.Generic;
using System.Linq;
using Api.ApiUtils;
using Api.Model;
using Api.Steps;
using Framework.BaseClasses;
using NUnit.Framework;

namespace Api.Test
{
    public class RestApiTests : BaseEntity
    {
        
        [Test]
        public void GitHubTest()
        {
            Log.Step(1, "Getting repositories");
            var repositoriesResponse = CommonSteps.GetRepositories();
            var repositoriesList = repositoriesResponse.GetContent<List<RepositoryData>>();
            Assert.That(repositoriesList.Count != 0);
            Assert.That(repositoriesResponse.StatusDescription == "OK");

            Log.Step(2, "Posting new repository");
            var repository = new RepositoryData("ThatIsNewRepository");
            var postRepository = CommonSteps.PostRepository(repository);
            Assert.That(postRepository.StatusDescription == "Created");

            Log.Step(3, "Asserting new repository presence in repositories list");
            var repositoriesAfterPostingResponse = CommonSteps.GetRepositories();
            var repositoriesListAfterPosting = repositoriesAfterPostingResponse.GetContent<List<RepositoryData>>();
            Assert.True(repositoriesListAfterPosting.Any(repo => repo.Name == repository.Name));

            Log.Step(4, "Updating name of posted repository");
            var newRepository = new RepositoryData("ThatIsUpdatedRepository");
            var updateRepositoryNameResponse =
                CommonSteps.UpdateRepositoryName(repository.Name, newRepository);
            var repositoriesListAfterUpdate = updateRepositoryNameResponse.GetContent<RepositoryData>();
            Assert.That(updateRepositoryNameResponse.StatusDescription == "OK");
            Assert.That(repositoriesListAfterUpdate.Name.Equals(newRepository.Name));

            Log.Step(5, "Asserting renamed repository presence in repositories list");
            var repositoriesAfterUpdateNameResponse = CommonSteps.GetRepositories();
            var repositoriesListAfterUpdateNameContent =
                repositoriesAfterUpdateNameResponse.GetContent<List<RepositoryData>>();
            Assert.True(repositoriesListAfterUpdateNameContent.Any(repo => repo.Name == newRepository.Name));

            Log.Step(6, "Delete created repository");
            var deleteRepositoryResponse = CommonSteps.DeleteRepository(newRepository.Name);
            Assert.That(deleteRepositoryResponse.StatusCode.ToString().Equals("NoContent"));

            Log.Step(7, "Asserting that repository is deleted");
            var repositoriesAfterDeleteResponse = CommonSteps.GetRepositories();
            var repositoriesListAfterDelete = repositoriesAfterDeleteResponse.GetContent<List<RepositoryData>>();
            Assert.That(repositoriesAfterDeleteResponse.StatusDescription == "OK");
            Assert.That(repositoriesListAfterDelete.Any(repo => repo.Name != newRepository.Name));
        }
    }
}