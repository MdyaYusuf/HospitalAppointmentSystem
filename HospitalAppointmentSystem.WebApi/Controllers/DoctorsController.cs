using HospitalAppointmentSystem.WebApi.Dtos.Doctors.Requests;
using HospitalAppointmentSystem.WebApi.Dtos.Doctors.Responses;
using HospitalAppointmentSystem.WebApi.Models;
using HospitalAppointmentSystem.WebApi.Models.ReturnModels;
using HospitalAppointmentSystem.WebApi.Service.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAppointmentSystem.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DoctorsController : ControllerBase
{
  private IDoctorService _doctorService;
  public DoctorsController(IDoctorService doctorService)
  {
    _doctorService = doctorService;
  }

  [HttpGet("getall")]
  public IActionResult GetAll()
  {
    var doctors = _doctorService.GetAll();
    return Ok(doctors);
  }

  [HttpGet("getbyid/{id:int}")]
  public IActionResult GetById([FromRoute] int id)
  {
    var doctor = _doctorService.GetById(id);
    return Ok(doctor);
  }

  [HttpPost("add")]
  public IActionResult Add([FromBody] CreateDoctorRequest request)
  {
    var createdProduct = _doctorService.Add(request);
    return Ok(createdProduct);
  }

  [HttpDelete("remove")]
  public IActionResult Remove([FromQuery] int id)
  {
    var deletedDoctor = _doctorService.Remove(id);
    return Ok(deletedDoctor);
  }

  [HttpPut("update/{id:int}")]
  public IActionResult Update([FromRoute]int id, [FromBody]UpdateDoctorRequest request)
  {
    var updatedDoctor = _doctorService.Update(id, request);
    return Ok(updatedDoctor);
  }
}
