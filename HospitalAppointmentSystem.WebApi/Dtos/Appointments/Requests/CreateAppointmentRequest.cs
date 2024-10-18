namespace HospitalAppointmentSystem.WebApi.Dtos.Appointments.Requests;

public sealed record CreateAppointmentRequest(string PatientName, DateTime AppointmentDate, string DoctorName);
