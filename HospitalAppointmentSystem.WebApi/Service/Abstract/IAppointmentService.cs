using HospitalAppointmentSystem.WebApi.Dtos.Appointments.Requests;
using HospitalAppointmentSystem.WebApi.Dtos.Appointments.Responses;
using HospitalAppointmentSystem.WebApi.Models;
using HospitalAppointmentSystem.WebApi.Models.ReturnModels;

namespace HospitalAppointmentSystem.WebApi.Service.Abstract;

public interface IAppointmentService
{
  ReturnModel<List<AppointmentResponseDto>> GetAll();
  ReturnModel<AppointmentResponseDto?> GetById(Guid id);
  ReturnModel<Appointment> Add(CreateAppointmentRequest request);
  ReturnModel<Appointment?> Remove(Guid id);
  void RemovePastAppointments();
}
