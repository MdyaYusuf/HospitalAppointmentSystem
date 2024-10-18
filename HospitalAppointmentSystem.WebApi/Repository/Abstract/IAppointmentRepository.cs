using HospitalAppointmentSystem.WebApi.Models;

namespace HospitalAppointmentSystem.WebApi.Repository.Abstract;

public interface IAppointmentRepository : IRepository<Appointment, Guid>
{
  Appointment Add(Appointment appointment, Doctor doctor, Patient patient);
  int GetDoctorAppointmentCount(int doctorId);
  void RemovePastAppointments();
}
