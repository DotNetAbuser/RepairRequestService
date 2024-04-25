using Core.Enums;

namespace Infrastructure.Services;

public class RepairRequestService(IRepairRequestRepository repairRequestRepository,
    IUserRepository userRepository,
    IEquipmentTypeRepository equipmentTypeRepository,
    IProblemTypeRepository problemTypeRepository,
    IRepairStatusRepository repairStatusRepository)
    : IRepairRequestService
{
    private readonly IRepairRequestRepository _repairRequestRepository = repairRequestRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IEquipmentTypeRepository _equipmentTypeRepository = equipmentTypeRepository;
    private readonly IProblemTypeRepository _problemTypeRepository = problemTypeRepository;
    private readonly IRepairStatusRepository _repairStatusRepository = repairStatusRepository;

    public async Task<Result<PaginatedData<RepairResponse>>> GetPaginatedRequests(int pageNumber, int pageSize,
        string searchTerm, string sortColumn, string sortOrder)
    {
        var paginatedRequestsEntity = await _repairRequestRepository
            .GetPaginatedRequestsAsync(pageNumber, pageSize,
                searchTerm, sortColumn, sortOrder);
        var requestsResponse = new List<RepairResponse>();
        foreach (var request in paginatedRequestsEntity.Data)
        {
            requestsResponse.Add(new RepairResponse
            {
                Id = request.Id.ToString(),
                UserId = request.UserId.ToString(),
                EquipmentType = request.EquipmentType.Name,
                ProblemType = request.ProblemType.Name,
                RepairStatus = request.RepairStatus.Name,
                ProblemDescription = request.ProblemDescription,
                LastName = request.User.LastName,
                FirstName = request.User.FirstName,
                MiddleName = request.User.MiddleName,
                Email = request.User.Email,
                Created = request.CreatedOn
            });
        }

        var paginatedRequestsResponse = new PaginatedData<RepairResponse>(data: requestsResponse,
            totalCount: paginatedRequestsEntity.TotalCount);
        return await Result<PaginatedData<RepairResponse>>
            .SuccessAsync(paginatedRequestsResponse, "Заявки успешно получены.");
    }

    public async Task<Result<PaginatedData<RepairResponse>>> GetPaginatedRequestsByUserid(string currentUserId, string userId,
        int pageNumber, int pageSize, string searchTerm,
        string sortColumn, string sortOrder)
    {
        var currentUser = await _userRepository.GetByUserIdAsync(Guid.Parse(currentUserId));
        var isUserRequests = currentUserId == userId;
        var isCurrentUserAdmin = currentUser.RoleId == (int)Role.Admin;
        var isAccessed = isUserRequests || isCurrentUserAdmin;
        if (!isAccessed)
        {
            return await Result<PaginatedData<RepairResponse>>
                .FailAsync("Для получения заявок других пользователей нужна роль админа!");
        }
        
        var paginatedRequestsEntity = await _repairRequestRepository
            .GetPaginatedRequestsByUserIdAsync(Guid.Parse(currentUserId), pageNumber, pageSize,
                searchTerm, sortColumn, sortOrder);
        var requestsResponse = new List<RepairResponse>();
        foreach (var request in paginatedRequestsEntity.Data)
        {
            requestsResponse.Add(new RepairResponse
            {
                Id = request.Id.ToString(),
                UserId = request.UserId.ToString(),
                EquipmentType = request.EquipmentType.Name,
                ProblemType = request.ProblemType.Name,
                RepairStatus = request.RepairStatus.Name,
                ProblemDescription = request.ProblemDescription,
                LastName = request.User.LastName,
                FirstName = request.User.FirstName,
                MiddleName = request.User.MiddleName,
                Email = request.User.Email,
                Created = request.CreatedOn
            });
        }

        var paginatedRequestsResponse = new PaginatedData<RepairResponse>(data: requestsResponse,
            totalCount: paginatedRequestsEntity.TotalCount);
        return await Result<PaginatedData<RepairResponse>>
            .SuccessAsync(paginatedRequestsResponse, "Заявки успешно получены.");
    }
    
    public async Task<Result<RepairResponse>> GetById(string currentUserId, string requestId)
    {
        var request = await _repairRequestRepository
            .GetByIdWithAllIncludesAsync(Guid.Parse(requestId));
        if (request == null)
        {
            return await Result<RepairResponse>
                .FailAsync("Заявка не найдена!");
        }
        
        var currentUser = await _userRepository.GetByUserIdAsync(Guid.Parse(currentUserId));
        var isUserRequests = currentUserId == request.UserId.ToString();
        var isCurrentUserAdmin = currentUser.RoleId == (int)Role.Admin;
        var isAccessed = isUserRequests || isCurrentUserAdmin;
        if (!isAccessed)
        {
            return await Result<RepairResponse>
                .FailAsync("Для просмотра завки другого пользователя необходимо иметь роль админа!");
        }

        var repairResponse = new RepairResponse
        {
            Id = request.Id.ToString(),
            UserId = request.UserId.ToString(),
            EquipmentType = request.EquipmentType.Name,
            ProblemType = request.ProblemType.Name,
            RepairStatus = request.RepairStatus.Name,
            ProblemDescription = request.ProblemDescription,
            LastName = request.User.LastName,
            FirstName = request.User.FirstName,
            MiddleName = request.User.MiddleName,
            Email = request.User.Email,
            Created = request.CreatedOn
        };
        return await Result<RepairResponse>
            .SuccessAsync(repairResponse, "Заявка успешно получена!");
    }

    public async Task<Result> CreateRequestAsync(string currentUserId, RepairRequest request)
    {
        var currentUser = await _userRepository.GetByUserIdAsync(Guid.Parse(currentUserId));
        var isUserRequests = currentUserId == request.UserId;
        var isCurrentUserAdmin = currentUser.RoleId == (int)Role.Admin;
        var isAccessed = isUserRequests || isCurrentUserAdmin;
        if (!isAccessed)
        {
            return await Result<string>
                .FailAsync("Для создания завки для другого пользователя необходимо иметь роль админа!");
        }
        
        var requestEntity = new RepairRequestEntity
        {
            Id = Guid.NewGuid(),
            UserId = Guid.Parse(request.UserId),
            EquipmentTypeId = Convert.ToInt32(request.EquipmentTypeId),
            ProblemTypeId = Convert.ToInt32(request.ProblemTypeId),
            RepairStatusId = Convert.ToInt32(request.RepairStatusId),
            ProblemDescription = request.ProblemDescription,
            CreatedOn = DateTime.UtcNow
        };
        await _repairRequestRepository.CreateAsync(requestEntity);
        return await Result<string>
            .SuccessAsync("Заявка успешно создана.");
    }

    public async Task<Result> UpdateRequestAsync(string currentUserId, string requestId, RepairRequest request)
    {
        var currentUser = await _userRepository.GetByUserIdAsync(Guid.Parse(currentUserId));
        var isUserRequests = currentUserId == request.UserId;
        var isCurrentUserAdmin = currentUser.RoleId == (int)Role.Admin;
        var isAccessed = isUserRequests || isCurrentUserAdmin;
        if (!isAccessed)
        {
            return await Result<string>
                .FailAsync("Для обновления не своей заявки необходимо иметь роль админа!");
        }
        
        var requestRepair = await _repairRequestRepository
            .GetByIdWithAllIncludesAsync(Guid.Parse(requestId));
        if (requestRepair == null)
        {
            return await Result<string>
                .FailAsync("Заявка не найдена!");
        }
        
        requestRepair.EquipmentTypeId = Convert.ToInt32(request.EquipmentTypeId);
        requestRepair.EquipmentType = null;
        requestRepair.ProblemTypeId = Convert.ToInt32(request.ProblemTypeId);
        requestRepair.ProblemType = null;
        requestRepair.RepairStatusId = Convert.ToInt32(request.RepairStatusId);
        requestRepair.RepairStatus = null;
        
        requestRepair.ProblemDescription = request.ProblemDescription;
        
        await _repairRequestRepository.UpdateAsync(requestRepair);
        return await Result<string>
            .SuccessAsync("Заявка успешно изменена.");
    }

    public async Task<Result> ToggleRepairStatusAsync(string currentUserId, string repairRequestId, RepairStatusRequest request)
    {
      
        var requestRepair = await _repairRequestRepository
            .GetByIdWithAllIncludesAsync(Guid.Parse(repairRequestId));
        if (requestRepair == null)
        {
            return await Result<string>
                .FailAsync("Заявка не найдена!");
        }
        
        var currentUser = await _userRepository.GetByUserIdAsync(Guid.Parse(currentUserId));
        var isUserRequests = currentUserId == requestRepair.UserId.ToString();
        var isCurrentUserAdmin = currentUser.RoleId == (int)Role.Admin;
        var isAccessed = isUserRequests || isCurrentUserAdmin;
        if (!isAccessed)
        {
            return await Result<string>
                .FailAsync("Для измение статуса не своей заявки необходимо иметь роль админа!");
        }
        
        requestRepair.RepairStatusId = request.Id;
        requestRepair.RepairStatus = null;
        requestRepair.UpdatedOn = DateTime.UtcNow;
        await _repairRequestRepository.UpdateAsync(requestRepair);
        return await Result<string>
            .SuccessAsync("Статус заявки успешно изменена.");
    }
}