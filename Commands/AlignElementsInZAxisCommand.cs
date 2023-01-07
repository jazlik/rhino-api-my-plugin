using System;
using System.Collections.Generic;
using Rhino;
using Rhino.Commands;
using Rhino.DocObjects;
using Rhino.Geometry;
using Rhino.Input;
using Rhino.Input.Custom;

namespace RhinoGeometryPlugin.Commands
{
    public class AlignElementsInZAxisCommand : Command
    {
        public AlignElementsInZAxisCommand()
        {
            Instance = this;
        }

        ///<summary>The only instance of the AlignElementsInZAxisCommand command.</summary>
        public static AlignElementsInZAxisCommand Instance { get; private set; }

        public override string EnglishName => "AlignElementsInZAxis";

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            const ObjectType geometryFilter = ObjectType.Surface | ObjectType.PolysrfFilter | ObjectType.Mesh;
            int integer1 = 100;
            OptionInteger optionInteger1 = new OptionInteger(integer1, 200, 900);
            List<Brep> breps = new List<Brep>();

            GetObject go = new GetObject();
            go.SetCommandPrompt("Select surfaces, polysurfaces or meshes. They will be scaled in Z axis!");
            go.GeometryFilter = geometryFilter;
            go.AddOptionInteger("scaleInZAxisFactor", ref optionInteger1);

            go.GroupSelect = true;
            go.SubObjectSelect = false;
            go.EnableClearObjectsOnEntry(false);
            go.EnableUnselectObjectsOnExit(false);
            go.DeselectAllBeforePostSelect = false;

            bool bHavePreselectedObjects = false;

            for (; ; )
            {
                GetResult res = go.GetMultiple(1, 0);

                if (res == GetResult.Option)
                {
                    go.EnablePreSelect(false, true);
                    continue;
                }

                else if (res != GetResult.Object)
                {
                    return Result.Cancel;
                }

                if (go.ObjectsWerePreselected)
                {
                    bHavePreselectedObjects = true;
                    go.EnablePreSelect(false, true);
                    continue;
                }

                break;
            }

            foreach (var obj in go.Objects())
            {
                var brep = obj.Brep();
                if (null == brep) return Result.Failure;
                double targetHeight = optionInteger1.CurrentValue;
                RhinoApp.WriteLine($"Target height is: {targetHeight}.");
                BoundingBox bBox = brep.GetBoundingBox(true);
                double currentHeight = GetHeight(brep);
                RhinoApp.WriteLine($"Current height is: {currentHeight}.");
                Point3d pointOrigin = new Point3d(bBox.Center.X, bBox.Center.Y, bBox.Min.Z);
                Transform transformationMatrix = Transform.Scale(new Plane(pointOrigin, Vector3d.ZAxis), 1, 1, targetHeight / currentHeight);
                doc.Objects.Transform(obj, transformationMatrix, true);
            }

            if (bHavePreselectedObjects)
            {
                for (int i = 0; i < go.ObjectCount; i++)
                {
                    RhinoObject rhinoObject = go.Object(i).Object();
                    if (null != rhinoObject)
                    {
                        rhinoObject.Select(false);
                    }
                }
                doc.Views.Redraw();
            }

            RhinoApp.WriteLine($"Value of optionInteger1 = {optionInteger1.CurrentValue}.");
            RhinoApp.WriteLine($"Object count: {go.ObjectCount}.");

            doc.Views.Redraw();
            return Result.Success;
        }

        #region Methods

        private double GetHeight(Brep input)
        {
            return input.GetBoundingBox(true).Diagonal.Z;
        }

        #endregion
    }
}
