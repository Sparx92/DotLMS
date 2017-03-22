using System.Runtime.Caching;

namespace DotLms.Services.Providers.Contracts
{
    public interface IMemoryCacheProvider
    {
        MemoryCache MemoryCache { get; }
    }
}