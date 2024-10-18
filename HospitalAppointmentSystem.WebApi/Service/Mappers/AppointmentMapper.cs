using HospitalAppointmentSystem.WebApi.Dtos.Appointments.Requests;
using HospitalAppointmentSystem.WebApi.Dtos.Appointments.Responses;
using HospitalAppointmentSystem.WebApi.Models;

namespace HospitalAppointmentSystem.WebApi.Service.Mappers;

public class AppointmentMapper
{
  public Appointment ConvertToEntity(CreateAppointmentRequest request, Guid patientId, int doctorId)
  {
    return new Appointment()
    {
      AppointmentDate = request.AppointmentDate,
      PatientId = patientId,
      DoctorId = doctorId
    };
  }

  public AppointmentResponseDto ConvertToResponse(Appointment appointment)
  {
    return new AppointmentResponseDto(PatientName: appointment.Patient.Name, AppointmentDate: appointment.AppointmentDate, DoctorName: appointment.Doctor.Name);
  }

  public List<AppointmentResponseDto> ConvertToResponseList(List<Appointment> appointments)
  {
    return appointments.Select(a =>  ConvertToResponse(a)).ToList();
  }
}
