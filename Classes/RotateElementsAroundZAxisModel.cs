using System;
using Rhino;
using Rhino.Geometry;
using Rhino.Input;
using Rhino.Input.Custom;

namespace RhinoGeometryPlugin.Classes
{
    public class RotateElementsAroundZAxisModel : ModelBase
    {
        public RotateElementsAroundZAxisModel() {; }

        public void RotateElementsAroundZAxis(float rotationAngle)
        {
            RhinoDoc doc = RhinoDoc.ActiveDoc;
            if (RhinoDoc.ActiveDoc == null) return;

            GetObject go = GetSelection();
            GetResult res = go.GetMultiple(1, 0);

            if (res == GetResult.Object)
            {
                foreach (var obj in go.Objects())
                {
                    var brep = obj.Brep();
                    if (null == brep) return;
                    BoundingBox bBox = brep.GetBoundingBox(true);
                    Point3d pointOrigin = new Point3d(bBox.Center.X, bBox.Center.Y, bBox.Min.Z);
                    Transform transformationMatrix = Transform.Rotation(ConvertToRadians(rotationAngle), pointOrigin);
                    doc.Objects.Transform(obj, transformationMatrix, true);
                }

                RhinoApp.WriteLine($"Elements were rotated by angle: {rotationAngle}°");

            }

            else RhinoApp.WriteLine("Select elements to be aligned! Selected count = 0");
        }
    }
}
