using HospitalAppointmentSystem.WebApi.Dtos.Appointments.Requests;
using HospitalAppointmentSystem.WebApi.Dtos.Appointments.Responses;
using HospitalAppointmentSystem.WebApi.Exceptions;
using HospitalAppointmentSystem.WebApi.Models;
using HospitalAppointmentSystem.WebApi.Models.ReturnModels;
using HospitalAppointmentSystem.WebApi.Repository.Abstract;
using HospitalAppointmentSystem.WebApi.Repository.Concrete;
using HospitalAppointmentSystem.WebApi.Service.Abstract;
using HospitalAppointmentSystem.WebApi.Service.Mappers;
using Microsoft.EntityFrameworkCore;

namespace HospitalAppointmentSystem.WebApi.Service.Concrete;

public class AppointmentService : IAppointmentService
{
  private IAppointmentRepository _appointmentRepository;
  private IPatientRepository _patientRepository;
  private IDoctorRepository _doctorRepository;
  private AppointmentMapper _appointmentMapper;
  public AppointmentService(IAppointmentRepository appointmentRepository, IPatientRepository patientRepository, IDoctorRepository doctorRepository, AppointmentMapper appointmentMapper)
  {
    _appointmentRepository = appointmentRepository;
    _patientRepository = patientRepository;
    _doctorRepository = doctorRepository;
    _appointmentMapper = appointmentMapper;
  }

  public ReturnModel<Appointment> Add(CreateAppointmentRequest request)
  {
    try
    {
      CheckNames(request.PatientName, request.DoctorName);

      var CreatedDate = DateTime.Now;
      CheckDayDifference(request.AppointmentDate, CreatedDate);

      var patient = _patientRepository.GetByName(request.PatientName);

      if (patient == null)
      {
        throw new NotFoundException("Bu isimde bir hasta kaydı bulunmamaktadır.");
      }

      var doctor = _doctorRepository.GetByName(request.DoctorName);

      if (doctor == null)
      {
        throw new NotFoundException("Bu isimde bir doktor kaydı bulunmamaktadır.");
      }

      Appointment appointment = _appointmentMapper.ConvertToEntity(request, patient.Id, doctor.Id);

      var appointmentCount = _appointmentRepository.GetDoctorAppointmentCount(doctor.Id);
      CheckAppointmentsCount(appointmentCount);

      Appointment createdAppointment = _appointmentRepository.Add(appointment, doctor, patient);

      return new ReturnModel<Appointment>()
      {
        Data = createdAppointment,
        Message = "Randevu başarılı bir şekilde oluşturuldu.",
        Success = true,
        StatusCode = System.Net.HttpStatusCode.OK
      };
    }
    catch (NotFoundException ex)
    {
      return ReturnModelOfException<Appointment>(ex);
    }
    catch (ValidationException ex)
    {
      return ReturnModelOfException<Appointment>(ex);
    }
    catch (Exception ex)
    {
      return ReturnModelOfException<Appointment>(ex);
    }
  }

  public ReturnModel<List<AppointmentResponseDto>> GetAll()
  {
    try
    {
      List<Appointment> appointments = _appointmentRepository.GetAll();
      List<AppointmentResponseDto> responseList = _appointmentMapper.ConvertToResponseList(appointments);

      return new ReturnModel<List<AppointmentResponseDto>>()
      {
        Data = responseList,
        Message = "Randevu listesi başarılı bir şekilde alındı.",
        Success = true,
        StatusCode = System.Net.HttpStatusCode.OK
      };
    }
    catch (NotFoundException ex)
    {
      return ReturnModelOfException<List<AppointmentResponseDto>>(ex);
    }
    catch (ValidationException ex)
    {
      return ReturnModelOfException<List<AppointmentResponseDto>>(ex);
    }
    catch (Exception ex)
    {
      return ReturnModelOfException<List<AppointmentResponseDto>>(ex);
    }
  }

  public ReturnModel<AppointmentResponseDto?> GetById(Guid id)
  {
    try
    {
      Appointment? appointment = _appointmentRepository.GetById(id);

      if (appointment != null)
      {
        AppointmentResponseDto? response = _appointmentMapper.ConvertToResponse(appointment);
        return new ReturnModel<AppointmentResponseDto?>()
        {
          Data = response,
          Message = $"Aradığınız {id} numaralı randevu başarılı bir şekilde bulundu.",
          Success = true,
          StatusCode = System.Net.HttpStatusCode.OK
        };
      }
      else
      {
        return new ReturnModel<AppointmentResponseDto?>()
        {
          Data = null,
          Message = $"Aradığınız {id} numarasına sahip randevu mevcut değildir.",
          Success = false,
          StatusCode = System.Net.HttpStatusCode.NotFound
        };
      }
    }
    catch (ValidationException ex)
    {
      return ReturnModelOfException<AppointmentResponseDto?>(ex);
    }
    catch (Exception ex)
    {
      return ReturnModelOfException<AppointmentResponseDto?>(ex);
    }
  }

  public ReturnModel<Appointment?> Remove(Guid id)
  {
    try
    {
      Appointment? deletedAppointment = _appointmentRepository.Remove(id);
      
      if (deletedAppointment != null)
      {
        return new ReturnModel<Appointment?>()
        {
          Data = deletedAppointment,
          Message = $"{id} numaralı randevu başarılı bir şekilde silindi.",
          Success = true,
          StatusCode = System.Net.HttpStatusCode.OK
        };
      }
      else
      {
        return new ReturnModel<Appointment?>()
        {
          Data = null,
          Message = $"{id} numaralı randevu silinemedi çünkü zaten yok.",
          Success = false,
          StatusCode = System.Net.HttpStatusCode.NotFound
        };
      }
    }
    catch (ValidationException ex)
    {
      return ReturnModelOfException<Appointment?>(ex);
    }
    catch (Exception ex)
    {
      return ReturnModelOfException<Appointment?>(ex);
    }
  }

  public void RemovePastAppointments()
  {
    _appointmentRepository.RemovePastAppointments();
  }

  private ReturnModel<T> ReturnModelOfException<T>(Exception ex)
  {
    if (ex.GetType() == typeof(NotFoundException))
    {
      return new ReturnModel<T>()
      {
        Data = default(T),
        Message = ex.Message,
        Success = false,
        StatusCode = System.Net.HttpStatusCode.NotFound
      };
    }
    if (ex.GetType() == typeof(ValidationException))
    {
      return new ReturnModel<T>()
      {
        Data = default(T),
        Message = ex.Message,
        Success = false,
        StatusCode = System.Net.HttpStatusCode.BadRequest
      };
    }
    return new ReturnModel<T>()
    {
      Data = default(T),
      Message = ex.Message,
      Success = false,
      StatusCode = System.Net.HttpStatusCode.InternalServerError
    };
  }

  private void CheckDayDifference(DateTime AppointmentDate, DateTime CreatedDate)
  {
    var dayDifference = (AppointmentDate - CreatedDate).Days;

    if (dayDifference < 3)
    {
      throw new ValidationException("Randevu minimum 3 gün sonrası için alınabilir.");
    }
  }

  private void CheckNames(string patientName, string doctorName)
  {
    if (string.IsNullOrWhiteSpace(patientName) || string.IsNullOrWhiteSpace(doctorName))
    {
      throw new ValidationException("Hasta ve doktor isimlerini mutlaka girmelisiniz.");
    }
  }

  private void CheckAppointmentsCount(int appointmentCount)
  {
    if (appointmentCount > 10)
    {
      throw new ValidationException("Bir doktor maksimum 10 randevu alabilir.");
    }
  }
}
