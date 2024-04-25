namespace Core.IRepositories;

public interface IEquipmentTypeRepository
{
    Task<EquipmentTypeEntity?> GetByNameAsync(string name);
    Task<IEnumerable<EquipmentTypeEntity>> GetAllAsync();
    Task<EquipmentTypeEntity?> GetByIdAsync(int equipmentTypeId);
    Task CreateAsync(EquipmentTypeEntity equipmentTypeEntity);
    Task UpdateAsync(EquipmentTypeEntity equipmentTypeEntity);
    Task DeleteAsync(EquipmentTypeEntity equipmentTypeEntity);
    Task<bool> IsExistByNameAsync(string name);
    Task<bool> IsExistForUpdateByNameAndEquipmentTypeIdAsync(string name, int equipmentTypeId);
}