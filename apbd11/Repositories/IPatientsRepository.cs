using apbd11.Models;

namespace apbd11.Repositories;

public interface IPatientsRepository
{
    public Task<Patient?> GetPatientByIdAsync(CancellationToken token, int id);
}