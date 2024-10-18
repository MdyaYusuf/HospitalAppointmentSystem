using HospitalAppointmentSystem.WebApi.Contexts;
using HospitalAppointmentSystem.WebApi.Models;
using HospitalAppointmentSystem.WebApi.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace HospitalAppointmentSystem.WebApi.Repository.Concrete;

public class EfPatientRepository : IPatientRepository
{
  private MsSqlContext _context;
  public EfPatientRepository(MsSqlContext context)
  {
    _context = context;
  }

  public Patient Add(Patient createdPatient)
  {
    _context.Patients.Add(createdPatient);
    _context.SaveChanges();
    return createdPatient;
  }

  public Patient? Remove(Guid id)
  {
    Patient? deletedPatient = GetById(id);
    _context.Patients.Remove(deletedPatient);
    _context.SaveChanges();
    return deletedPatient;
  }

  public List<Patient> GetAll()
  {
    return _context.Patients.ToList();
  }

  public Patient? GetById(Guid id)
  {
    Patient? patient = _context.Patients.FirstOrDefault(p => p.Id == id);
    return patient;
  }

  public Patient? Update(Guid id, Patient patient)
  {
    Patient? updatedPatient = GetById(id);

    updatedPatient.Name = patient.Name;
    updatedPatient.PhoneNumber = patient.PhoneNumber;
    updatedPatient.Email = patient.Email;
    updatedPatient.Address = patient.Address;

    _context.Patients.Update(updatedPatient);
    _context.SaveChanges();
    return updatedPatient;
  }

  public Patient? GetByName(string name)
  {
    return _context.Patients.FirstOrDefault(p => p.Name == name);
  }
}
