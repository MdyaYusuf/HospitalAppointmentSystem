using HospitalAppointmentSystem.WebApi.Dtos.Doctors.Responses;
using HospitalAppointmentSystem.WebApi.Dtos.Patients.Requests;
using HospitalAppointmentSystem.WebApi.Dtos.Patients.Responses;
using HospitalAppointmentSystem.WebApi.Exceptions;
using HospitalAppointmentSystem.WebApi.Models;
using HospitalAppointmentSystem.WebApi.Models.ReturnModels;
using HospitalAppointmentSystem.WebApi.Repository.Abstract;
using HospitalAppointmentSystem.WebApi.Service.Abstract;
using HospitalAppointmentSystem.WebApi.Service.Mappers;

namespace HospitalAppointmentSystem.WebApi.Service.Concrete;

public class PatientService : IPatientService
{
  private IPatientRepository _patientRepository;
  private PatientMapper _patientMapper;
  public PatientService(IPatientRepository patientRepository, PatientMapper patientMapper)
  {
    _patientRepository = patientRepository;
    _patientMapper = patientMapper;
  }

  public ReturnModel<Patient> Add(CreatePatientRequest request)
  {
    try
    {
      Patient patient = _patientMapper.ConvertToEntity(request);
      Patient createdPatient = _patientRepository.Add(patient);

      return new ReturnModel<Patient>()
      {
        Data = createdPatient,
        Message = "Hasta başarılı bir şekilde eklendi.",
        Success = true,
        StatusCode = System.Net.HttpStatusCode.OK
      };
    }
    catch (NotFoundException ex)
    {
      return ReturnModelOfException<Patient>(ex);
    }
    catch (ValidationException ex)
    {
      return ReturnModelOfException<Patient>(ex);
    }
    catch (Exception ex)
    {
      return ReturnModelOfException<Patient>(ex);
    }
  }

  public ReturnModel<List<PatientResponseDto>> GetAll()
  {
    try
    {
      List<Patient> patients = _patientRepository.GetAll();
      List<PatientResponseDto> responseList = _patientMapper.ConverToResponseList(patients);

      return new ReturnModel<List<PatientResponseDto>>()
      {
        Data = responseList,
        Message = "Hasta listesi başarılı bir şekilde alındı.",
        Success = true,
        StatusCode = System.Net.HttpStatusCode.OK
      };
    }
    catch (NotFoundException ex)
    {
      return ReturnModelOfException<List<PatientResponseDto>>(ex);
    }
    catch (ValidationException ex)
    {
      return ReturnModelOfException<List<PatientResponseDto>>(ex);
    }
    catch (Exception ex)
    {
      return ReturnModelOfException<List<PatientResponseDto>>(ex);
    }
  }

  public ReturnModel<PatientResponseDto?> GetById(Guid id)
  {
    try
    {
      Patient? patient = _patientRepository.GetById(id);

      if (patient != null)
      {
        PatientResponseDto? response = _patientMapper.ConvertToResponse(patient);
        return new ReturnModel<PatientResponseDto?>()
        {
          Data = response,
          Message = $"Aradığınız {id} numaralı hasta başarılı bir şekilde bulundu.",
          Success = true,
          StatusCode = System.Net.HttpStatusCode.OK
        };
      }
      else
      {
        return new ReturnModel<PatientResponseDto?>()
        {
          Data = null,
          Message = $"Aradığınız {id} numarasına sahip bir hasta hastanemizde mevcut değildir.",
          Success = false,
          StatusCode = System.Net.HttpStatusCode.NotFound
        };
      }
    }
    catch (ValidationException ex)
    {
      return ReturnModelOfException<PatientResponseDto?>(ex);
    }
    catch (Exception ex)
    {
      return ReturnModelOfException<PatientResponseDto?>(ex);
    }
  }

  public ReturnModel<Patient?> Remove(Guid id)
  {
    try
    {
      Patient? deletedPatient = _patientRepository.Remove(id);

      if (deletedPatient != null)
      {
        return new ReturnModel<Patient?>()
        {
          Data = deletedPatient,
          Message = $"{id} numaralı hasta başarılı bir şekilde silindi.",
          Success = true,
          StatusCode = System.Net.HttpStatusCode.OK
        };
      }
      else
      {
        return new ReturnModel<Patient?>()
        {
          Data = null,
          Message = $"{id} numaralı hasta silinemedi çünkü zaten yok.",
          Success = false,
          StatusCode = System.Net.HttpStatusCode.NotFound
        };
      }
    }
    catch (ValidationException ex)
    {
      return ReturnModelOfException<Patient?>(ex);
    }
    catch (Exception ex)
    {
      return ReturnModelOfException<Patient?>(ex);
    }
  }

  public ReturnModel<Patient?> Update(Guid id, UpdatePatientRequest request)
  {
    try
    {
      Patient patient = _patientMapper.ConvertToEntity(request);
      Patient? updatedPatient = _patientRepository.Update(id, patient);

      if (updatedPatient != null)
      {
        return new ReturnModel<Patient?>()
        {
          Data = updatedPatient,
          Message = $"{id} numaralı hasta başarılı bir şekilde güncellenmiştir.",
          Success = true,
          StatusCode = System.Net.HttpStatusCode.OK
        };
      }
      else
      {
        return new ReturnModel<Patient?>()
        {
          Data = null,
          Message = $"Hasta güncellenemedi çünkü {id} numaralı bir hasta, hastanemizde mevcut değil.",
          Success = false,
          StatusCode = System.Net.HttpStatusCode.NotFound
        };
      }
    }
    catch (ValidationException ex)
    {
      return ReturnModelOfException<Patient?>(ex);
    }
    catch (Exception ex)
    {
      return ReturnModelOfException<Patient?>(ex);
    }
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
}
