// VFX Control — custom Inspector host for VisualEffect.
//
// Hosts the shared VfxControl controller inside the Inspector instead of a window. Wins over the VFX
// package's stock inspector (non-Unity assembly takes precedence — see Documentation~/VfxControl.md).
// Drives the edited set from Editor.targets (no scene-selection tracking) and routes gizmos via the
// controller's scene-GUI hook.
//
// Per-tab popups: the component context menu (gear ▸ / right-click) has "VFX Control ▸ <Tab>" entries
// that open Unity's native dockable PropertyEditor (EditorUtility.OpenPropertyEditor) filtered to one
// tab — the inspector equivalent of the window's tab tear-off. The chosen tab is handed to the freshly
// created inspector via a static "pending solo tab" (consumed in OnEnable), then the controller's solo
// machinery (IsSolo → pin _tab + hide the tab strip) does the rest.

using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEngine.VFX;

namespace VfxControl.EditorTools
{
    [CustomEditor(typeof(VisualEffect))]
    [CanEditMultipleObjects]
    public sealed class VfxControlInspector : Editor, IVfxHost
    {
        private VfxControl _ctrl;
        private VisualElement _root;
        private string _soloTab; // non-null in a per-tab popup; null for the normal (full) inspector

        public VisualElement Root => _root;
        public bool IsInspector => true;
        public string SoloTab => _soloTab;
        public void OpenSolo(string tabId, string label) { } // no in-place tear-off; popups go through the context menu

        private void OnEnable()
        {
            // Consume the hand-off from a "VFX Control ▸ <Tab>" context command, if this editor is the one
            // it just opened. A normal main-Inspector editor sees null here → full inspector.
            _soloTab = s_pendingSoloTab;
            s_pendingSoloTab = null;
        }

        public override VisualElement CreateInspectorGUI()
        {
            _ctrl?.Disable();                    // guard against a re-bind creating a second controller
            _root = new VisualElement();
            _ctrl = new VfxControl(this);
            _ctrl.Enable();
            _ctrl.SetTargets(GetTargets());      // fixed targets, not scene selection
            _ctrl.Rebuild();
            return _root;
        }

        private void OnDisable()
        {
            _ctrl?.Disable();
            _ctrl = null;
        }

        private List<VisualEffect> GetTargets()
        {
            var list = new List<VisualEffect>();
            foreach (var t in targets)
                if (t is VisualEffect ve) list.Add(ve);
            return list;
        }

        // ---- per-tab dockable popups (component context menu) ----------------------------------------
        // The next VfxControlInspector created after one of these consumes it (see OnEnable).
        private static string s_pendingSoloTab;

        [MenuItem("CONTEXT/VisualEffect/VFX Control/Properties")] private static void OpenProps(MenuCommand c) => OpenTab(c, "props");
        [MenuItem("CONTEXT/VisualEffect/VFX Control/Playback")]   private static void OpenPlay(MenuCommand c) => OpenTab(c, "play");
        [MenuItem("CONTEXT/VisualEffect/VFX Control/Renderer")]   private static void OpenRender(MenuCommand c) => OpenTab(c, "render");
        [MenuItem("CONTEXT/VisualEffect/VFX Control/Debug")]      private static void OpenDebug(MenuCommand c) => OpenTab(c, "debug");

        private static void OpenTab(MenuCommand command, string tab)
        {
            if (command?.context == null) return;
            s_pendingSoloTab = tab;
            EditorUtility.OpenPropertyEditor(command.context); // native dockable PropertyEditor → our inspector
        }
    }
}
