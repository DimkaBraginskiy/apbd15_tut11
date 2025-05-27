using apbd11.Services;
using Microsoft.AspNetCore.Mvc;

namespace apbd11.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PatientsController
{
    private readonly IPatientsService _patientsService;

    public PatientsController(IPatientsService patientsService)
    {
        _patientsService = patientsService;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatientByIdAsync(CancellationToken token, int id)
    {
        var patient = await _patientsService.GetPatientByIdAsync(token, id);
        if (patient == null)
        {
            return new NotFoundObjectResult($"Patient with {id} was not found.");
        }

        return new OkObjectResult(patient);
    }
}