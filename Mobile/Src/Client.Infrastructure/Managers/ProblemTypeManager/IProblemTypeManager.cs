namespace Client.Infrastructure.Managers.ProblemTypeManager;

public interface IProblemTypeManager
{
    Task<IResult<IEnumerable<ProblemTypeResponse>>> GetAllAsync(string equipmentTypeId);
    Task<IResult<ProblemTypeResponse>> GetByIdAsync(string problemTypeId);
    Task<IResult> CreateAsync(ProblemTypeRequest request);
    Task<IResult> UpdateAsync(string problemId, ProblemTypeRequest request);
    Task<IResult> DeleteAsync(string problemTypeId);
}