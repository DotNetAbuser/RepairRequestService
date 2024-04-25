namespace Client.Services;

public class StorageService
    : IStorageService
{
    public async Task SetItemAsStringAsync(string key, string value) =>
        await SecureStorage.SetAsync(key, value);
    public async Task<string> GetItemAsStringAsync(string key) =>
        await SecureStorage.GetAsync(key);
    public async Task RemoveItemAsync(string key) =>
        SecureStorage.Remove(key);
}