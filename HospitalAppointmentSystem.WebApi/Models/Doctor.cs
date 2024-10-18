using HospitalAppointmentSystem.WebApi.Models.Enums;

namespace HospitalAppointmentSystem.WebApi.Models;

public class Doctor : Entity<int>
{
  public string Name { get; set; } = string.Empty;
  public Gender Gender { get; set; }
  public Branch Branch { get; set; }
  public DateTime DateOfBirth { get; set; }
  public string PhoneNumber { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public List<Appointment>? Appointments { get; set; }
}
