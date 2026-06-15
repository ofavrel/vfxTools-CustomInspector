# VFX Control

A denser, more controllable **custom inspector for Unity's `VisualEffect` component**, distributed as a
UPM package.

> **Status — 0.1.0 (probe).** This release contains only a scaffold and an *inspector-priority probe*: a
> bare-minimum `[CustomEditor(typeof(VisualEffect))]` used to confirm that this package's editor reliably
> overrides the stock VFX inspector when installed via UPM. The full inspector lands on a later branch.

## Requirements
- Unity **6000.0+**
- `com.unity.visualeffectgraph` installed in the target project

## Install (for testing)
**Package Manager ▸ + ▸ Add package from disk…** and select this package's `package.json`.
(Once published to a git remote: **Add package from git URL…** with the repo URL.)

## Verify the probe wins
1. Select a GameObject with a `VisualEffect` component.
   - The Inspector should show a blue **“VFX Control custom inspector (PROBE, UPM package)…”** HelpBox —
     that means *our* editor won over the VFX package's.
2. Run **Tools ▸ VFX Control ▸ Probe: Diagnose VisualEffect Editor**. The Console prints:
   - `WINNING editor = VfxControl.Inspector.VfxControlInspectorProbe (asm VfxTools.VfxControl.Editor)`
   - one `candidate editor:` line per registered editor targeting `VisualEffect` (expect ours **and**
     `UnityEditor.VFX.AdvancedVisualEffectEditor`).
3. Re-check after a script recompile, entering/exiting Play Mode, and a full Unity restart — the winner
   should never flip.

## Why it wins
Unity's `CustomEditorAttributes` ranks **non-Unity** assemblies ahead of Unity ones when multiple
`[CustomEditor]` target the same type. This package references no Unity packages (its editor asmdef has
`references: []`), so it stays a non-Unity assembly and outranks the VFX package's editor.

## License
MIT — see [LICENSE.md](LICENSE.md).
