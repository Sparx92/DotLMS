using System.Collections.Specialized;
using System.Runtime.Caching;
using Bytes2you.Validation;
using DotLms.Services.Providers.Contracts;

namespace DotLms.Services.Providers
{
    public class MemoryCacheProvider : IMemoryCacheProvider
    {
        private MemoryCache memoryCache;

        public MemoryCacheProvider(MemoryCache memoryCache)
        {
            Guard.WhenArgument(memoryCache,nameof(memoryCache)).IsNull().Throw();

            this.memoryCache = memoryCache;
        }

        public MemoryCache MemoryCache
        {
            get { return this.memoryCache; }
        }
    }
}