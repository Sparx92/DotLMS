using System;
using DotLms.Services.Providers.Contracts;

namespace DotLms.Services.Providers
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow()
        {
            return DateTime.UtcNow;
        }
    }
}
