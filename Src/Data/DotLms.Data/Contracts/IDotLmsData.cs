using System;

namespace DotLms.Data.Contracts
{
    public interface IDotLmsData : IDisposable
    {
        void Commit();
    }
}