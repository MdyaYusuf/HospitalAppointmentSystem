using HospitalAppointmentSystem.WebApi.Models.Enums;

namespace HospitalAppointmentSystem.WebApi.Dtos.Doctors.Requests;

public sealed record UpdateDoctorRequest(string Name, Branch Branch, string PhoneNumber, string Email);
