namespace Client.Infrastructure.Managers.ProblemTypeManager;

public class ProblemTypeManager(IHttpClientFactory factory) 
    : IProblemTypeManager
{
    private readonly IHttpClientFactory _factory = factory;
    public async Task<IResult<IEnumerable<ProblemTypeResponse>>> GetAllAsync(string equipmentTypeId)
    {
        var response = await _factory.CreateClient("Client").GetAsync(ProblemTypeRoutes.GetAllByEquipmentTypeId(equipmentTypeId));
        var result = await response.ToResult<IEnumerable<ProblemTypeResponse>>();
        return result;
    }

    public async Task<IResult<ProblemTypeResponse>> GetByIdAsync(string problemTypeId)
    {
        var response = await _factory.CreateClient("Client").GetAsync(ProblemTypeRoutes.GetById(problemTypeId));
        var result = await response.ToResult<ProblemTypeResponse>();
        return result;
    }

    public async Task<IResult> CreateAsync(ProblemTypeRequest request)
    {
        var response = await _factory.CreateClient("Client").PostAsJsonAsync(ProblemTypeRoutes.Create, request);
        var result = await response.ToResult();
        return result;
    }

    public async Task<IResult> UpdateAsync(string problemId, ProblemTypeRequest request)
    {
        var response = await _factory.CreateClient("Client").PutAsJsonAsync(ProblemTypeRoutes.Update(problemId), request);
        var result = await response.ToResult();
        return result;
    }

    public async Task<IResult> DeleteAsync(string problemTypeId)
    {
        var response = await _factory.CreateClient("Client").DeleteAsync(ProblemTypeRoutes.Delete(problemTypeId));
        var result = await response.ToResult();
        return result;
    }
}