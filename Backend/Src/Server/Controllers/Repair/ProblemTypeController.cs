namespace Server.Controllers.Repair;

[ApiController]
[Route("api/repair/problem_type")]
public class ProblemTypeController(IProblemTypeService problemTypeService)
    : ControllerBase
{
    private readonly IProblemTypeService _problemTypeService = problemTypeService;

    [HttpGet("equipment_type/{equipmentTypeId}")]
    [Authorize]
    public async Task<IActionResult> GetAllByEquipmentTypeIdAsync(string equipmentTypeId)
    {
        var response = await _problemTypeService.GetAllAsync(equipmentTypeId);
        return Ok(response);
    }

    [HttpGet("{problemTypeId}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetByIdAsync(string problemTypeId)
    {
        var response = await _problemTypeService.GetByIdAsync(problemTypeId);
        return Ok(response);
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> CreateAsync(ProblemTypeRequest request)
    {
        return Ok(await _problemTypeService.CreateAsync(request));
    }

    [HttpPut("{problemTypeId}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> UpdateAsync(string problemTypeId, ProblemTypeRequest request)
    {
        return Ok(await _problemTypeService.UpdateAsync(problemTypeId, request));
    }

    [HttpDelete("{problemTypeId}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> DeleteAsync(string problemTypeId)
    {
        return Ok(await _problemTypeService.DeleteAsync(problemTypeId));
    }
}