using HospitalAppointmentSystem.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalAppointmentSystem.WebApi.Contexts;

public class MsSqlContext : DbContext
{
  public MsSqlContext(DbContextOptions opt) : base(opt)
  {
        
  }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseSqlServer("Server = localhost, 1433; Database = HospitalAppointmentSystem_db; User = sa; Password = 123456+-+; TrustServerCertificate = true");
  }

  public DbSet<Doctor> Doctors { get; set; }
  public DbSet<Patient> Patients { get; set; }
  public DbSet<Appointment> Appointments { get; set; }
}
