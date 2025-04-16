using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comandas.Services.Interfaces
{
    public interface IRedisService
    {
        //Set;get;remove;Keyexists

        Task<bool> SetAsync<T>(string key, T data, TimeSpan? ttl);
        Task<T> GetAsync<T>(string key);
        Task<bool> RemoveAsync(string key);
        Task<bool> KeyExistsAsync(string key);
    }
}
