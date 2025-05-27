using apbd11.DTOs;
using apbd11.Repositories;

namespace apbd11.Services;

public class PatientsService : IPatientsService
{
    private readonly IPatientsRepository _patientsRepository;
    
    public PatientsService(IPatientsRepository patientsRepository)
    {
        _patientsRepository = patientsRepository;
    }
    
    
    public async Task<PatientResponseDto?> GetPatientByIdAsync(CancellationToken token, int id)
    {
        var patient = await _patientsRepository.GetPatientByIdAsync(token, id);

        if (patient == null)
        {
            return null;
        }

        return new PatientResponseDto
        {
            IdPatient = patient.IdPatient,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            BirthDate = patient.BirthDate,
            Prescriptions = patient.Prescriptions
                .OrderBy(p => p.DueDate)
                .Select(p => new PrescriptionResponseDto
                {
                    IdPrescription = p.IdPrescription,
                    Date = p.Date,
                    DueDate = p.DueDate,
                    Medicaments = p.PrescriptionMedicaments.Select(pm => new MedicamentResponseDto
                    {
                        IdMedicament = pm.Medicament.IdMedicament,
                        Name = pm.Medicament.Name,
                        Dose = pm.Dose,
                        Description = pm.Description
                    }).ToList(),
                    Doctor = new DoctorResponseDto
                    {
                        IdDoctor = p.Doctor.IdDoctor,
                        FirstName = p.Doctor.FirstName
                    }
                }).ToList()
        };
    }

}