using HospitalAppointmentSystem.WebApi.Models;

namespace HospitalAppointmentSystem.WebApi.Repository.Abstract;

public interface IDoctorRepository : IRepository<Doctor, int>
{
  Doctor Add(Doctor doctor);
  Doctor? Update(int id, Doctor doctor);
  Doctor? GetByName(string name);
}
