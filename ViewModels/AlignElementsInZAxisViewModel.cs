using Rhino;
using Rhino.UI;
using RhinoGeometryPlugin.Classes;

namespace RhinoGeometryPlugin.ViewModels
{
    public class AlignElementsInZAxisViewModel : ViewModelBase
    {

        public AlignElementsInZAxisViewModel()
        {
            Rhino.UI.Panels.Show += OnShowPanel;
        }

        public AlignElementsInZAxisModel AlignElementsInZAxisModel { get; set; } = new AlignElementsInZAxisModel();

        #region Binding Properties
        //View is binded to ViewModel using UserControl.DatContext in View
        //Each variable with Binding Path in View, should be in Binding Properties here

        private float alignHeight = 0;
        public float AlignHeight
        {
            get => alignHeight;
            set
            {
                alignHeight = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public void AlignElementsToGivenHeight()
        {
            AlignElementsInZAxisModel.AlignElementsInZAxis(AlignHeight);
        }
    }
}
