using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Rhino.DocObjects;
using Rhino.Geometry;
using Rhino.Input.Custom;

namespace RhinoGeometryPlugin.Classes
{
    public class ModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public GetObject GetSelection()
        {
            const ObjectType geometryFilter = ObjectType.Surface | ObjectType.PolysrfFilter | ObjectType.Mesh;
            GetObject go = new GetObject();
            go.GeometryFilter = geometryFilter;
            go.GroupSelect = true;
            go.EnablePreSelect(true, false);
            go.EnablePostSelect(false);
            go.SubObjectSelect = false;
            go.EnableClearObjectsOnEntry(false);
            go.EnableUnselectObjectsOnExit(false);

            return go;
        }

        public double GetHeight(Brep input)
        {
            return input.GetBoundingBox(true).Diagonal.Z;
        }

        public double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }
    }
}
