using HospitalAppointmentSystem.WebApi.Models;

namespace HospitalAppointmentSystem.WebApi.Repository.Abstract;

public interface IPatientRepository : IRepository<Patient, Guid>
{
  Patient Add(Patient patient);
  Patient? Update(Guid id, Patient patient);
  Patient? GetByName(string name);
}
