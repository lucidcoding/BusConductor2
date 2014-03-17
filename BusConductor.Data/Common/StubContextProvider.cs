using BusConductor.Data.Core;

namespace BusConductor.Data.Common
{
    public class StubContextProvider : IContextProvider
    {
        public Context GetContext()
        {
            return null;
        }

        public void Dispose()
        {
        }

        public void SaveChanges()
        {
        }
    }
}
