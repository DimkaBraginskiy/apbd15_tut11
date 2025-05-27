using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace apbd11.Models;

[PrimaryKey(nameof(Medicament), nameof(Prescription))]
[Table("Prescription_Medicament")]
public class Prescription_Medicament
{
    [ForeignKey(nameof(Medicament))]
    public int IdMedicament { get; set; }
    public Medicament Medicament { get; set; }
    
    [ForeignKey(nameof(Prescription))]
    public int IdPrescription { get; set; }
    public Prescription Prescription { get; set; }
    
    public int Dose { get; set; }
    public string Description { get; set; }
}