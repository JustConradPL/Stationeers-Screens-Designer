
using System.Windows;
using System.Windows.Markup;

namespace StationeersScreensDesigner.Generated
{
    /// <summary>
    /// Auto-generated. Holds convention-based ViewModel→View DataTemplates.
    /// </summary>
    public static class GeneratedViewMappings
    {
        private static ResourceDictionary? _dictionary;

        /// <summary>
        /// ResourceDictionary containing DataTemplates for { StationeersScreensDesigner.ViewModels.*ViewModel ↔ StationeersScreensDesigner.Views.*View }.
        /// </summary>
        public static ResourceDictionary Dictionary => _dictionary ??=
            (ResourceDictionary)XamlReader.Parse(@"<!-- Auto-generated -->
<ResourceDictionary xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
                    xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""
                   xmlns:vm=""clr-namespace:StationeersScreensDesigner.ViewModels;assembly=StationeersScreensDesigner""
                   xmlns:view=""clr-namespace:StationeersScreensDesigner.Views;assembly=StationeersScreensDesigner"">

    <DataTemplate DataType=""{x:Type vm:MainViewModel}"">
        <view:MainView />
    </DataTemplate>

</ResourceDictionary>
");
    }
}
