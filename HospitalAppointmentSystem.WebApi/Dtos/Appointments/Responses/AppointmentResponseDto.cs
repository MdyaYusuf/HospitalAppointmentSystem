namespace HospitalAppointmentSystem.WebApi.Dtos.Appointments.Responses;

public sealed record AppointmentResponseDto(string PatientName, DateTime AppointmentDate, string DoctorName);
