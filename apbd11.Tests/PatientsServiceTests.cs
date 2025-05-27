using apbd11.Models;
using Moq;
using Xunit;
using Assert = NUnit.Framework.Assert;
using apbd11.Repositories;
using apbd11.Services;

namespace apbd11.Tests;

public class PatientsServiceTests
{
    [Fact]
    public async Task GetPatientByIdAsync_ReturnsPatientResponse_WhenPatientExists()
    {
        // Arrange
        var patientId = 1;
        var mockRepo = new Mock<IPatientsRepository>();
        mockRepo.Setup(r => r.GetPatientByIdAsync(It.IsAny<CancellationToken>(), patientId))
            .ReturnsAsync(new Patient
            {
                IdPatient = patientId,
                FirstName = "John",
                LastName = "Doe",
                BirthDate = new DateTime(1990, 1, 1),
                Prescriptions = new List<Prescription>()
            });

        var service = new PatientsService(mockRepo.Object);

        // Act
        var result = await service.GetPatientByIdAsync(CancellationToken.None, patientId);

        // Assert
        Assert.NotNull(result);
        Assert.AreEqual(patientId, result.IdPatient);
        Assert.AreEqual("John", result.FirstName);
        Assert.AreEqual("Doe", result.LastName);
    }

    [Fact]
    public async Task GetPatientByIdAsync_ReturnsNull_WhenPatientDoesNotExist()
    {
        // Arrange
        var mockRepo = new Mock<IPatientsRepository>();
        mockRepo.Setup(r => r.GetPatientByIdAsync(It.IsAny<CancellationToken>(), It.IsAny<int>()))
            .ReturnsAsync((Patient?)null);

        var service = new PatientsService(mockRepo.Object);

        // Act
        var result = await service.GetPatientByIdAsync(CancellationToken.None, 999);

        // Assert
        Assert.Null(result);
    }
}