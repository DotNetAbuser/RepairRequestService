namespace Shared.Responses.Identity;

public class UserRolesResponse
{
    public List<UserRoleModel> UserRoles { get; set; } = [];
}

public class UserRoleModel
{
    public string RoleName { get; set; } = string.Empty;
    public bool Selected { get; set; }
}