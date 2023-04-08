using System.Runtime.Caching;

namespace InMemoryCachingWebApi.Services;

public class CacheService : ICacheService
{
    private ObjectCache _memoryCache = MemoryCache.Default;

    public T GetData<T>(string key)
        => (T)this._memoryCache.Get(key);

    public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
    {
        if (string.IsNullOrEmpty(key))
        {
            return false;
        }

        this._memoryCache.Set(key, value, expirationTime);

        return true;
    }

    public bool RemoveData(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            return false;
        }

        this.RemoveData(key);

        return true;
    }
}
