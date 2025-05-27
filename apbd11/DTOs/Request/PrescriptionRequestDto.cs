namespace apbd11.DTOs;

public class PrescriptionRequestDto
{
    public PatientRequestDto Patient { get; set; }
    public List<MedicamentRequestDto> Medicaments { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public int IdDoctor { get; set; }
}