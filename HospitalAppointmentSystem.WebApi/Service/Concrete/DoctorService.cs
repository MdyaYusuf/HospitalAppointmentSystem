using HospitalAppointmentSystem.WebApi.Dtos.Doctors.Requests;
using HospitalAppointmentSystem.WebApi.Dtos.Doctors.Responses;
using HospitalAppointmentSystem.WebApi.Exceptions;
using HospitalAppointmentSystem.WebApi.Models;
using HospitalAppointmentSystem.WebApi.Models.ReturnModels;
using HospitalAppointmentSystem.WebApi.Repository.Abstract;
using HospitalAppointmentSystem.WebApi.Service.Abstract;
using HospitalAppointmentSystem.WebApi.Service.Mappers;

namespace HospitalAppointmentSystem.WebApi.Service.Concrete;

public class DoctorService : IDoctorService
{
  private IDoctorRepository _doctorRepository;
  private DoctorMapper _doctorMapper;
  public DoctorService(IDoctorRepository doctorRepository, DoctorMapper doctorMapper)
  {
    _doctorRepository = doctorRepository;
    _doctorMapper = doctorMapper;
  }

  public ReturnModel<Doctor> Add(CreateDoctorRequest request)
  {
    try
    {
      CheckDoctorName(request.Name);
      Doctor doctor = _doctorMapper.ConvertToEntity(request);
      Doctor createdDoctor = _doctorRepository.Add(doctor);
      return new ReturnModel<Doctor>()
      {
        Data = createdDoctor,
        Message = "Doktor başarılı bir şekilde eklendi.",
        Success = true,
        StatusCode = System.Net.HttpStatusCode.OK
      };
    }
    catch (NotFoundException ex)
    {
      return ReturnModelOfException<Doctor>(ex);
    }
    catch (ValidationException ex)
    {
      return ReturnModelOfException<Doctor>(ex);
    }
    catch (Exception ex)
    {
      return ReturnModelOfException<Doctor>(ex);
    }
  }

  public ReturnModel<List<DoctorResponseDto>> GetAll()
  {
    try
    {
      List<Doctor> doctors = _doctorRepository.GetAll();
      List<DoctorResponseDto> responseList = _doctorMapper.ConvertToResponseList(doctors);
      return new ReturnModel<List<DoctorResponseDto>>()
      {
        Data = responseList,
        Message = "Doktor listesi başarılı bir şekilde alındı.",
        Success = true,
        StatusCode = System.Net.HttpStatusCode.OK
      };
    }
    catch (NotFoundException ex)
    {
      return ReturnModelOfException<List<DoctorResponseDto>>(ex);
    }
    catch (ValidationException ex)
    {
      return ReturnModelOfException<List<DoctorResponseDto>>(ex);
    }
    catch (Exception ex)
    {
      return ReturnModelOfException<List<DoctorResponseDto>>(ex);
    }
  }

  public ReturnModel<DoctorResponseDto?> GetById(int id)
  {
    try
    {
      Doctor? doctor = _doctorRepository.GetById(id);
      if (doctor != null)
      {
        DoctorResponseDto? response = _doctorMapper.ConvertToResponse(doctor);
        return new ReturnModel<DoctorResponseDto?>()
        {
          Data = response,
          Message = $"Aradığınız {id} numaralı doktor başarılı bir şekilde bulundu.",
          Success = true,
          StatusCode = System.Net.HttpStatusCode.OK
        };
      }
      else
      {
        return new ReturnModel<DoctorResponseDto?>()
        {
          Data = null,
          Message = $"Aradığınız {id} numarasına sahip bir doktor hastanemizde mevcut değildir.",
          Success = false,
          StatusCode = System.Net.HttpStatusCode.NotFound
        };
      }
    }
    catch (ValidationException ex)
    {
      return ReturnModelOfException<DoctorResponseDto?>(ex);
    }
    catch (Exception ex)
    {
      return ReturnModelOfException<DoctorResponseDto?>(ex);
    }
  }

  public ReturnModel<Doctor?> Remove(int id)
  {
    try
    {
      Doctor? deletedDoctor = _doctorRepository.Remove(id);

      if (deletedDoctor != null)
      {
        return new ReturnModel<Doctor?>()
        {
          Data = deletedDoctor,
          Message = $"{id} numaralı doktor başarılı bir şekilde silindi.",
          Success = true,
          StatusCode = System.Net.HttpStatusCode.OK
        };
      }
      else
      {
        return new ReturnModel<Doctor?>()
        {
          Data = null,
          Message = $"{id} numaralı doktor silinemedi çünkü zaten yok.",
          Success = false,
          StatusCode = System.Net.HttpStatusCode.NotFound
        };
      }
    }
    catch (ValidationException ex)
    {
      return ReturnModelOfException<Doctor?>(ex);
    }
    catch (Exception ex)
    {
      return ReturnModelOfException<Doctor?>(ex);
    }
  }

  public ReturnModel<Doctor?> Update(int id, UpdateDoctorRequest request)
  {
    try
    {
      Doctor doctor = _doctorMapper.ConverToEntity(request);
      Doctor? updatedDoctor = _doctorRepository.Update(id, doctor);

      if (updatedDoctor != null)
      {
        return new ReturnModel<Doctor?>()
        {
          Data = updatedDoctor,
          Message = $"{id} numaralı doktor başarılı bir şekilde güncellenmiştir.",
          Success = true,
          StatusCode = System.Net.HttpStatusCode.OK
        };
      }
      else
      {
        return new ReturnModel<Doctor?>()
        {
          Data = null,
          Message = $"Doktor güncellenemedi çünkü {id} numaralı bir doktor, hastanemizde mevcut değil.",
          Success = false,
          StatusCode = System.Net.HttpStatusCode.NotFound
        };
      }
    }
    catch (ValidationException ex)
    {
      return ReturnModelOfException<Doctor?>(ex);
    }
    catch (Exception ex)
    {
      return ReturnModelOfException<Doctor?>(ex);
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

  private void CheckDoctorName(string name)
  {
    if (string.IsNullOrWhiteSpace(name))
    {
      throw new ValidationException("Doktor ismi 1 karakterden az olamaz.");
    }
  }
}
