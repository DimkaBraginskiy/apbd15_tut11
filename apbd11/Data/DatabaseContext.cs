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

        modelBuilder.Entity<Doctor>().HasData(new List<Doctor>()
        {
            new Doctor() { IdDoctor = 1, FirstName = "John", LastName = "Smith", Email = "Jsmth@gmail.com" },
            new Doctor() { IdDoctor = 2, FirstName = "Walter", LastName = "White", Email = "Heisenberg@gmail.com" }
        });
        
        modelBuilder.Entity<Patient>().HasData(new List<Patient>()
        {
            new Patient() { IdPatient = 1, FirstName = "Skyler", LastName = "White", BirthDate = new DateTime(1990, 1, 1) },
            new Patient() { IdPatient = 2, FirstName = "John", LastName = "Doe", BirthDate = new DateTime(1985, 5, 15) }
        });
        
        modelBuilder.Entity<Medicament>().HasData(new List<Medicament>()
        {
            new Medicament() { IdMedicament = 1, Name = "Aspirin", Description = "Pain reliever", Type = "Analgesic" },
            new Medicament() { IdMedicament = 2, Name = "Ibuprofen", Description = "Anti-inflammatory", Type = "NSAID" }
        });
        
        modelBuilder.Entity<Prescription>().HasData(new List<Prescription>()
        {
            new Prescription() { IdPrescription = 1, Date = new DateTime(2023, 10, 1), DueDate = new DateTime(2023, 10, 15), IdPatient = 1, IdDoctor = 1 },
            new Prescription() { IdPrescription = 2, Date = new DateTime(2022, 10, 5), DueDate = new DateTime(2024, 10, 20), IdPatient = 2, IdDoctor = 2 }
        });
        
        modelBuilder.Entity<Prescription_Medicament>().HasData(new List<Prescription_Medicament>()
        {
            new Prescription_Medicament() { IdMedicament = 1, IdPrescription = 1, Dose = 500, Details = "Take twice a day" },
            new Prescription_Medicament() { IdMedicament = 2, IdPrescription = 2, Dose = 200, Details = "Take once a day" }
        });
    }
}