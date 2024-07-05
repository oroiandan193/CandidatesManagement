using NUnit.Framework;
using Moq;
using System.Threading.Tasks;
using CandidatesManagement.Core.Entities;
using CandidatesManagement.Core.Repositories;
using CandidatesManagement.Application.Services;
using CandidatesManagement.Application.Contracts.Dtos;

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

        mockRepository.Setup(r => r.FindByEmailAddressAsync(dto.EmailAddress))
                      .ReturnsAsync((JobCandidate)null);

        // Act  & Assert
        Assert.ThrowsAsync<ArgumentException>(async () => await candidatesService.UpsertJobCandidateAsync(dto));
    }

    [Test]
    public async Task UpsertJobCandidateAsync_UpdateExistingCandidate_CandidateIsUpdated()
    {
        // Arrange
        var candidateBuilder = new JobCandidateBuilder();

        var existingCandidate = candidateBuilder.Build();

        var dto = candidateBuilder.BuildDto();

        dto.PhoneNumber = "+40123456789";

        mockRepository.Setup(r => r.FindByEmailAddressAsync(dto.EmailAddress))
                      .ReturnsAsync(existingCandidate);

        // Act
        await candidatesService.UpsertJobCandidateAsync(dto);

        // Assert
        mockRepository.Verify(r => r.InsertJobCandidateAsync(It.IsAny<JobCandidate>()), Times.Never);
        mockRepository.Verify(r => r.UpdateJobCandidateAsync(existingCandidate), Times.Once);
    }
}
