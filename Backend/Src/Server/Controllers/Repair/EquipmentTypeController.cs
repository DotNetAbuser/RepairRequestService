namespace Server.Controllers.Repair;

[ApiController]
[Route("api/repair/equipment_type")]
public class EquipmentTypeController(IEquipmentTypeService equipmentTypeService)
    : ControllerBase
{
    private readonly IEquipmentTypeService _equipmentTypeService = equipmentTypeService;

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllAsync()
    {
        var response = await _equipmentTypeService.GetAllAsync();
        return Ok(response);
    }

    [HttpGet("{equipmentTypeId}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetByIdAsync(string equipmentTypeId)
    {
        var response = await _equipmentTypeService.GetByIdAsync(equipmentTypeId);
        return Ok(response);
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> CreateAsync(EquipmentTypeRequest request)
    {
        return Ok(await _equipmentTypeService.CreateAsync(request));
    }

    [HttpPut("{equipmentTypeId}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> UpdateAsync(string equipmentTypeId, EquipmentTypeRequest request)
    {
        return Ok(await _equipmentTypeService.UpdateAsync(equipmentTypeId, request));
    }

    [HttpDelete]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> DeleteAsync(string equipmentTypeId)
    {
        return Ok(await _equipmentTypeService.DeleteAsync(equipmentTypeId));
    }
    
}