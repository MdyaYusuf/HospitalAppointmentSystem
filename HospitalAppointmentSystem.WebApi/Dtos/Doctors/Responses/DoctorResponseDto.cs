using HospitalAppointmentSystem.WebApi.Models.Enums;

namespace HospitalAppointmentSystem.WebApi.Dtos.Doctors.Responses;

public sealed record DoctorResponseDto(string Name, Gender Gender, Branch Branch, DateTime DateOfBirth);
