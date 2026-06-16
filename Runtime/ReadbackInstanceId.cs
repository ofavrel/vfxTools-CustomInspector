// VFX Inspector — the readback instance-id port type.
//
// A 1-field [VFXType] so graph authors get an easy, type-matched blackboard property instead of an
// exposed Int with a magic name: add "Readback Instance Id" from the blackboard (+) and wire it to the
// "Debug Readback" subgraph block. The VFX Inspector finds this property BY TYPE (RealType ==
// nameof(ReadbackInstanceId)), assigns each selected VisualEffect a distinct Id at runtime so instances
// land in separate readback regions, and hides the property from its Properties tab. Leave Id at 0.
//
// [VFXType] must live in a RUNTIME assembly (VFX Graph can't see editor-only types) — hence this folder.

using System;
using UnityEngine;
using UnityEngine.VFX;

namespace VfxInspector
{
    [Serializable]
    [VFXType(VFXTypeAttribute.Usage.Default, "Readback Instance Id")]
    public struct ReadbackInstanceId
    {
        [Tooltip("Wire to the Debug Readback block. The VFX Inspector assigns this per selected instance — leave at 0.")]
        public uint Id;
    }
}
