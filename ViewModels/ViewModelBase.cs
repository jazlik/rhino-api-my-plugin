using Rhino.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RhinoGeometryPlugin.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public ViewModelBase() {; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void OnShowPanel(object sender, ShowPanelEventArgs e)
        {
        }
    }
}
