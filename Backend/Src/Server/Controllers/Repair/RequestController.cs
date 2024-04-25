namespace Server.Controllers.Repair;

[ApiController]
[Route("api/repair/request")]
public class RequestController(IRepairRequestService repairRequestService)
    : ControllerBase
{
    private readonly IRepairRequestService _repairRequestService = repairRequestService;

    [HttpGet]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetPaginatedAllRequestsAsync(int pageNumber, int pageSize,
        string? searchTerm, string? sortColumn, string? sortOrder)
    {
        var response = await _repairRequestService.GetPaginatedRequests(pageNumber, pageSize,
            searchTerm, sortColumn,sortOrder);
        return Ok(response);
    }
    
    [HttpGet("user/{userId}")]
    [Authorize]
    public async Task<IActionResult> GetPaginatedAllRequestsByUserIdAsync(string userId, int pageNumber, int pageSize,
        string? searchTerm, string? sortColumn, string? sortOrder)
    {
        var currentUserId = HttpContext.User.GetLoggedInUserId<string>();
        var response = await _repairRequestService.GetPaginatedRequestsByUserid(currentUserId, userId,
            pageNumber, pageSize,
            searchTerm, sortColumn,sortOrder);
        return Ok(response);
    }
    
    [HttpGet("{repairRequestId}")]
    [Authorize]
    public async Task<IActionResult> GetByIdAsync(string repairRequestId)
    {
        var currentUserId = HttpContext.User.GetLoggedInUserId<string>();
        var response = await _repairRequestService.GetById(currentUserId, repairRequestId);
        return Ok(response);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateAsync(RepairRequest request)
    {
        var currentUserId = HttpContext.User.GetLoggedInUserId<string>();
        return Ok(await _repairRequestService.CreateRequestAsync(currentUserId, request));
    }
    
    [HttpPut("{repairRequestId}")]
    [Authorize]
    public async Task<IActionResult> UpdateAsync(string repairRequestId, RepairRequest request)
    {
        var currentUserId = HttpContext.User.GetLoggedInUserId<string>();
        return Ok(await _repairRequestService.UpdateRequestAsync(currentUserId ,repairRequestId, request));
    }
    
    [HttpPut("status/{repairRequestId}")]
    [Authorize]
    public async Task<IActionResult> ToggleRequestStatusAsync(string repairRequestId, RepairStatusRequest request)
    {
        var currentUserId = HttpContext.User.GetLoggedInUserId<string>();
        return Ok(await _repairRequestService.ToggleRepairStatusAsync(currentUserId, repairRequestId, request));
    }
}