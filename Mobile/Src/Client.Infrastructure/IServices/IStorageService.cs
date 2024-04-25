namespace Client.Infrastructure.IServices;

public interface IStorageService
{
    Task SetItemAsStringAsync(string key, string value);
    Task<string> GetItemAsStringAsync(string key);
    Task RemoveItemAsync(string key);
}