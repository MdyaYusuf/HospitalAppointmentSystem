using HospitalAppointmentSystem.WebApi.Dtos.Doctors.Requests;
using HospitalAppointmentSystem.WebApi.Dtos.Doctors.Responses;
using HospitalAppointmentSystem.WebApi.Models;

namespace HospitalAppointmentSystem.WebApi.Service.Mappers;

public class DoctorMapper
{
  public Doctor ConvertToEntity(CreateDoctorRequest request)
  {
    return new Doctor()
    {
      Name = request.Name,
      Gender = request.Gender,
      Branch = request.Branch,
      DateOfBirth = request.DateOfBirth,
      PhoneNumber = request.PhoneNumber,
      Email = request.Email
    };
  }

  public Doctor ConverToEntity(UpdateDoctorRequest request)
  {
    return new Doctor()
    {
      Name = request.Name,
      Branch = request.Branch,
      PhoneNumber = request.PhoneNumber,
      Email = request.Email
    };
  }

  public DoctorResponseDto ConvertToResponse(Doctor doctor)
  {
    return new DoctorResponseDto(Name: doctor.Name, Gender: doctor.Gender, Branch: doctor.Branch, DateOfBirth: doctor.DateOfBirth);
  }

  public List<DoctorResponseDto> ConvertToResponseList(List<Doctor> doctors)
  {
    return doctors.Select(d => ConvertToResponse(d)).ToList();
  }
}
