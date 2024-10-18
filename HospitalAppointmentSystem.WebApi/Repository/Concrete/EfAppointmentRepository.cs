using HospitalAppointmentSystem.WebApi.Contexts;
using HospitalAppointmentSystem.WebApi.Models;
using HospitalAppointmentSystem.WebApi.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace HospitalAppointmentSystem.WebApi.Repository.Concrete;

public class EfAppointmentRepository : IAppointmentRepository
{
  private MsSqlContext _context;
  public EfAppointmentRepository(MsSqlContext context)
  {
    _context = context;
  }

  public Appointment Add(Appointment createdAppointment, Doctor doctor, Patient patient)
  {
    _context.Appointments.Add(createdAppointment);
    _context.SaveChanges();
    return createdAppointment;
  }

  public Appointment? Remove(Guid id)
  {
    Appointment? appointment = GetById(id);
    _context.Appointments.Remove(appointment);
    _context.SaveChanges();
    return appointment;
  }

  public List<Appointment> GetAll()
  {
    return _context.Appointments.Include(a => a.Patient).Include(a => a.Doctor).ToList();
  }

  public Appointment? GetById(Guid id)
  {
    Appointment? appointment = _context.Appointments.Include(a => a.Patient).Include(a => a.Doctor).FirstOrDefault(a => a.Id == id);
    return appointment;
  }

  public int GetDoctorAppointmentCount(int doctorId)
  {
    return _context.Appointments.Count(a => a.DoctorId == doctorId);
  }

  public void RemovePastAppointments()
  {
    var pastAppointments = _context.Appointments.Where(a => a.AppointmentDate < DateTime.Now).ToList();
    _context.Appointments.RemoveRange(pastAppointments);
    _context.SaveChanges();
  }
}
