using apbd11.DTOs;
using apbd11.Services;
using Microsoft.AspNetCore.Mvc;

namespace apbd11.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PrescriptionsController
{
    private readonly IPrescriptionsService _PrescriptionsService;
        
    public PrescriptionsController(IPrescriptionsService prescriptionsService) 
    { 
        _PrescriptionsService = prescriptionsService;
    }
    
    [HttpPost] 
    public async Task<IActionResult> CreatePrescriptionAsync(CancellationToken token, 
        [FromBody] PrescriptionRequestDto dto)
    { 
        return null;
    }
        
}