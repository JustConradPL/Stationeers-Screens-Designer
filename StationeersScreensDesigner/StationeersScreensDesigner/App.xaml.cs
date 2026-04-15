using System.Configuration;
using System.Data;
using System.Windows;

using CodeParagon.Wpf.MVVM.Navigation;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Win32;

using StationeersScreensDesigner.Models;
using StationeersScreensDesigner.ViewModels;
using StationeersScreensDesigner.Views;

namespace StationeersScreensDesigner
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _provider;

        public App()
        {
            var serviceCollection = new ServiceCollection();

            #region registering navigation

            var register = new NavigationServiceRegister()

                .RegisterWindow<MainView, MainViewModel>()

                .RegisterViewModel<MainViewModel>()

                .RegisterNavigationService();

            serviceCollection.Add(register.Services);

            #endregion

            serviceCollection.AddSingleton<IEnumerable<LuaUIElement>>(LuaUIElementsCollection.All);

            _provider = serviceCollection.BuildServiceProvider();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            #region Display Main Window
            var mainWin = _provider.GetRequiredService<MainView>();
            Current.MainWindow = mainWin;
            mainWin.Show();
            #endregion

            //Auto viewmodel -> model mappings
            Resources.MergedDictionaries.Add(StationeersScreensDesigner.Generated.GeneratedViewMappings.Dictionary);

        }
    }

}
