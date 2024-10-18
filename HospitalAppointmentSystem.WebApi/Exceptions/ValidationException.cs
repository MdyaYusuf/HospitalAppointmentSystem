namespace HospitalAppointmentSystem.WebApi.Exceptions;

public class ValidationException : Exception
{
  public ValidationException(string msg) : base(msg)
  {
        
  }
}
