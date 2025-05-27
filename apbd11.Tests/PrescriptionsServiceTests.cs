using apbd11.DTOs;
using apbd11.Models;
using apbd11.Repositories;
using apbd11.Services;
using Moq;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace apbd11.Tests;

public class PrescriptionsServiceTests
{
    [Fact]
    public async Task CreatePrescriptionAsync_ThrowsException_WhenDueDateBeforeDate()
    {
        var service = new PrescriptionsService(Mock.Of<IPrescriptionsRepository>());

        var dto = new PrescriptionRequestDto
        {
            Date = DateTime.Today,
            DueDate = DateTime.Today.AddDays(-1),
            Medicaments = new List<MedicamentRequestDto>(),
            IdDoctor = 1,
            Patient = new PatientRequestDto()
        };

         Assert.ThrowsAsync<ArgumentException>(() =>
            service.CreatePrescriptionAsync(CancellationToken.None, dto));
        
        
    }

    [Fact]
    public async Task CreatePrescriptionAsync_ThrowsException_WhenTooManyMedicaments()
    {
        // Arrange
        var service = new PrescriptionsService(Mock.Of<IPrescriptionsRepository>());
        var dto = new PrescriptionRequestDto
        {
            Date = DateTime.Today,
            DueDate = DateTime.Today.AddDays(1),
            Medicaments = Enumerable.Range(1, 10)
                .Select(i => new MedicamentRequestDto { IdMedicament = i })
                .ToList(),
            IdDoctor = 1,
            Patient = new PatientRequestDto()
        };

        // Act & Assert
        var exception = await Xunit.Assert.ThrowsAsync<ArgumentException>(() =>
            service.CreatePrescriptionAsync(CancellationToken.None, dto));

        // Проверка сообщения
        Xunit.Assert.Contains("more than 10", exception.Message);
    }

    [Fact]
    public async Task CreatePrescriptionAsync_ReturnsId_WhenValid()
    {
        // Arrange
        var repo = new Mock<IPrescriptionsRepository>();

        repo.Setup(r => r.MedicamentExistsAsync(It.IsAny<CancellationToken>(), It.IsAny<int>())).ReturnsAsync(true);
        repo.Setup(r => r.DoctorExistsAsync(It.IsAny<CancellationToken>(), It.IsAny<int>())).ReturnsAsync(true);
        repo.Setup(r => r.GetOrCreatePatientAsync(It.IsAny<CancellationToken>(), It.IsAny<PatientRequestDto>()))
            .ReturnsAsync(new Patient { IdPatient = 1 });
        repo.Setup(r => r.CreatePrescriptionAsync(It.IsAny<CancellationToken>(), It.IsAny<Prescription>()))
            .ReturnsAsync(123);

        var service = new PrescriptionsService(repo.Object);

        var dto = new PrescriptionRequestDto
        {
            Date = DateTime.Today,
            DueDate = DateTime.Today.AddDays(1),
            IdDoctor = 1,
            Patient = new PatientRequestDto(),
            Medicaments = new List<MedicamentRequestDto>
            {
                new MedicamentRequestDto { IdMedicament = 1 }
            }
        };

        // Act
        var result = await service.CreatePrescriptionAsync(CancellationToken.None, dto);

        // Assert
        Assert.AreEqual(123, result);
    }
}