using DirectoryCopier.Presentation.Wpf.Views;
using Ninject;
using Ninject.Extensions.Conventions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DirectoryCopier.Presentation.Wpf
{
    public partial class App : Application
    {
        private StandardKernel CreateContainer()
        {
            var container = new StandardKernel();

            container.Bind(configurator => configurator
            .From("DirectoryCopier.Presentation.Wpf", "DirectoryCopier.Domain")
            .SelectAllClasses()
            .BindAllInterfaces()
            );

            return container;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var container = CreateContainer();
            MainView view = container.Get<MainView>();

            view.Show();
        }
    }
}
