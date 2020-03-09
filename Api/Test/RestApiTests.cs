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
            repositoriesList.Count.Should().NotBe(0,
                "Repositories quantity shouldn't be 0 when getting repositories");
            repositoriesResponse.StatusDescription.Should().Be("OK",
                "Status should be 'OK' when getting repositories");

            Log.Step(2, "Creating new repository");
            var repository = new RepositoryData("ThatIsNewRepository");
            var createRepository = CommonSteps.CreateRepository(repository);
            createRepository.StatusDescription.Should().Be("Created",
                "Status Description should be 'Created' when creating a repository");

            Log.Step(3, "Asserting new repository presence in repositories list");
            var repositoriesAfterCreationResponse = CommonSteps.GetRepositories();
            var repositoriesListAfterCreation = repositoriesAfterCreationResponse.GetContent<List<RepositoryData>>();
            repositoriesListAfterCreation.Any(repo => repo.Name == repository.Name)
                .Should().Be(true, "Created repository should be present in repository list");

            Log.Step(4, "Updating name of posted repository");
            var newRepository = new RepositoryData("ThatIsUpdatedRepository");
            var updateRepositoryNameResponse =
                CommonSteps.UpdateRepositoryName(repository.Name, newRepository);
            var repositoryDataAfterUpdate = updateRepositoryNameResponse.GetContent<RepositoryData>();
            updateRepositoryNameResponse.StatusDescription.Should().Be("OK",
                "Status should be 'OK' when updating repository name");
            repositoryDataAfterUpdate.Name.Should().Be(newRepository.Name,
                $"Repository name should be {newRepository.Name} after updating name ");

            Log.Step(5, "Asserting renamed repository presence in repositories list");
            var repositoriesAfterUpdateNameResponse = CommonSteps.GetRepositories();
            var repositoriesListAfterUpdateNameContent =
                repositoriesAfterUpdateNameResponse.GetContent<List<RepositoryData>>();
            repositoriesListAfterUpdateNameContent.Any(repo => repo.Name == newRepository.Name)
                .Should().Be(true,
                    $"Repository with name {newRepository.Name} should be present in repository list");

            Log.Step(6, "Delete created repository");
            var deleteRepositoryResponse = CommonSteps.DeleteRepository(newRepository.Name);
            deleteRepositoryResponse.StatusCode.ToString().Should().Be("NoContent",
                "StatusCode should be 'NoContent' when deleting repository");

            Log.Step(7, "Asserting that repository is deleted");
            var repositoriesAfterDeleteResponse = CommonSteps.GetRepositories();
            var repositoriesListAfterDelete = repositoriesAfterDeleteResponse.GetContent<List<RepositoryData>>();
            repositoriesAfterDeleteResponse.StatusDescription.Should().Be("OK");
            repositoriesListAfterDelete.Any(repo => repo.Name != newRepository.Name)
                .Should().Be(true,
                    $"Deleted repository with name{newRepository.Name} shouldn't be present in repository list after deleting repository");
        }
    }
}