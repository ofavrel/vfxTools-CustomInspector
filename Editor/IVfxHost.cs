// VFX Control — host abstraction shared by the dockable window and the custom Inspector.
//
// The VfxControl controller builds its UI into the host's Root and reads the host's tear-off state.
// The window host backs SoloTab with a [SerializeField] (persists across reloads) + a real tear-off;
// the inspector host reports null (never solo) and a no-op OpenSolo.

using UnityEngine.UIElements;

namespace VfxControl.EditorTools
{
    internal interface IVfxHost
    {
        // The element the controller builds into (and schedules its clock on).
        VisualElement Root { get; }

        // True for the custom Inspector host (drives layout/feature gating, e.g. no transport/tear-off).
        bool IsInspector { get; }

        // The pinned tab of a torn-off window, or null/empty for a full window / the inspector.
        string SoloTab { get; }

        // Tear off the given tab into its own window (window host); no-op for the inspector.
        void OpenSolo(string tabId, string label);
    }
}
