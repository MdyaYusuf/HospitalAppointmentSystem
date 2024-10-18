using HospitalAppointmentSystem.WebApi.Dtos.Appointments.Requests;
using HospitalAppointmentSystem.WebApi.Service.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAppointmentSystem.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppointmentsController : ControllerBase
{
  private IAppointmentService _appointmentService;
  public AppointmentsController(IAppointmentService appointmentService)
  {
    _appointmentService = appointmentService;   
  }

  [HttpGet("getall")]
  public IActionResult GetAll()
  {
    var appointments = _appointmentService.GetAll();
    return Ok(appointments);
  }

  [HttpGet("getbyid")]
  public IActionResult GetById([FromQuery] Guid id)
  {
    var appointment = _appointmentService.GetById(id);
    return Ok(appointment);
  }

  [HttpPost("add")]
  public IActionResult Add([FromBody] CreateAppointmentRequest request)
  {
    var createdAppointment = _appointmentService.Add(request);
    return Ok(createdAppointment);
  }

  [HttpDelete("remove-past-appointments")]
  public IActionResult RemovePastAppointments()
  {
    _appointmentService.RemovePastAppointments();
    return Ok("Tarihi geçmiş bütün randevular silindi.");
  }
}
