namespace Infrastructure.Services;

public class ProblemTypeService(IProblemTypeRepository problemTypeRepository,
    IEquipmentTypeRepository equipmentTypeRepository)
    : IProblemTypeService
{
    private readonly IProblemTypeRepository _problemTypeRepository = problemTypeRepository;
    private readonly IEquipmentTypeRepository _equipmentTypeRepository = equipmentTypeRepository;
    public async Task<Result<IEnumerable<ProblemTypeResponse>>> GetAllAsync(string equipmentTypeId)
    {
        var problemsTypeEntity = await _problemTypeRepository
            .GetAllByEquipmentTypeIdAsync(Convert.ToInt32(equipmentTypeId));
        var problemsTypeResponse = new List<ProblemTypeResponse>();
        foreach (var problemType in problemsTypeEntity)
        {
            problemsTypeResponse.Add(new()
            {
                Id = problemType.Id,
                Name = problemType.Name
            });
        }
        return await Result<IEnumerable<ProblemTypeResponse>>
            .SuccessAsync(problemsTypeResponse, "Проблемы по выбранному типу оборудования успешно получены!");
    }

    public async Task<Result<ProblemTypeResponse>> GetByIdAsync(string problemTypeId)
    {
        var problemTypeEntity = await _problemTypeRepository
            .GetByIdAsync(Convert.ToInt32(problemTypeId));
        if (problemTypeEntity == null)
        {
            return await Result<ProblemTypeResponse>
                .FailAsync("Тип проблемы не найден!");
        }
        var problemTypeResponse = new ProblemTypeResponse
        {
            Id = problemTypeEntity.Id,
            Name = problemTypeEntity.Name
        };
        return await Result<ProblemTypeResponse>
            .SuccessAsync(problemTypeResponse, "Тип проблемы успешно получен.");
    }

    public async Task<Result> CreateAsync(ProblemTypeRequest request)
    {
        var isExistProblemTypeByNameAndEquipmentTypeId = await _problemTypeRepository
                .IsExistByNameAndEquipmentTypeIdAsync(request.Name, Convert.ToInt32(request.EquipmentTypeId));
        if (isExistProblemTypeByNameAndEquipmentTypeId)
        {
            return await Result<string>
                .FailAsync("Тип проблемы с таким же именем и типом оборудования уже существует!");
        }
        var problemTypeEntity = new ProblemTypeEntity
        {
            EquipmentTypeId = Convert.ToInt32(request.EquipmentTypeId),
            Name = request.Name,
            CreatedOn = DateTime.UtcNow
        };
        await _problemTypeRepository.CreateAsync(problemTypeEntity);
        return await Result<string>
            .SuccessAsync("Тип проблемы успешно создан.");
    }

    public async Task<Result> UpdateAsync(string problemId, ProblemTypeRequest request)
    {
        var isExistForUpdateByNameAndEquipmentTypeId = await _equipmentTypeRepository
            .IsExistForUpdateByNameAndEquipmentTypeIdAsync(request.Name, Convert.ToInt32(request.EquipmentTypeId));
        if (isExistForUpdateByNameAndEquipmentTypeId)
        {
            return await Result<string>
                .FailAsync("Тип проблемы с таким же именем и типом оборудования уже существует!");
        }
        var problemTypeEntity = await _problemTypeRepository
            .GetByIdAsync(Convert.ToInt32(problemId));
        if (problemTypeEntity == null)
        {
            return await Result<string>
                .FailAsync("Тип проблемы не найден!");
        }
        problemTypeEntity.EquipmentTypeId = Convert.ToInt32(request.EquipmentTypeId);
        problemTypeEntity.Name = request.Name;
        problemTypeEntity.UpdatedOn = DateTime.UtcNow;
        await _problemTypeRepository.UpdateAsync(problemTypeEntity);
        return await Result<string>
            .SuccessAsync("Тип проблемы успешно обновлен.");
    }

    public async Task<Result> DeleteAsync(string problemTypeId)
    {
        var problemTypeEntity = await _problemTypeRepository
            .GetByIdAsync(Convert.ToInt32(problemTypeId));
        if (problemTypeEntity == null)
        {
            return await Result<string>
                .FailAsync("Тип проблемы не найден!");
        }
        await _problemTypeRepository.DeleteAsync(problemTypeEntity);
        return await Result<string>
            .SuccessAsync("Тип проблемы успешно удалён!");

    }
}