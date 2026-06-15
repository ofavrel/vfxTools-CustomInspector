// VFX Control — the dockable EditorWindow host for the shared VfxControl controller.
//
// Owns the window-only concerns: the menu entries, tab tear-off (solo) + its [SerializeField]
// persistence across domain reloads, and forwarding the window lifecycle/selection to the controller.
// The UI itself lives in the host-agnostic VfxControl (see VfxControlWindow.*.cs partials).

using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;

namespace VfxControl.EditorTools
{
    public sealed class VfxControlWindow : EditorWindow, IVfxHost
    {
        // [SerializeField] so a torn-off window keeps its OWN pinned tab across a domain reload/restart
        // (Unity serializes a null string as ""; the controller's IsSolo treats null/empty as full).
        [SerializeField] private string _soloTab;
        private VfxControl _ctrl;

        public VisualElement Root => rootVisualElement;
        public bool IsInspector => false;
        public string SoloTab => _soloTab;

        public void OpenSolo(string tabId, string label)
        {
            var w = CreateWindow<VfxControlWindow>();
            w._soloTab = tabId;
            w.titleContent = new GUIContent("VFX · " + label);
            w.minSize = new Vector2(300, 300);
            w.Show();
            w._ctrl?.ApplyHostTabAndRebuild(); // pin to the torn-off tab + render lean
        }

        [MenuItem("Window/VFX Control")]
        public static void Open()
        {
            var w = GetWindow<VfxControlWindow>();
            // If the focused instance is a torn-off pop-out, restore it to a full window.
            if (!string.IsNullOrEmpty(w._soloTab)) { w._soloTab = null; w._ctrl?.Rebuild(); }
            w.titleContent = new GUIContent("VFX Control");
            w.minSize = new Vector2(320, 360);
            w.Show();
        }

        // Logs exactly where exposed-property enumeration succeeds or fails for the selected/target VFX.
        // Kept off the "Window/VFX Control" path — a MenuItem that is a prefix of another turns the
        // shorter one into a submenu and hides its command.
        [MenuItem("Tools/VFX Control/Diagnose Target")]
        private static void Diagnose()
        {
            var go = Selection.activeGameObject;
            var ve = go != null ? go.GetComponent<VisualEffect>() : Selection.activeObject as VisualEffect;
            var asset = ve != null ? ve.visualEffectAsset : Selection.activeObject as VisualEffectAsset;

            Debug.Log($"[VFX Control] Diagnose — component={(ve != null ? ve.name : "null")}, " +
                      $"persistent={(ve != null && EditorUtility.IsPersistent(ve))}, " +
                      $"asset={(asset != null ? asset.name : "null")}");
            Debug.Log($"[VFX Control] Binding: {VfxGraphReflection.DescribeBindingState()}");

            VfxGraphReflection.Verbose = true;
            try
            {
                var ps = VfxGraphReflection.GetExposedParameters(asset);
                Debug.Log($"[VFX Control] Enumerated {ps.Count} parameter(s): " +
                          string.Join(", ", ps.Select(p => $"{p.Name}[{p.SheetType}/{p.RealType}] cat='{p.Category}'")));

                var evts = VfxGraphReflection.GetEventNames(asset);
                Debug.Log($"[VFX Control] Custom events ({evts.Count}): {string.Join(", ", evts)}");

                var customs = VfxGraphReflection.GetCustomAttributes(asset);
                Debug.Log($"[VFX Control] Custom attributes ({customs.Count}): " +
                          string.Join(", ", customs.Select(c => $"{c.name}#{c.type}")));
            }
            finally { VfxGraphReflection.Verbose = false; }
        }

        private void OnEnable()
        {
            _ctrl = new VfxControl(this);
            _ctrl.Enable();
            _ctrl.RefreshTarget();
            _ctrl.Rebuild();
        }

        private void OnDisable()
        {
            _ctrl?.Disable();
            _ctrl = null;
        }

        private void OnSelectionChange() => _ctrl?.HandleSelectionChange();
    }
}
