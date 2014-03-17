using System.Configuration;
using BusConductor.Application.Core;
using BusConductor.Data.Common;
using Lucidity.Utilities.Logging;
using StructureMap.Configuration.DSL;
using Lucidity.Utilities.Contracts.Logging;

namespace BusConductor.UI.IntegrationTests.Core
{
    public class TestRegistry : Registry
    {
        public TestRegistry()
        {
            Configure(x =>
                      {
                          x.ImportRegistry(typeof(ApplicationRegistry));
                          For<IContextProvider>().Singleton().Use<GenericContextProvider>();
                          For<ILog>().Use<SqlLog>().Ctor<string>("connectionString").Is(ConfigurationManager.ConnectionStrings["BusConductor"].ConnectionString);
                      });

        }
    }
}
