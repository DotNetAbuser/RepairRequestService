namespace Core.IRepositories;

public interface IProblemTypeRepository
{
    Task<IEnumerable<ProblemTypeEntity>> GetAllByEquipmentTypeIdAsync(int equipmentTypeId);


    Task<ProblemTypeEntity?> GetByNameAndEquipmentIdAsync(string problemTypeName, int equipmentTypeId);
    Task<ProblemTypeEntity?> GetByIdAsync(int problemTypeId);
    Task UpdateAsync(ProblemTypeEntity problemTypeEntity);
    Task CreateAsync(ProblemTypeEntity problemTypeEntity);
    Task DeleteAsync(ProblemTypeEntity problemTypeEntity);
    Task<bool> IsExistByNameAndEquipmentTypeIdAsync(string name, int equipmentTypeId);
}