using HospitalAppointmentSystem.WebApi.Dtos.Patients.Requests;
using HospitalAppointmentSystem.WebApi.Dtos.Patients.Responses;
using HospitalAppointmentSystem.WebApi.Models;

namespace HospitalAppointmentSystem.WebApi.Service.Mappers;

public class PatientMapper
{
  public Patient ConvertToEntity(CreatePatientRequest request)
  {
    return new Patient()
    {
      Name = request.Name,
      Gender = request.Gender,
      DateOfBirth = request.DateOfBirth,
      PhoneNumber = request.PhoneNumber,
      Email = request.Email,
      Address = request.Address
    };
  }

  public Patient ConvertToEntity(UpdatePatientRequest request)
  {
    return new Patient()
    {
      Name = request.Name,
      PhoneNumber = request.PhoneNumber,
      Email = request.Email,
      Address = request.Address
    };
  }

  public PatientResponseDto ConvertToResponse(Patient patient)
  {
    return new PatientResponseDto(Name: patient.Name, Gender: patient.Gender, DateOfBirth: patient.DateOfBirth);
  }

  public List<PatientResponseDto> ConverToResponseList(List<Patient> patients)
  {
    return patients.Select(p => ConvertToResponse(p)).ToList();
  }
}
