using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using System.Windows;

namespace CastleTest
{
    public class Bootstrapper : PrismBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<Interceptor.InterceptorModule>();
            moduleCatalog.AddModule<AsyncInterceptor.AsyncInterceptorModule>();
        }
    }
}
