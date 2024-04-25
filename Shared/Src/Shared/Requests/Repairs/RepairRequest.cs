namespace Shared.Requests.Repairs;

public record RepairRequest(
    [Required] 
    string UserId,
    [Required]
    int EquipmentTypeId,
    [Required]
    int ProblemTypeId, 
    [Required]
    int RepairStatusId,
    [Required]
    string ProblemDescription);