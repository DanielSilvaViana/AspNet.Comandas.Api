using Comandas.Services.Interfaces;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Comandas.Services
{
    public class RedisService : IRedisService
    {
        private readonly IDatabase _database;
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public RedisService(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
            _database = connectionMultiplexer.GetDatabase();
        }
        public async Task<T> GetAsync<T>(string key)
        {
            var data = await _database.StringGetAsync(key);
            if(!data.HasValue)
            {
                return default!;
            }
            return JsonConvert.DeserializeObject<T>(data);
        }

        public async Task<bool> KeyExistsAsync(string key)
        {
            return await _database.KeyExistsAsync(key);
        }

        public async Task<bool> RemoveAsync(string key)
        {
           return await _database.KeyDeleteAsync(key);
        }

        public async Task<bool> SetAsync<T>(string key, T data, TimeSpan? ttl)
        {
            var json = JsonConvert.SerializeObject(data);
            return await _database.StringSetAsync(key, json, ttl);
        }
    }
}
