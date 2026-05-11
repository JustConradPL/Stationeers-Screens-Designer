using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StationeersScreensDesigner.Controls
{
    /// <summary>
    /// Logika interakcji dla klasy SizeTextBox.xaml
    /// </summary>
    public partial class SizeTextBox : UserControl
    {
        public SizeTextBox()
        {
            InitializeComponent();
        }
        public string ValueWidth
        {
            get { return (string)GetValue(ValueWidthProperty); }
            set { SetValue(ValueWidthProperty, value); }
        }

        public static readonly DependencyProperty ValueWidthProperty =
            DependencyProperty.Register("ValueWidth", typeof(string), typeof(SizeTextBox), new FrameworkPropertyMetadata("860",FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string ValueHeight
        {
            get { return (string)GetValue(ValueHeightProperty); }
            set { SetValue(ValueHeightProperty, value); }
        }

        public static readonly DependencyProperty ValueHeightProperty =
            DependencyProperty.Register("ValueHeight", typeof(string), typeof(SizeTextBox), new FrameworkPropertyMetadata("585",FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
    }
}
