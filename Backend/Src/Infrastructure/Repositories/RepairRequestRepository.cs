namespace Infrastructure.Repositories;

public class RepairRequestRepository(ApplicationDbContext dbContext)
    : IRepairRequestRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    
     public async Task<PaginatedData<RepairRequestEntity>> GetPaginatedRequestsAsync(int pageNumber, int pageSize,
         string searchTerm, string sortColumn, string sortOrder)
    {
        var query = _dbContext.RepairRequests
            .AsNoTracking();

        if (!string.IsNullOrWhiteSpace(searchTerm))
            query = query.Where(r =>
                r.EquipmentType.Name.ToLower().Contains(searchTerm.ToLower()) ||
                r.ProblemType.Name.ToLower().Contains(searchTerm.ToLower()) ||
                r.RepairStatus.Name.ToLower().Contains(searchTerm.ToLower()) ||
                r.User.LastName.ToLower().Contains(searchTerm.ToLower()) ||
                r.User.FirstName.ToLower().Contains(searchTerm.ToLower()) ||
                r.User.MiddleName.ToLower().Contains(searchTerm.ToLower()));

        Expression<Func<RepairRequestEntity, object>> keySelector = sortColumn?.ToLower() switch
        {
            "Тип оборудования" => r => r.EquipmentTypeId,
            "Тип проблемы" => r => r.ProblemTypeId,
            "Статус заявки" => r => r.RepairStatusId,
            "Фамилия пользователя" => r => r.User.LastName,
            "Имя пользователя" => r => r.User.FirstName,
            "Отчество пользователя" => r => r.User.MiddleName,
            _ => r => r.CreatedOn
        };

        if (sortOrder?.ToLower() == "asc") query = query.OrderBy(keySelector);
        else query = query.OrderByDescending(keySelector);
        
        var data = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Include(r => r.User)
            .Include(r => r.EquipmentType)
            .Include(r => r.ProblemType)
            .Include(r => r.RepairStatus)
            .ToListAsync();
        var totalCount = await _dbContext.RepairRequests
            .CountAsync();
        return new PaginatedData<RepairRequestEntity>(data, totalCount);
    }
    
    public async Task<PaginatedData<RepairRequestEntity>> GetPaginatedRequestsByUserIdAsync(Guid currentUserId,
        int pageNumber, int pageSize, string searchTerm,
        string sortColumn, string sortOrder)
    {
        var query = _dbContext.RepairRequests
            .AsNoTracking();

        if (!string.IsNullOrWhiteSpace(searchTerm))
            query = query.Where(r =>
                r.EquipmentType.Name.ToLower().Contains(searchTerm.ToLower()) ||
                r.ProblemType.Name.ToLower().Contains(searchTerm.ToLower()) ||
                r.RepairStatus.Name.ToLower().Contains(searchTerm.ToLower()) ||
                r.User.LastName.ToLower().Contains(searchTerm.ToLower()) ||
                r.User.FirstName.ToLower().Contains(searchTerm.ToLower()) ||
                r.User.MiddleName.ToLower().Contains(searchTerm.ToLower()));

        Expression<Func<RepairRequestEntity, object>> keySelector = sortColumn?.ToLower() switch
        {
            "Тип оборудования" => r => r.EquipmentType.Name,
            "Тип проблемы" => r => r.ProblemType.Name,
            "Статус заявки" => r => r.RepairStatus.Name,
            "Фамилия пользователя" => r => r.User.LastName,
            "Имя пользователя" => r => r.User.FirstName,
            "Отчество пользователя" => r => r.User.MiddleName,
            _ => r => r.CreatedOn
        };

        if (sortOrder?.ToLower() == "asc") query = query.OrderBy(keySelector);
        else query = query.OrderByDescending(keySelector);
        
        var data = await query
            .Where(x => x.UserId == currentUserId)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Include(r => r.User)
            .Include(r => r.EquipmentType)
            .Include(r => r.ProblemType)
            .Include(r => r.RepairStatus)
            .ToListAsync();
        var totalCount = await _dbContext.RepairRequests
            .Where(x => x.UserId == currentUserId)
            .CountAsync();
        return new PaginatedData<RepairRequestEntity>(data, totalCount);
    }

    public async Task<RepairRequestEntity?> GetByIdWithAllIncludesAsync(Guid requestId)
    {
        return await _dbContext.RepairRequests
            .AsNoTracking()
            .Include(r => r.User)
            .Include(r => r.EquipmentType)
            .Include(r => r.ProblemType)
            .Include(r => r.RepairStatus)
            .SingleOrDefaultAsync(x => x.Id == requestId);
    }

    public async Task CreateAsync(RepairRequestEntity requestEntity)
    {
        await _dbContext.RepairRequests.AddAsync(requestEntity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(RepairRequestEntity requestRepair)
    {
        _dbContext.RepairRequests.Update(requestRepair);
        await _dbContext.SaveChangesAsync();
    }
}