namespace Client.Infrastructure.Managers.EquipmentTypeManager;

public class EquipmentTypeManager(IHttpClientFactory factory) 
    : IEquipmentTypeManager
{
    private readonly IHttpClientFactory _factory = factory;
    public async Task<IResult<IEnumerable<EquipmentTypeResponse>>> GetAllAsync()
    {
        var response = await _factory.CreateClient("Client").GetAsync(EquipmentTypeRoutes.GetAll);
        var result = await response.ToResult<IEnumerable<EquipmentTypeResponse>>();
        return result;
    }

    public async Task<IResult<EquipmentTypeResponse>> GetByIdAsync(string equipmentTypeId)
    {
        var response = await _factory.CreateClient("Client").GetAsync(EquipmentTypeRoutes.GetById(equipmentTypeId));
        var result = await response.ToResult<EquipmentTypeResponse>();
        return result;
    }

    public async Task<IResult> CreateAsync(EquipmentTypeRequest request)
    {
        var response = await _factory.CreateClient("Client").PostAsJsonAsync(EquipmentTypeRoutes.Create, request);
        var result = await response.ToResult();
        return result;
    }

    public async Task<IResult> UpdateAsync(string equipmentTypeId, EquipmentTypeRequest request)
    {
        var response = await _factory.CreateClient("Client").PutAsJsonAsync(EquipmentTypeRoutes.Update(equipmentTypeId), request);
        var result = await response.ToResult();
        return result;
    }

    public async Task<IResult> DeleteAsync(string equipmentTypeId)
    {
        var response = await _factory.CreateClient("Client").DeleteAsync(EquipmentTypeRoutes.Delete(equipmentTypeId));
        var result = await response.ToResult();
        return result;
    }
}