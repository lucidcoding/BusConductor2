using System;
using BusConductor.Data.Core;

namespace BusConductor.Data.Common
{
    public interface IContextProvider : IDisposable
    {
        Context GetContext();
        void SaveChanges();
    }
}
