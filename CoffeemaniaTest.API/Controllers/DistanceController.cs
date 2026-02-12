using CoffeemaniaTest.Application.DTO.Requests;
using CoffeemaniaTest.Application.DTO.Respones;
using CoffeemaniaTest.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoffeemaniaTest.API.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class DistanceController : ControllerBase
{
    private readonly IDistanceCalculatorService _distanceCalculatorService;

    public DistanceController(IDistanceCalculatorService distanceCalculatorService)
    {
        _distanceCalculatorService = distanceCalculatorService;
    }
    
    [HttpPost]
    [Route("[action]")]
    public async Task<ActionResult<DistanceResponse>> CalculateDistanceAsync(
        [FromBody] DistanceRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var distance = await _distanceCalculatorService.CalculateDistanceAsync(request.Coordinate1,
                request.Coordinate2, cancellationToken);

            return Ok(new DistanceResponse(distance, $"Расстояние между точками составляет {distance}"));
        }
        catch (ArgumentNullException argumentNullException)
        {
            return BadRequest(new { argumentNullException.Message });
        }
        catch (OperationCanceledException operationCanceledException)
        {
            return BadRequest(new { operationCanceledException.Message });
        }
        catch (Exception exception)
        {
            return BadRequest(new { exception.Message });
        }
    }
}