using HospitalAppointmentSystem.WebApi.Dtos.Doctors.Requests;
using HospitalAppointmentSystem.WebApi.Dtos.Doctors.Responses;
using HospitalAppointmentSystem.WebApi.Models;
using HospitalAppointmentSystem.WebApi.Models.ReturnModels;

namespace HospitalAppointmentSystem.WebApi.Service.Abstract;

public interface IDoctorService
{
  ReturnModel<List<DoctorResponseDto>> GetAll();
  ReturnModel<DoctorResponseDto?> GetById(int id);
  ReturnModel<Doctor> Add(CreateDoctorRequest request);
  ReturnModel<Doctor?> Remove(int id);
  ReturnModel<Doctor?> Update(int id, UpdateDoctorRequest request);
}
