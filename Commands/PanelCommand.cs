using System;
using System.Runtime.InteropServices;
using Rhino;
using Rhino.Commands;
using Rhino.UI;
using RhinoGeometryPlugin.Views;

namespace RhinoGeometryPlugin.Commands
{
    [Guid("0874C691-A083-4C05-9E31-96C2C63F7C7E")] // Guid jest potrzebny żeby Panel się wyświetlał w Rhino
    public class PanelHost : RhinoWindows.Controls.WpfElementHost
    {
        public PanelHost() : base(new PanelMainView(), null)
        {
        }
    }

    public class PanelCommand : Command
    {
        public PanelCommand Instance { get; private set; }

        // public static
        public PanelCommand()
        {
            Instance = this;
            Panels.RegisterPanel(
              RhinoGeometryPlugin.Instance,
              typeof(PanelHost),
              "Panel",
              System.Drawing.SystemIcons.WinLogo,
              PanelType.System
              );
        }

        public override string EnglishName => "Panel";

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            return Result.Success;
        }
    }
}
