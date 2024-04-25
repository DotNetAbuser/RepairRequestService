namespace Server.Controllers.Repair;

[ApiController]
[Route("api/repair/repair_status")]
public class RepairStatusController(IRepairStatusService repairStatusService )
    : ControllerBase
{
    private readonly IRepairStatusService _repairStatusService = repairStatusService;

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllStatuses()
    {
        var response = await _repairStatusService.GetAllStatusesAsync();
        return Ok(response);
    }

    [HttpGet("{statusRepairRequestId}")]
    [Authorize]
    public async Task<IActionResult> GetByIdAsync(string statusRepairRequestId)
    {
        var response = await _repairStatusService.GetByIdAsync(statusRepairRequestId);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(RepairStatusRequest request)
    {
        return Ok(await _repairStatusService.CreateAsync(request));
    }

    [HttpPut("{statusRepairRequestId}")]
    public async Task<IActionResult> UpdateAsync(string statusRepairRequestId, RepairStatusRequest request)
    {
        return Ok(await _repairStatusService.UpdateStatusAsync(statusRepairRequestId, request));
    }

    [HttpDelete("{statusRepairRequestId}")]
    public async Task<IActionResult> DeleteAsync(string statusRepairRequestId)
    {
        return Ok(await _repairStatusService.DeleteAsync(statusRepairRequestId));
    }
    
}