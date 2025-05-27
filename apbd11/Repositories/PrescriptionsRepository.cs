using apbd11.Data;
using apbd11.DTOs;
using apbd11.Models;
using Microsoft.EntityFrameworkCore;

namespace apbd11.Repositories;

public class PrescriptionsRepository : IPrescriptionsRepository
{
    private readonly DatabaseContext _context;

    public PrescriptionsRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<bool> MedicamentExistsAsync(CancellationToken token, int idMedicament)
    {
        return await _context.Medicaments.AnyAsync(m => m.IdMedicament == idMedicament, cancellationToken: token);
    }
    
    public async Task<bool> DoctorExistsAsync(CancellationToken token, int idDoctor)
    {
        return await _context.Doctors.AnyAsync(d => d.IdDoctor == idDoctor, cancellationToken: token);
    }
    
    public async Task<Patient> GetOrCreatePatientAsync(CancellationToken token, PatientRequestDto dto)
    {
        var patient = await _context.Patients.FindAsync(dto.IdPatient, token);
        if (patient != null)
        {
            return patient;
        }


        patient = new Patient
        {
            IdPatient = dto.IdPatient,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            BirthDate = dto.BirthDate
        };
        
        await _context.Patients.AddAsync(patient, token);
        return patient;
    }
    
    public async Task<int> CreatePrescriptionAsync(CancellationToken token, Prescription prescription)
    {
        await _context.Prescriptions.AddAsync(prescription, token);
        await _context.SaveChangesAsync(token);
        return prescription.IdPrescription;
    }
}