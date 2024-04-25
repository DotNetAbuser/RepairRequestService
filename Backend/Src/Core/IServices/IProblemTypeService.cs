namespace Core.IServices;

public interface IProblemTypeService
{
    Task<Result<IEnumerable<ProblemTypeResponse>>> GetAllAsync(string equipmentTypeId);
    Task<Result<ProblemTypeResponse>> GetByIdAsync(string problemTypeId);
    Task<Result> CreateAsync(ProblemTypeRequest request);
    Task<Result> UpdateAsync(string problemId, ProblemTypeRequest request);
    Task<Result> DeleteAsync(string problemTypeId);
}