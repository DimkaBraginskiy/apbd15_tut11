using apbd11.DTOs;

namespace apbd11.Services;

public interface IPrescriptionsService
{
    public Task<int> CreatePrescriptionAsync(CancellationToken token, PrescriptionRequestDto dto);
}