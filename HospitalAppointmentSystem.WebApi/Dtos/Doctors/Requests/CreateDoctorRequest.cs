using HospitalAppointmentSystem.WebApi.Models.Enums;

namespace HospitalAppointmentSystem.WebApi.Dtos.Doctors.Requests;

public sealed record CreateDoctorRequest(string Name, Gender Gender, Branch Branch, DateTime DateOfBirth, string PhoneNumber, string Email);
