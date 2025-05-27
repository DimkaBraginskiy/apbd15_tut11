using apbd11.DTOs;
using apbd11.Models;

namespace apbd11.Repositories;

public interface IPrescriptionsRepository
{
    public Task<bool> MedicamentExistsAsync(CancellationToken token, int idMedicament);

    public Task<bool> DoctorExistsAsync(CancellationToken token, int idDoctor);
    public Task<Patient> GetOrCreatePatientAsync(CancellationToken token, PatientRequestDto dto);
    public Task<int> CreatePrescriptionAsync(CancellationToken token, Prescription prescription);
}