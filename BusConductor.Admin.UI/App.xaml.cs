using System.Windows;
using BusConductor.Admin.UI.Core;
using StructureMap;

namespace BusConductor.Admin.UI
{
    public partial class App : System.Windows.Application
    {
        public App()
        {
            ObjectFactory.Container.Configure(x => x.AddRegistry<UiRegistry>());
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }
    }
}
