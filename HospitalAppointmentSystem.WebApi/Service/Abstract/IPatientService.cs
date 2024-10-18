using HospitalAppointmentSystem.WebApi.Dtos.Patients.Requests;
using HospitalAppointmentSystem.WebApi.Dtos.Patients.Responses;
using HospitalAppointmentSystem.WebApi.Models;
using HospitalAppointmentSystem.WebApi.Models.ReturnModels;

namespace HospitalAppointmentSystem.WebApi.Service.Abstract;

public interface IPatientService
{
  ReturnModel<List<PatientResponseDto>> GetAll();
  ReturnModel<PatientResponseDto?> GetById(Guid id);
  ReturnModel<Patient> Add(CreatePatientRequest request);
  ReturnModel<Patient?> Remove(Guid id);
  ReturnModel<Patient?> Update(Guid id, UpdatePatientRequest request);
}
