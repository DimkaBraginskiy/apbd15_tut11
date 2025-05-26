using apbd11.Models;
using Microsoft.EntityFrameworkCore;

namespace apbd11.Data;

public class DatabaseContext :DbContext
{
    
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Prescription_Medicament> PrescriptionMedicaments { get; set; }

    protected DatabaseContext()
    {
    }
    
    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doctor>(d =>
        {
            d.ToTable("Doctor");
            d.HasKey(e => e.IdDoctor);
            d.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
            d.Property(e => e.LastName).HasMaxLength(100).IsRequired();
            d.Property(e => e.Email).HasMaxLength(100).IsRequired();
        });

        modelBuilder.Entity<Patient>(p =>
        {
            p.ToTable("Patient");
            p.HasKey(e => e.IdPatient);
            p.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
            p.Property(e => e.LastName).HasMaxLength(100).IsRequired();
            p.Property(e => e.BirthDate).HasColumnType("date");
        });

        modelBuilder.Entity<Medicament>(m =>
        {
            m.ToTable("Medicament");
            m.HasKey(e => e.IdMedicament);
            m.Property(e => e.Name).HasMaxLength(100).IsRequired();
            m.Property(e => e.Description).HasMaxLength(100).IsRequired();
            m.Property(e => e.Type).HasMaxLength(100).IsRequired();
        });

        modelBuilder.Entity<Prescription>(p =>
        {
            p.ToTable("Prescription");
            p.HasKey(e => e.IdPrescription);
            p.Property(e => e.Date).HasColumnType("date");
            p.Property(e => e.DueDate).HasColumnType("date");

            p.HasOne(p => p.Doctor)
                .WithMany(d => d.Prescriptions)
                .HasForeignKey(p => p.IdDoctor);

            p.HasOne(p => p.Patient)
                .WithMany(pat => pat.Prescriptions)
                .HasForeignKey(p => p.IdDoctor);
        });

        modelBuilder.Entity<Prescription_Medicament>(pm =>
        {
            pm.ToTable("Prescription_Medicament");
            pm.HasKey(e => new {e.IdMedicament, e.IdPrescription});
            pm.Property(e => e.Dose);
            pm.Property(e => e.Details).HasMaxLength(500);

            pm.HasOne(pm => pm.Medicament)
                .WithMany(m => m.PrescriptionMedicaments)
                .HasForeignKey(pm => pm.IdMedicament);

            pm.HasOne(pm => pm.Prescription)
                .WithMany(p => p.PrescriptionMedicaments)
                .HasForeignKey(pm => pm.IdPrescription);
        });
    }
}