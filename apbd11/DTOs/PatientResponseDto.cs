namespace apbd11.DTOs;

public class PatientResponseDto
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    
    public List<PrescriptionResponseDto> Prescriptions { get; set; }
    
}