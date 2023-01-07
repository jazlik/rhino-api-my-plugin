using System;
using Rhino;
using Rhino.Geometry;
using Rhino.Input;
using Rhino.Input.Custom;

namespace RhinoGeometryPlugin.Classes
{
    public class AlignElementsInZAxisModel : ModelBase
    {
        public AlignElementsInZAxisModel() {; }

        public void AlignElementsInZAxis(float targetHeight)
        {
            if (targetHeight > 0)
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
                        double currentHeight = GetHeight(brep);
                        Point3d pointOrigin = new Point3d(bBox.Center.X, bBox.Center.Y, bBox.Min.Z);
                        Transform transformationMatrix = Transform.Scale(new Plane(pointOrigin, Vector3d.ZAxis), 1, 1, targetHeight / currentHeight);
                        doc.Objects.Transform(obj, transformationMatrix, true);
                    }

                    RhinoApp.WriteLine($"Elements were aligned to height: {targetHeight}");

                }

                else RhinoApp.WriteLine("Select elements to be aligned! Selected count = 0");
            }

            else RhinoApp.WriteLine("Value must be greater than 0!");
        }
    }
}
