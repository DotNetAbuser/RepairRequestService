namespace Infrastructure.Services;

public class RepairStatusService(IRepairStatusRepository repairStatusRepository)
    : IRepairStatusService
{
    private readonly IRepairStatusRepository _repairStatusRepository = repairStatusRepository;
    public async Task<Result<IEnumerable<RepairStatusResponse>>> GetAllStatusesAsync()
    {
        var repairStatusesEntity = await _repairStatusRepository.GetAllAsync();
        var repairStatusesResponse = new List<RepairStatusResponse>();
        foreach (var repairStatus in repairStatusesEntity)
        {
            repairStatusesResponse.Add(new()
            {
                Id = repairStatus.Id,
                Name = repairStatus.Name
            });
        }
        return await Result<IEnumerable<RepairStatusResponse>>
            .SuccessAsync(repairStatusesResponse, "Список статусов успешно получен!");
    }

    public async Task<Result<RepairStatusResponse>> GetByIdAsync(string statusRepairRequestId)
    {
        var repairStatusEntity = await _repairStatusRepository
            .GetByIdAsync(Convert.ToInt32(statusRepairRequestId));
        if (repairStatusEntity == null)
        {
            return await Result<RepairStatusResponse>
                .FailAsync("Статус не найден!");
        }

        var repairStatusResponse = new RepairStatusResponse
        {
            Id = repairStatusEntity.Id,
            Name = repairStatusEntity.Name
        };

        return await Result<RepairStatusResponse>
            .SuccessAsync(repairStatusResponse, "Статус получен.");
    }

    public async Task<Result> CreateAsync(RepairStatusRequest request)
    {
        var isExistByName = await _repairStatusRepository
            .IsExistByName(request.Name);
        if (isExistByName)
        {
            return await Result<string>
                .FailAsync("Статус с таким же именем уже существует!");
        }
        var statusEntity = new RepairStatusEntity
        {
            Name = request.Name,
            CreatedOn = DateTime.UtcNow
        };
        await _repairStatusRepository.CreateAsync(statusEntity);
        return await Result<string>
            .SuccessAsync("Статус успешно создан.");
    }

    public async Task<Result> UpdateStatusAsync(string statusRepairRequestId, RepairStatusRequest request)
    {
        var isExistForUpdateByName = await _repairStatusRepository
            .IsExistForUpdateByName(Convert.ToInt32(statusRepairRequestId), request.Name);
        if (isExistForUpdateByName)
        {
            return await Result<string>
                .FailAsync("Статус с таким же названием уже существует!");
        }
        var statusRepairRequestEntity = await _repairStatusRepository
            .GetByIdAsync(Convert.ToInt32(statusRepairRequestId));
        if (statusRepairRequestEntity == null)
        {
            return await Result<string>
                .FailAsync("Статус не найден");
        }
        statusRepairRequestEntity.Name = request.Name;
        statusRepairRequestEntity.UpdatedOn = DateTime.UtcNow;
        await _repairStatusRepository.UpdateAsync(statusRepairRequestEntity);
        return await Result<string>
            .SuccessAsync("Статус успешно изменён");
    }

    public async Task<Result> DeleteAsync(string statusRepairRequestId)
    {
        var statusRepairRequestEntity = await _repairStatusRepository
            .GetByIdAsync(Convert.ToInt32(statusRepairRequestId));
        if (statusRepairRequestEntity == null)
        {
            return await Result<string>
                .FailAsync("Статус не найден!");
        }

        await _repairStatusRepository.DeleteAsync(statusRepairRequestEntity);
        return await Result<string>
            .SuccessAsync("Статус успешно удалён.");
    }
}