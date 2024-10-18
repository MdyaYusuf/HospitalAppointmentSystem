using HospitalAppointmentSystem.WebApi.Dtos.Patients.Requests;
using HospitalAppointmentSystem.WebApi.Service.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAppointmentSystem.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientsController : ControllerBase
{
  private IPatientService _patientService;
  public PatientsController(IPatientService patientService)
  {
    _patientService = patientService;
  }

  [HttpGet("getall")]
  public IActionResult GetAll()
  {
    var patients = _patientService.GetAll();
    return Ok(patients);
  }

  [HttpGet("getbyid")]
  public IActionResult GetById([FromQuery] Guid id)
  {
    var patient = _patientService.GetById(id);
    return Ok(patient);
  }

  [HttpPost("add")]
  public IActionResult Add([FromBody] CreatePatientRequest request)
  {
    var createdPatient = _patientService.Add(request);
    return Ok(createdPatient);
  }

  [HttpDelete("remove")]
  public IActionResult Remove([FromQuery] Guid id)
  {
    var deletedPatient = _patientService.Remove(id);
    return Ok(deletedPatient);
  }

  [HttpPut("update")]
  public IActionResult Update([FromQuery] Guid id, [FromBody] UpdatePatientRequest request)
  {
    var updatedPatient = _patientService.Update(id, request);
    return Ok(updatedPatient);
  }
}
