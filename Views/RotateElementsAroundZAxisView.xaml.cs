using System;
using System.Windows;
using System.Windows.Controls;
using RhinoGeometryPlugin.ViewModels;

namespace RhinoGeometryPlugin.Views
{
    /// <summary>
    /// Interaction logic for RotateElementsAroundZAxisView.xaml
    /// </summary>
    public partial class RotateElementsAroundZAxisView : UserControl
    {
        public RotateElementsAroundZAxisView()
        {
            DataContext = new RotateElementsAroundZAxisViewModel();
            InitializeComponent();
        }

        private RotateElementsAroundZAxisViewModel ViewModel
        {
            get { return DataContext as RotateElementsAroundZAxisViewModel; }
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            var vm = ViewModel;
            if (vm == null) return;
            vm.RotateElementsByGivenAngle();
        }
    }
}
