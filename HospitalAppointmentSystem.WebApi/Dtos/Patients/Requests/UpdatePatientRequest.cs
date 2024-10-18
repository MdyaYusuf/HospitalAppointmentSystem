namespace HospitalAppointmentSystem.WebApi.Dtos.Patients.Requests;

public record UpdatePatientRequest(string Name, string PhoneNumber, string Email, string Address);
