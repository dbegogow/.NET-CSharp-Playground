using System.Text.Json;
using System.Net.Http.Json;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Hybrid;

public record Post(int UserId, int Id, string Title, string Body);

public class PostsService(
    IHttpClientFactory httpClientFactory,
    IMemoryCache memoryCache,
    IDistributedCache distributedCache,
    HybridCache hybridCache)
{
    public async Task<List<Post>?> GetUserPostsAsync(string userId)
    {
        var cacheKey = $"posts_{userId}";

        // Before (Memory Chache)
        var posts = await memoryCache.GetOrCreateAsync(
            cacheKey,
            async _ => await GetPostsAsync(userId));

        // Before (Distributed Chache)
        var postsJson = await distributedCache.GetStringAsync(cacheKey);

        if (postsJson is null)
        {
            posts = await GetPostsAsync(userId);

            await distributedCache.SetStringAsync(
                cacheKey,
                JsonSerializer.Serialize(posts));
        }
        else
        {
            posts = JsonSerializer.Deserialize<List<Post>>(postsJson);
        }

        // .NET 9 Hybrid Cache
        posts = await hybridCache.GetOrCreateAsync(
            cacheKey,
            async _ => await GetPostsAsync(userId), new HybridCacheEntryOptions()
            {
                Flags = HybridCacheEntryFlags.DisableLocalCache | // Act as distributed cache
                        HybridCacheEntryFlags.DisableDistributedCache // Act as local cache
            });

        return posts;
    }

    private async Task<List<Post>?> GetPostsAsync(string userId)
    {
        Console.WriteLine("===========Fetching posts from API");

        var url = $"https://jsonplaceholder.typicode.com/posts?userId={userId}";

        var client = httpClientFactory.CreateClient();

        var response = await client.GetAsync(url);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<List<Post>>();
    }
}