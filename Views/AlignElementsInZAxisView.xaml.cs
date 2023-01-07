using System;
using System.Windows;
using System.Windows.Controls;
using RhinoGeometryPlugin.ViewModels;
using Rhino;

namespace RhinoGeometryPlugin.Views
{
    /// <summary>
    /// Interaction logic for AlignElementsInZAxisView.xaml
    /// </summary>
    public partial class AlignElementsInZAxisView : UserControl
    {
        public AlignElementsInZAxisView()
        {
            DataContext = new AlignElementsInZAxisViewModel();
            InitializeComponent();
        }

        private AlignElementsInZAxisViewModel ViewModel
        {
            get { return DataContext as AlignElementsInZAxisViewModel; }
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            var vm = ViewModel;
            if (vm == null) return;
            vm.AlignElementsToGivenHeight();
        }

        // IsVisibleChanged="Control_VisibleChanged" event in View
        private void Control_VisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                // Visible code here
                RhinoApp.WriteLine("Panel now visible");
            }
            else
            {
                // Hidden code here
                RhinoApp.WriteLine("Panel now hidden");
            }
        }
    }

}
