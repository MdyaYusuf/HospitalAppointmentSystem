using HospitalAppointmentSystem.WebApi.Models.Enums;

namespace HospitalAppointmentSystem.WebApi.Dtos.Patients.Responses;

public sealed record PatientResponseDto(string Name, Gender Gender, DateTime DateOfBirth);
