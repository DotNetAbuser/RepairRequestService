namespace Client.Infrastructure.Managers.EquipmentTypeManager;

public interface IEquipmentTypeManager
{
    Task<IResult<IEnumerable<EquipmentTypeResponse>>> GetAllAsync();
    Task<IResult<EquipmentTypeResponse>> GetByIdAsync(string equipmentTypeId);
    Task<IResult> CreateAsync(EquipmentTypeRequest request);
    Task<IResult> UpdateAsync(string equipmentTypeId, EquipmentTypeRequest request);
    Task<IResult> DeleteAsync(string equipmentTypeId);
}