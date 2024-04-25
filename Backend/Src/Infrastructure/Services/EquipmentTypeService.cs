namespace Infrastructure.Services;

public class EquipmentTypeService(IEquipmentTypeRepository equipmentTypeRepository) 
    : IEquipmentTypeService
{
    private readonly IEquipmentTypeRepository _equipmentTypeRepository = equipmentTypeRepository;

    public async Task<Result<IEnumerable<EquipmentTypeResponse>>> GetAllAsync()
    {
        var equipmentsTypeEntity = await _equipmentTypeRepository
            .GetAllAsync();
        var equipmentsTypeResponse = new List<EquipmentTypeResponse>();
        foreach (var equipmentTypeEntity in equipmentsTypeEntity)
        {
            equipmentsTypeResponse.Add(new()
            {
                Id = equipmentTypeEntity.Id,
                Name = equipmentTypeEntity.Name
            });
        }

        return await Result<IEnumerable<EquipmentTypeResponse>>
            .SuccessAsync(equipmentsTypeResponse, "Типы обородования успешно получены.");
    }

    public async Task<Result<EquipmentTypeResponse>> GetByIdAsync(string equipmentTypeId)
    {
        var equipmentTypeEntity = await _equipmentTypeRepository
            .GetByIdAsync(Convert.ToInt32(equipmentTypeId));
        if (equipmentTypeEntity == null)
        {
            return await Result<EquipmentTypeResponse>
                .FailAsync("Тип оборудования не найдено!");
        }

        var equipmentTypeResponse = new EquipmentTypeResponse
        {
            Id = equipmentTypeEntity.Id,
            Name = equipmentTypeEntity.Name
        };
        return await Result<EquipmentTypeResponse>
            .SuccessAsync(equipmentTypeResponse, "Тип оборудования успешно получено");
    }

    public async Task<Result> CreateAsync(EquipmentTypeRequest request)
    {
        var isExistEquipmentTypeWithName = await _equipmentTypeRepository.IsExistByNameAsync(request.Name);
        if (isExistEquipmentTypeWithName)
        {
            return await Result<string>
                .FailAsync("Тип оборудование с таким названием уже существует!");
        }

        var equipmentTypeEntity = new EquipmentTypeEntity
        {
            Name = request.Name,
            
            CreatedOn = DateTime.UtcNow
        };
        await _equipmentTypeRepository.CreateAsync(equipmentTypeEntity);
        return await Result<string>
            .SuccessAsync("Тип оборудования успешно создан.");
    }

    public async Task<Result> UpdateAsync(string equipmentTypeId, EquipmentTypeRequest request)
    {
        var isExistForUpdateByName = await _equipmentTypeRepository
            .IsExistForUpdateByNameAndEquipmentTypeIdAsync(request.Name, Convert.ToInt32(equipmentTypeId));
        if (isExistForUpdateByName)
        {
            return await Result<string>
                .FailAsync("Тип оборудования с таким же названием уже существуют!");
        }
        var equipmentTypeEntity = await _equipmentTypeRepository
            .GetByIdAsync(Convert.ToInt32(equipmentTypeId));
        if (equipmentTypeEntity == null)
        {
            return await Result<string>
                .FailAsync("Тип оборудования не найден!");
        }
        equipmentTypeEntity.Name = request.Name;
        await _equipmentTypeRepository.UpdateAsync(equipmentTypeEntity);
        return await Result<string>
            .SuccessAsync("Тип оборудования успешно обновлён!");
    }

    public async Task<Result> DeleteAsync(string equipmentTypeId)
    {
        var equipmentTypeEntity = await _equipmentTypeRepository
            .GetByIdAsync(Convert.ToInt32(equipmentTypeId));
        if (equipmentTypeEntity == null)
        {
            return await Result<string>
                .FailAsync("Тип оборудования не найден!");
        }
        await _equipmentTypeRepository.DeleteAsync(equipmentTypeEntity);
        return await Result<string>
            .SuccessAsync("Тип оборудования успешно удалён!");
    }
}