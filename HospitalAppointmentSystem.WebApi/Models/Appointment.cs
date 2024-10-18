namespace HospitalAppointmentSystem.WebApi.Models;

public class Appointment : Entity<Guid>
{
  public Appointment()
  {
    CreatedDate = DateTime.Now;  
  }

  public DateTime AppointmentDate { get; set; }
  public DateTime CreatedDate { get; set; }
  public Guid PatientId { get; set; }
  public Patient Patient { get; set; } = null!;
  public int DoctorId { get; set; }
  public Doctor Doctor { get; set; } = null!;
}
