using System;
using System.Collections.Generic;
using System.Text;

using CodeParagon.Wpf.MVVM;
using CodeParagon.Wpf.MVVM.Navigation;

using Microsoft.Extensions.DependencyInjection;

namespace StationeersScreensDesigner.Helpers
{
    public static class NavigationHelper
    {
        public static readonly Action<IServiceProvider, Type> NavigateToViewModel = (p, vm) =>
        p.GetRequiredService<INavigationService>().NavigateTo(vm);

        public static void NavigateTo(this IServiceProvider serviceProvider, Type viewModelType)
        {
            serviceProvider.GetRequiredService<INavigationService>().NavigateTo(viewModelType);
        }

        public static void NavigateTo<T>(this IServiceProvider serviceProvider) where T : ViewModelBase
        {
            serviceProvider.GetRequiredService<INavigationService>().NavigateTo<T>();
        }
    }
}
