using apbd11.DTOs;

namespace apbd11.Services;

public interface IPatientsService
{
    public Task<PatientResponseDto?> GetPatientByIdAsync(CancellationToken token, int id);
}