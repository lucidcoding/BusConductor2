using BusConductor.Application.Core;
using BusConductor.Data.Common;
using Lucidity.Utilities.Contracts.Logging;
using Lucidity.Utilities.Logging;
using StructureMap.Configuration.DSL;

namespace BusConductor.Admin.UI.Core
{
    public class UiRegistry : Registry
    {
        public UiRegistry()
        {
            Configure(x =>
            {
                x.ImportRegistry(typeof(ApplicationRegistry));
                For<IContextProvider>().Singleton().Use<GenericContextProvider>();
                For<ILog>().Use<StubLog>();
            });
        }
    }
}
