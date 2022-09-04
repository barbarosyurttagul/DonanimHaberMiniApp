using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using StackExchange.Redis;

namespace DH.Core.CrossCuttingConcerns.Caching.Redis
{
	public class RedisCacheManager : ICacheManager
	{
        private readonly IConnectionMultiplexer _rediscon;
        private readonly IDatabase _cache;
        private TimeSpan ExpireTime => TimeSpan.FromDays(1);

        
        public RedisCacheManager(IConnectionMultiplexer rediscon)
		{
            _rediscon = rediscon;
            _cache = rediscon.GetDatabase();
		}

        public void Add(string key, string value, int duration)
        {
            _cache.StringSet(key, value, ExpireTime);
        }

        public T Get<T>(string key)
        {
            throw new NotImplementedException();
        }

        public object Get(string key)
        {
            return _cache.StringGet(key);
        }

        public bool IsAdd(string key)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            _cache.KeyDelete(key);
        }

        public void RemoveByPattern(string pattern)
        {
            throw new NotImplementedException();
        }

        public T GetOrAdd<T>(string key, Func<T> action) where T : class
        {
            var result = _cache.StringGet(key);
            if (result.IsNull)
            {
                result = JsonSerializer.Serialize(action());
                _cache.StringSet(key, result, ExpireTime);
            }
            return JsonSerializer.Deserialize<T>(result);
        }
    }
}

