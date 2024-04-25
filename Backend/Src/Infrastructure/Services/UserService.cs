using System.Collections;

namespace Infrastructure.Services;

public class UserService(IUserRepository userRepository,
    IPasswordHasher passwordHasher, IRoleRepository roleRepository)
    : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly IRoleRepository _roleRepository = roleRepository;
    
    public async Task<Result> RegisterUserAsync(RegisterRequest request)
    {
        var userWithSameEmail = await _userRepository
            .GetUserByEmailAsync(request.Email.ToLower());
        if (userWithSameEmail != null)
        {
            return await Result<string>
                .FailAsync("Пользователь с такой же почтой уже существует!");
        }

        var userEntity = new UserEntity
        {   
            Id = Guid.NewGuid(),
            RoleId = (int)Role.Guest,
            LastName = request.LastName,
            FirstName = request.FirstName,
            MiddleName = request.MiddleName,
            Email = request.Email.ToLower(),
            PasswordHash = _passwordHasher.Generate(request.Password),
            CreatedOn = DateTime.UtcNow,
            IsActive = request.ActivateUser
        };
        
        await _userRepository.CreateAsync(userEntity);
        return await Result<string>
            .SuccessAsync("Пользователь успешно зарегестрирован!");
    }

    public async Task<Result<PaginatedData<UserResponse>>> GetPaginatedUsersAsync(int pageNumber, int pageSize,
        string searchTerm, string sortColumn, string sortOrder)
    {
        var paginatedUserEntity = await _userRepository.GetPaginatedUsersAsync(pageNumber, pageSize,
            searchTerm, sortColumn, sortOrder);
        var userResponseList = new List<UserResponse>();
        foreach (var userEntity in paginatedUserEntity.Data)
        {
            userResponseList.Add(
                new()
                {
                    Id = userEntity.Id.ToString(),
                    LastName = userEntity.LastName,
                    FirstName = userEntity.FirstName,
                    MiddleName = userEntity.MiddleName,
                    Email = userEntity.Email,
                    IsActive = userEntity.IsActive
                });
        }
        var paginatedUserResponse =
            new PaginatedData<UserResponse>(data: userResponseList, totalCount: paginatedUserEntity.TotalCount);
        return await Result<PaginatedData<UserResponse>>
            .SuccessAsync(paginatedUserResponse, "Пользователи успешно получены!");
    }

    public async Task<Result<UserResponse>> GetByIdAsync(string userId)
    {
        var userEntity = await _userRepository.GetByUserIdAsync(Guid.Parse(userId));
        if (userEntity == null)
        {
            return await Result<UserResponse>
                .FailAsync("Пользователь не найден!");
        }
        var userResponse = new UserResponse
        {
            Id = userEntity.Id.ToString(),
            LastName = userEntity.LastName,
            FirstName = userEntity.FirstName,
            MiddleName = userEntity.MiddleName,
            Email = userEntity.Email,
            IsActive = userEntity.IsActive
        };
        return await Result<UserResponse>
            .SuccessAsync(userResponse, "Пользователь успешно получен.");
    }

    public async Task<Result> ToggleUserStatusAsync(ToggleUserStatusRequest request)
    {
        var userEntity = await _userRepository
            .GetByUserIdAsync(Guid.Parse(request.UserId));
        if (userEntity == null)
        {
            return await Result<string>
                .FailAsync("Пользователь не найден!");
        }

        userEntity.IsActive = request.ActivateUser;
        userEntity.UpdatedOn = DateTime.UtcNow;
        await _userRepository.UpdateAsync(userEntity);
        return await Result<string>
            .SuccessAsync("Статус пользователя успешно изменён.");
    }

    public async Task<Result<UserRolesResponse>> GetRolesByUserIdAsync(string userId)
    {
        var UserRolesList = new List<UserRoleModel>();
        var rolesEntities = await _roleRepository.GetAllAsync();
        var user = await _userRepository
            .GetByUserIdWithRoleIncludeAsync(Guid.Parse(userId));

        foreach (var roleEntity in rolesEntities)
        {
            var userRole = new UserRoleModel
            {
                RoleName = roleEntity.Name,
            };
            userRole.Selected = roleEntity.Name == user.Role.Name;
            UserRolesList.Add(userRole);
        }

        var response = new UserRolesResponse{ UserRoles = UserRolesList };
        return await Result<UserRolesResponse>
            .SuccessAsync(response, "Роли пользователя успешно получены!");
    }

    public async Task<Result> UpdateRoleAsync(string userId, UpdateUserRoleRequest request, string currentUserId)
    {
        var user = await _userRepository
            .GetByUserIdAsync(Guid.Parse(userId));
        var isDefaultUser = user.Email == "admin@example.com" ||
                            user.Email == "worker@example.com" ||
                            user.Email == "guest@example.com";
        if (isDefaultUser)
        {
            return await Result<string>
                .FailAsync("Это действие невозможно для этого пользователя");
        }
        var selectedRole = request.UserRoles;
        var role = await _roleRepository.GetByNameAsync(selectedRole.RoleName);
        user.RoleId = role.Id;
        user.UpdatedOn = DateTime.UtcNow;
        await _userRepository.UpdateAsync(user);
        return await Result<string>
            .SuccessAsync("Роли пользователя успешно обновлены!");
    }
    
}