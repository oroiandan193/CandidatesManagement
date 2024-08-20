
using Microsoft.Extensions.Caching.Distributed;

namespace CandidatesManagement.Infrastructure.Cache
{
    internal class CacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;

        public CacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
        {
            var cachedValue = await _distributedCache.GetAsync(key, cancellationToken);

            if (cachedValue == null) 
            {
                return null;
            }

            var deserialized = System.Text.Json.JsonSerializer.Deserialize<T>(cachedValue);

            return deserialized;
        }

        public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            await _distributedCache.RemoveAsync(key, cancellationToken);
        }

        public async Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default) where T : class
        {
            var serialized = System.Text.Json.JsonSerializer.Serialize(value);

            if (serialized is null)
                return;

            await _distributedCache.SetStringAsync(key, serialized, cancellationToken);
        }
    }
}
