// VFX Control — custom Inspector host for VisualEffect (Phase 2, Increment 1).
//
// Hosts the shared VfxControl controller inside the Inspector instead of a window. Wins over the VFX
// package's stock inspector (non-Unity assembly takes precedence — see Documentation~/VfxControl.md).
// This increment shows the Properties tab only (the controller filters its tabs when IsInspector);
// other tabs + an inlined transport arrive in later increments. Drives the edited set from
// Editor.targets (no scene-selection tracking) and routes gizmos via the controller's scene-GUI hook.

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

        public VisualElement Root => _root;
        public bool IsInspector => true;
        public string SoloTab => null;           // no tear-off in the inspector
        public void OpenSolo(string tabId, string label) { }

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
    }
}
