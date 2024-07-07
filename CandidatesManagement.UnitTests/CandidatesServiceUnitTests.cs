using NUnit.Framework;
using Moq;
using System.Threading.Tasks;
using CandidatesManagement.Core.Entities;
using CandidatesManagement.Core.Repositories;
using CandidatesManagement.Application.Services;
using CandidatesManagement.Application.Contracts.Dtos;
using CandidatesManagement.Core.ValueObjects;

namespace CandidatesManagement.UnitTests;

[TestFixture]
public class CandidatesServiceTests
{
    private Mock<ICandidatesRepository> mockRepository;
    private CandidatesService candidatesService;

    [SetUp]
    public void Setup()
    {
        mockRepository = new Mock<ICandidatesRepository>();
        candidatesService = new CandidatesService(mockRepository.Object);
    }

    [Test]
    public async Task UpsertJobCandidateAsync_InvalidCandidateDetails_ArgumentExceptionIsThrown()
    {
        // Arrange
        var dto = new UpsertJobCandidateDto
        {
            EmailAddress = "newcandidate@example.com",
            // Other properties as needed
        };

        // Act  & Assert
        Assert.ThrowsAsync<ArgumentException>(async () => await candidatesService.UpsertJobCandidateAsync(dto));
    }

    [Test]
    public async Task UpsertJobCandidateAsync_CandidateDoesntExist_JobCandidateIsInserted()
    {
        // Arrange
        var builder = new UpsertJobCandidateDtoBuilder()
            .WithFirstName("John")
            .WithLastName("Doe")
            .WithEmailAddress("john.doe@example.com")
            .WithPhoneNumber("123-456-7890")
            .WithIntervalStart(TimeSpan.FromHours(9))
            .WithIntervalEnd(TimeSpan.FromHours(17))
            .WithLinkedInProfile("https://linkedin.com/in/johndoe")
            .WithGitHubProfile("https://github.com/johndoe")
            .WithComment("This is a test comment");

        var dto = builder.Build();

        var candidateEntity = dto.ToEntity();

        // Act
        await candidatesService.UpsertJobCandidateAsync(dto);

        // Assert
        mockRepository.Verify(r => r.UpdateJobCandidateAsync(candidateEntity), Times.Never);
        mockRepository.Verify(r => r.InsertJobCandidateAsync(It.IsAny<JobCandidate>()), Times.Once);
    }

    [Test]
    public async Task UpsertJobCandidateAsync_UpdateExistingCandidate_JobCandidateIsUpdated()
    {
        // Arrange
        var builder = new UpsertJobCandidateDtoBuilder()
            .WithFirstName("John")
            .WithLastName("Doe")
            .WithEmailAddress("john.doe@example.com")
            .WithPhoneNumber("123-456-7890")
            .WithIntervalStart(TimeSpan.FromHours(9))
            .WithIntervalEnd(TimeSpan.FromHours(17))
            .WithLinkedInProfile("https://linkedin.com/in/johndoe")
            .WithGitHubProfile("https://github.com/johndoe")
            .WithComment("This is a test comment");

        var dto = builder.Build();

        var candidateEntity = dto.ToEntity();

        dto.PhoneNumber = "+40123456789";

        mockRepository.Setup(r => r.FindByEmailAddressAsync(candidateEntity.EmailAddress))
                      .ReturnsAsync(candidateEntity);

        // Act
        await candidatesService.UpsertJobCandidateAsync(dto);

        // Assert
        mockRepository.Verify(r => r.InsertJobCandidateAsync(It.IsAny<JobCandidate>()), Times.Never);
        mockRepository.Verify(r => r.UpdateJobCandidateAsync(candidateEntity), Times.Once);
    }
}
