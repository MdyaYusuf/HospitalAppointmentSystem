using HospitalAppointmentSystem.WebApi.Models.Enums;

namespace HospitalAppointmentSystem.WebApi.Dtos.Patients.Requests;

public sealed record CreatePatientRequest(string Name, Gender Gender, DateTime DateOfBirth, string PhoneNumber, string Email, string Address);
