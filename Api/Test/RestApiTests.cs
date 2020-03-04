using System.Collections.Generic;
using System.Linq;
using Api.ApiUtils;
using Api.Model;
using Api.Steps;
using FluentAssertions;
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
            repositoriesList.Count.Should().NotBe(0);
            repositoriesResponse.StatusDescription.Should().Be("OK");

            Log.Step(2, "Posting new repository");
            var repository = new RepositoryData("ThatIsNewRepository");
            var postRepository = CommonSteps.CreateRepository(repository);
            postRepository.StatusDescription.Should().Be("Created");

            Log.Step(3, "Asserting new repository presence in repositories list");
            var repositoriesAfterPostingResponse = CommonSteps.GetRepositories();
            var repositoriesListAfterPosting = repositoriesAfterPostingResponse.GetContent<List<RepositoryData>>();
            repositoriesListAfterPosting.Any(repo => repo.Name == repository.Name).Should().Be(true);

            Log.Step(4, "Updating name of posted repository");
            var newRepository = new RepositoryData("ThatIsUpdatedRepository");
            var updateRepositoryNameResponse =
                CommonSteps.UpdateRepositoryName(repository.Name, newRepository);
            var repositoriesListAfterUpdate = updateRepositoryNameResponse.GetContent<RepositoryData>();
            updateRepositoryNameResponse.StatusDescription.Should().Be("OK");
            repositoriesListAfterUpdate.Name.Should().Be(newRepository.Name);

            Log.Step(5, "Asserting renamed repository presence in repositories list");
            var repositoriesAfterUpdateNameResponse = CommonSteps.GetRepositories();
            var repositoriesListAfterUpdateNameContent =
                repositoriesAfterUpdateNameResponse.GetContent<List<RepositoryData>>();
            repositoriesListAfterUpdateNameContent.Any(repo => repo.Name == newRepository.Name).Should().Be(true);

            Log.Step(6, "Delete created repository");
            var deleteRepositoryResponse = CommonSteps.DeleteRepository(newRepository.Name);
            deleteRepositoryResponse.StatusCode.ToString().Should().Be("NoContent");

            Log.Step(7, "Asserting that repository is deleted");
            var repositoriesAfterDeleteResponse = CommonSteps.GetRepositories();
            var repositoriesListAfterDelete = repositoriesAfterDeleteResponse.GetContent<List<RepositoryData>>();
            repositoriesAfterDeleteResponse.StatusDescription.Should().Be("OK");
            repositoriesListAfterDelete.Any(repo => repo.Name != newRepository.Name).Should().Be(true);
        }
    }
}