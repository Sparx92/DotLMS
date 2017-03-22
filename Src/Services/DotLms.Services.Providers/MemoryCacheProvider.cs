using System.Collections.Specialized;
using System.Runtime.Caching;
using DotLms.Services.Providers.Contracts;

namespace DotLms.Services.Providers
{
    public class MemoryCacheProvider : IMemoryCacheProvider
    {
        private MemoryCache memoryCache;

        public MemoryCacheProvider(MemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public MemoryCache MemoryCache
        {
            get { return this.memoryCache; }
        }
    }
}