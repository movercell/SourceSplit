using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using LiveSplit.Portal2Split;
using LiveSplit.UI.Components;
using System;
using LiveSplit.Model;

[assembly: ComponentFactory(typeof(Portal2SplitFactory))]

namespace LiveSplit.Portal2Split
{
    public class Portal2SplitFactory : IComponentFactory
    {
        private Portal2SplitComponent _instance;

        public string ComponentName => "Portal2Split";
        public string Description => "Game Time / Auto-splitting for Portal 2 and it's beta builds.";
        public ComponentCategory Category => ComponentCategory.Control;

        public IComponent Create(LiveSplitState state)
        {
            // hack to prevent double loading
            string caller = new StackFrame(1).GetMethod().Name;
            bool createAsLayoutComponent = (caller == "LoadLayoutComponent" || caller == "AddComponent");

            // if component is already loaded somewhere else
            if (_instance != null && !_instance.Disposed)
            {
                MessageBox.Show(
                    "Portal2Split is already loaded in the " +
                        (_instance.IsLayoutComponent ? "Layout Editor" : "Splits Editor") + "!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);

                throw new Exception("Component already loaded.");
            }

            return (_instance = new Portal2SplitComponent(state, createAsLayoutComponent));
        }

        public string UpdateName => this.ComponentName;
        public string UpdateURL => null;
        public Version Version => Assembly.GetExecutingAssembly().GetName().Version;
        public string XMLURL => null;
    }
}
