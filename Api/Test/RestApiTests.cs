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
            var repositories = CommonSteps.GetRepositories();
            var repositoriesList = RestApiHelper.GetContent<List<RepositoryData>>(repositories);
            Assert.That(repositoriesList.Count != 0);
            Assert.That(repositories.StatusDescription == "OK");

            Log.Step(2, "Posting new repository");
            var repository = new RepositoryData("ThatIsNewRepository");
            var postRepository = CommonSteps.PostRepository(repository);
            Assert.That(postRepository.StatusDescription == "Created");

            Log.Step(3, "Asserting new repository presence in repositories list");
            var repositoriesAfterPosting = CommonSteps.GetRepositories();
            var repositoriesListAfterPosting = RestApiHelper.GetContent<List<RepositoryData>>(repositoriesAfterPosting);
            Assert.True(repositoriesListAfterPosting.Any(rep => rep.name == repository.name));

            Log.Step(4, "Updating name of posted repository");
            var newRepository = new RepositoryData("ThatIsUpdatedRepository");
            var updateRepositoryName =
                CommonSteps.UpdateRepositoryName(repository.name, newRepository);
            var repositoriesListAfterUpdate = RestApiHelper.GetContent<RepositoryData>(updateRepositoryName);
            Assert.That(updateRepositoryName.StatusDescription == "OK");
            Assert.That(repositoriesListAfterUpdate.name.Equals(newRepository.name));

            Log.Step(5, "Asserting renamed repository presence in repositories list");
            var repositoriesAfterUpdateName = CommonSteps.GetRepositories();
            var repositoriesListAfterUpdateNameContent =
                RestApiHelper.GetContent<List<RepositoryData>>(repositoriesAfterUpdateName);
            Assert.True(repositoriesListAfterUpdateNameContent.Any(repo => repo.name == newRepository.name));

            Log.Step(6, "Delete created repository");
            var deleteRepository = CommonSteps.DeleteRepository(newRepository.name);
            Assert.That(deleteRepository.StatusCode.ToString().Equals("NoContent"));

            Log.Step(7, "Asserting that repository is deleted");
            var repositoriesAfterDelete = CommonSteps.GetRepositories();
            var repositoriesListAfterDelete = RestApiHelper.GetContent<List<RepositoryData>>(repositoriesAfterDelete);
            Assert.That(repositoriesAfterDelete.StatusDescription == "OK");
            Assert.That(repositoriesListAfterDelete.Any(repo => repo.name != newRepository.name));
        }
    }
}