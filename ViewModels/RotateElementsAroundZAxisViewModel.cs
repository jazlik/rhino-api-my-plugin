using Rhino;
using Rhino.UI;
using RhinoGeometryPlugin.Classes;

namespace RhinoGeometryPlugin.ViewModels
{
    public class RotateElementsAroundZAxisViewModel : ViewModelBase
    {
        public RotateElementsAroundZAxisViewModel()
        {
            Rhino.UI.Panels.Show += OnShowPanel;
        }

        public RotateElementsAroundZAxisModel RotateElementsAroundZAxisModel { get; set; } = new RotateElementsAroundZAxisModel();

        #region Binding Properties
        //View is binded to ViewModel using UserControl.DatContext in View
        //Each variable with Binding Path in View, should be in Binding Properties here

        private float rotationAngle;
        public float RotationAngle
        {
            get => rotationAngle;
            set
            {
                rotationAngle = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public void RotateElementsByGivenAngle()
        {
            RotateElementsAroundZAxisModel.RotateElementsAroundZAxis(RotationAngle);
        }
    }
}
