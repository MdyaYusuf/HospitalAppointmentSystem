using HospitalAppointmentSystem.WebApi.Contexts;
using HospitalAppointmentSystem.WebApi.Models;
using HospitalAppointmentSystem.WebApi.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace HospitalAppointmentSystem.WebApi.Repository.Concrete;

public class EfDoctorRepository : IDoctorRepository
{
  private MsSqlContext _context;
  public EfDoctorRepository(MsSqlContext context)
  {
    _context = context;
  }

  public Doctor Add(Doctor createdDoctor)
  {
    _context.Doctors.Add(createdDoctor);
    _context.SaveChanges();
    return createdDoctor;
  }

  public Doctor? Remove(int id)
  {
    Doctor? deletedDoctor = GetById(id);
    _context.Doctors.Remove(deletedDoctor);
    _context.SaveChanges();
    return deletedDoctor;
  }

  public List<Doctor> GetAll()
  {
    return _context.Doctors.ToList();
  }

  public Doctor? GetById(int id)
  {
    Doctor? doctor = _context.Doctors.FirstOrDefault(d => d.Id == id);
    return doctor;
  }

  public Doctor? Update(int id, Doctor doctor)
  {
    Doctor? updatedDoctor = GetById(id);

    updatedDoctor.Name = doctor.Name;
    updatedDoctor.Branch = doctor.Branch;
    updatedDoctor.PhoneNumber = doctor.PhoneNumber;
    updatedDoctor.Email = doctor.Email;

    _context.Doctors.Update(updatedDoctor);
    _context.SaveChanges();
    return updatedDoctor;
  }

  public Doctor? GetByName(string name)
  {
    return _context.Doctors.FirstOrDefault(d => d.Name == name);
  }
}
