using System;

namespace DotLms.Services.Providers.Contracts
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow();
    }
}