namespace Core.IServices;

public interface IEquipmentTypeService
{
    Task<Result<IEnumerable<EquipmentTypeResponse>>> GetAllAsync();
    Task<Result<EquipmentTypeResponse>> GetByIdAsync(string equipmentTypeId);
    Task<Result> CreateAsync(EquipmentTypeRequest request);
    Task<Result> UpdateAsync(string equipmentTypeId, EquipmentTypeRequest request);
    Task<Result> DeleteAsync(string equipmentTypeId);
}