using apbd11.DTOs;
using apbd11.Models;
using apbd11.Repositories;

namespace apbd11.Services;

public class PrescriptionsService : IPrescriptionsService
{
    private readonly IPrescriptionsRepository _prescriptionsRepository;
    
    public PrescriptionsService(IPrescriptionsRepository prescriptionsRepository)
    {
        _prescriptionsRepository = prescriptionsRepository;
    }
    
    public async Task<int> CreatePrescriptionAsync(CancellationToken token, PrescriptionRequestDto dto)
    {
        if(dto.DueDate < dto.Date)
        {
            throw new ArgumentException("Due date cannot be earlier than the prescription date.");
        }

        if (dto.Medicaments.Count > 10)
        {
            throw new ArgumentException("A prescription cannot contain more than 10 medicaments.");
        }
        
        foreach(var medicament in dto.Medicaments)
        {
           if(!await _prescriptionsRepository.MedicamentExistsAsync(token, medicament.IdMedicament))
           {
               throw new ArgumentException($"Medicament with id {medicament.IdMedicament} does not exist.");
           }
        }
        
        if(! await _prescriptionsRepository.DoctorExistsAsync(token, dto.IdDoctor))
        {
            throw new ArgumentException($"Doctor with id {dto.IdDoctor} does not exist.");
        }

        var patient = await _prescriptionsRepository.GetOrCreatePatientAsync(token, dto.Patient);

        var prescription = new Prescription
        {
            Date = dto.Date,
            DueDate = dto.DueDate,
            IdPatient = patient.IdPatient,
            IdDoctor = dto.IdDoctor,
            PrescriptionMedicaments = dto.Medicaments.Select(m => new Prescription_Medicament
            {
                IdMedicament = m.IdMedicament,
                Dose = m.Dose,
                Description = m.Description
            }).ToList()
        };
        
        var prescriptionId = await _prescriptionsRepository.CreatePrescriptionAsync(token, prescription);
        
        return prescriptionId;

    }
}