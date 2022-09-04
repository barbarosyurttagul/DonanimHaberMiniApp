using System;
namespace DH.Core.CrossCuttingConcerns.Caching
{
	public interface ICacheManager
	{
        T Get<T>(string key);
        object Get(string key);
        T GetOrAdd<T>(string key, Func<T> action) where T : class;
        void Add(string key, string value, int duration);
        bool IsAdd(string key);
        void Remove(string key);
        void RemoveByPattern(string pattern);
    }
}

