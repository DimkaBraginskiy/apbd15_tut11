using apbd11.Data;
using apbd11.Models;
using Microsoft.EntityFrameworkCore;

namespace apbd11.Repositories;

public class PatientsRepository : IPatientsRepository
{
    private readonly DatabaseContext _context;
    
    public PatientsRepository(DatabaseContext context)
    {
        _context = context;
    }
    
    
    
    public async Task<Patient?> GetPatientByIdAsync(CancellationToken token, int id)
    {
        return await _context.Patients
            .Include(p => p.Prescriptions)
            .ThenInclude(pr => pr.Doctor)
            .Include(p => p.Prescriptions)
            .ThenInclude(pr => pr.PrescriptionMedicaments)
            .ThenInclude(pm => pm.Medicament)
            .Where(p => p.IdPatient == id)
            .FirstOrDefaultAsync(token);

    }
}