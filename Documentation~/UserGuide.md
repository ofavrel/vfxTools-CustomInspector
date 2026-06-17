# VFX Inspector — User Guide

A friendlier, more powerful inspector for Unity's **Visual Effect** component. This guide is
**task-based**: each section starts with what you want to achieve, then shows you how. Skim the
[Quick start](#quick-start), then jump straight to whatever you're trying to do.

> 🖼️ *Screenshot — `01-hero.png`: the VFX Inspector open on an effect, tabs and transport visible.*

---

## Contents

- [Quick start](#quick-start)
- [Install](#install)
- [The layout at a glance](#the-layout-at-a-glance)
- **Recipes — "I want to…"**
  - [Tweak an effect's properties](#tweak-an-effects-properties)
  - [Find a property fast (search, filters, favorites)](#find-a-property-fast)
  - [Turn a whole feature on or off](#turn-a-whole-feature-on-or-off)
  - [Play, scrub, and loop the effect](#play-scrub-and-loop-the-effect)
  - [Change seed, duration, and initial event](#change-seed-duration-and-initial-event)
  - [Send an event (with a payload)](#send-an-event-with-a-payload)
  - [Edit shapes directly in the Scene](#edit-shapes-directly-in-the-scene)
  - [Check performance & memory](#check-performance--memory)
  - [Inspect individual particles](#inspect-individual-particles)
  - [Export particle data to CSV](#export-particle-data-to-csv)
  - [Edit several effects at once](#edit-several-effects-at-once)
  - [Pop a tab out into its own window](#pop-a-tab-out-into-its-own-window)
- [Troubleshooting & FAQ](#troubleshooting--faq)
- [Learn more](#learn-more)

---

## Quick start

1. **Install** the package (see [Install](#install)).
2. In the **Hierarchy**, select any GameObject that has a **Visual Effect** component.
3. The VFX Inspector appears automatically in the Inspector — it replaces Unity's stock VFX inspector.

That's it. Everything below is about getting more out of it.

> **Outcome:** you select an effect and immediately see a denser, organized, controllable inspector
> instead of the default flat list.

---

## Install

**Requirements**

- Unity **6000.0** or newer
- The **Visual Effect Graph** package (`com.unity.visualeffectgraph`) — installed automatically as a
  dependency.

**Steps**

1. Open **Window ▸ Package Manager**.
2. Click **➕ ▸ Add package from disk…** and pick this package's `package.json`
   *(or **Add package from git URL…** and paste the repository URL).*
3. Done — select a Visual Effect and the new inspector takes over.

> 🖼️ *Screenshot — `02-install.png`: the Package Manager "Add package" menu.*

---

## The layout at a glance

The inspector is organized into a header, a row of **tabs**, an optional **section rail** down the
side, and a body. A persistent **transport bar** for playback sits near the top.

> 🖼️ *Screenshot — `03-anatomy.png`: the inspector with callouts on the transport, tabs, section rail,
> search/filter chips, and footer.*

| Tab | What it's for |
|---|---|
| **All** | A traditional, everything-stacked view — Properties + Renderer + Playback, no rail. The default. |
| **Properties** | Your effect's exposed parameters, grouped by category. |
| **Playback** | Duration, seed, initial event, and the Send-Event panel. |
| **Debug** | Live stats, per-system bars, texture usage, the per-particle spreadsheet, and visualizers. |
| **Renderer** | The effect's renderer settings (probes, sorting, rendering layers). |

Across the top of most tabs you'll find a **search box** and **filter chips** (All / ★ Favorites /
Modified). These narrow down whatever tab you're currently on.

---

# Recipes — "I want to…"

## Tweak an effect's properties

**Open the Properties tab.** Your exposed parameters are grouped into collapsible **categories**, with
typed controls (sliders, color pickers, vectors, curves, gradients, object fields, dropdowns).

- **Drag to scrub** numeric values by dragging on the field's label.
- **Modified markers** highlight any value you've changed from the asset default.
- **Reset** a single value with the **↺** that appears when you hover a row.
- **Copy / paste** a value: right-click a property's label. Values round-trip with the standard
  Inspector too, so you can copy from one effect and paste into another.
- **Lock proportions** on multi-component values (like a scale) with the **chain** icon, just like the
  Transform's scale lock.

> 🖼️ *Screenshot — `04-properties.png`: a category expanded, showing a slider, a color field, and the
> hover tools (↺ reset, ★ favorite).*

> **Outcome:** the values you actually care about are easy to find, change, and reset — without hunting
> through a flat list.

---

## Find a property fast

When an effect exposes a lot of parameters:

- **Search** — type in the box to filter the current tab to matching properties.
- **Filter chips** — **All**, **★ Favorites**, **Modified**. Click **Modified** to see only what you've
  changed; click **★** to see only your pinned favorites.
- **Favorites (★)** — hover any row and click the **star** to pin it. Pinned properties collect into a
  **Favorites group** at the top of the tab, so your go-to controls are always one glance away.
- **Categories** — use the **section rail** on the left to jump to a category. **Alt/Option-click** a
  category or struct header to expand/collapse everything beneath it at once.

> 🖼️ *Screenshot — `05-favorites.png`: the Favorites group at the top with a couple of pinned controls,
> and the filter chips highlighted.*

> **Outcome:** the three or four controls you tune constantly are pinned and instantly reachable, even in
> a graph with dozens of parameters.

---

## Turn a whole feature on or off

Many graphs gate a feature (a sub-effect, a color-over-life block, etc.) behind a boolean. When a
category has a matching on/off boolean, the inspector promotes it to a **master toggle** in the category
header.

- Flip the header toggle to **enable/disable** the whole category.
- When off, the category's controls grey out and collapse (the values are still there — expand to peek).

> 🖼️ *Screenshot — `06-enable-gate.png`: a category header with its master enable toggle, shown both on
> and off.*

> **Outcome:** switch an entire feature of the effect on or off from one obvious toggle, instead of
> hunting for the right boolean.

---

## Play, scrub, and loop the effect

The **transport bar** near the top is always available, whatever tab you're on.

- **Scrub bar** — drag to seek through the effect's timeline; the time and live particle count show
  alongside.
- **Buttons** — restart · step back · **play/pause** · step forward · **Loop**.
- **Rate** — a slider (0–10×) to speed up or slow down playback; **↺** resets to 1×.

> 🖼️ *Screenshot — `07-transport.png`: the transport bar with the scrub, buttons, and Rate slider
> labeled.*

> **Outcome:** preview and inspect any moment of the effect — including paused, frame-stepped, or
> slowed-down — without leaving the inspector.

---

## Change seed, duration, and initial event

Open the **Playback** tab → **Playback options**.

- **Duration** — how long the preview timeline runs (default 10s).
- **Start Seed** + **Reseed (⚄)** — set the random seed, or randomize and restart to explore variations.
- **Reseed on Play** — get a fresh seed each time it loops/plays.
- **Initial Event** — which event the effect starts with (e.g. `OnPlay`).

Each of these can be **pinned (★)**, **reset (↺)**, and shows a **modified marker** like any property.

> 🖼️ *Screenshot — `08-playback-options.png`: the Playback options section with Duration, Start Seed +
> Reseed, and Initial Event.*

> **Outcome:** dial in repeatable previews (fixed seed) or explore randomness (reseed) without touching
> the graph.

---

## Send an event (with a payload)

Open the **Playback** tab → **Send Event**. This lets you fire the effect's events live and attach a
data payload — great for testing event-driven behavior.

1. **Event chips** — click **OnPlay** / **OnStop**, or any **custom event** your graph defines, to send
   it immediately.
2. **Event Attributes** — build a payload in the list below: add an attribute with the **+** button and
   pick a **built-in** attribute, one of your graph's **custom attributes**, or a free **custom** one.
3. Set its value (e.g. a position, a color, a float). The next chip you click sends the event **with**
   that payload.

> 🖼️ *Screenshot — `09-send-event.png`: the Send Event section — event chips on top, the Event
> Attributes list with a couple of rows below.*

> **Outcome:** trigger events and pass real data to them from the editor, so you can test reactions
> (bursts, color changes, spawns) without writing a test script.

> 💡 The standard **`color`** attribute is edited with a color picker here and sends correctly — a known
> quirk the stock event tester gets wrong.

---

## Edit shapes directly in the Scene

For spaceable struct properties — **Position, Direction, Box, Cone, Sphere, Circle, Torus, Line, Plane,
Transform** — you can edit them with handles right in the Scene view.

1. Find the property in the **Properties** tab (it shows a space icon and an **Edit in Scene** button on
   its header).
2. Click **Edit in Scene**. A gizmo appears at the effect.
3. Drag the handles to move/rotate/scale, change radii, sweep arcs, etc. The values update live in the
   inspector.

The gizmo respects the active tool (Move/Rotate/Scale) and the property's **Local vs World** space.

> 🖼️ *Screenshot — `10-gizmo.png`: a Cone or Sphere gizmo active in the Scene view with its handles and
> on-screen label.*

> **Outcome:** position and size spawn shapes by eye in the Scene, instead of typing numbers blind.

---

## Check performance & memory

Open the **Debug** tab → **Live statistics** and **Systems**.

- **Live statistics** — alive particles, efficiency, system count, bounds, state, **CPU time**, **GPU
  time**, and total **attribute memory**.
- **Systems** — a **capacity bar** per particle system (green = well-utilized, red = under-used), with a
  per-system **cpu / gpu / memory** breakdown. Expand a system to see its full **attribute layout**
  (what each particle stores and how many bytes).
- **Textures** — every texture wired into the graph, with resolution and memory size; click to ping the
  asset.

> 🖼️ *Screenshot — `11-debug-stats.png`: the Live statistics grid and a couple of per-system capacity
> bars.*

> **Outcome:** spot an over-allocated system, a heavy texture, or a CPU/GPU cost at a glance — directly
> on the effect you're looking at.

> ℹ️ Some timing and memory figures appear only once the graph has compiled, and CPU/GPU markers show
> while the effect is updating. If a value reads "—", it isn't available yet.

---

## Inspect individual particles

The **Debug ▸ Particles** spreadsheet shows live, per-particle attribute values (position, velocity,
color, age, size, …). Because VFX particles live on the GPU, this is **opt-in**: you instrument the
graph once so it can report its particles back.

**Set it up (once per graph):**

1. In the VFX Graph, add the package's **"Debug Readback" subgraph block** (from the package's
   `Readback/` folder) to a system's **Update** or **Output** context.
2. Save the graph.
3. Select the effect, open **Debug ▸ Particles** — the spreadsheet fills with live particles.

**Use it:**

- **Columns** are driven by what the system actually uses; click headers to **sort**, and right-click to
  **show/hide** columns.
- **Select** rows (Ctrl/Shift for several). With a row selected, **Alt-click** frames the Scene view on
  those particles.
- Toggle a column's **eye** to draw that attribute's value as a **label in the Scene** at each selected
  particle.

> 🖼️ *Screenshot — `12-particles.png`: the per-particle spreadsheet with a few columns and the Scene
> overlay showing values on selected particles.*

> **Outcome:** see the actual numbers a particle carries this frame — invaluable for debugging "why is
> this one behaving oddly?"

> 🔢 **Multiple systems / instances:** put a Debug Readback block in each system (give each a distinct
> `systemId`), and for telling instances apart, add a **Readback Instance Id** property from the
> blackboard and wire it to the block. The panel shows a legend mapping numbers to systems. See the
> in-panel help for details.

---

## Export particle data to CSV

With the **Particles** spreadsheet showing data, click **Export CSV** in its toolbar and choose a file.
You get the current captured frame — every attribute (multi-component ones split into X/Y/Z, colors
into R/G/B), regardless of which columns are hidden.

> **Outcome:** pull a frame of particle data into a spreadsheet or script for analysis.

---

## Edit several effects at once

Select **multiple GameObjects** that share the **same** Visual Effect asset. The inspector edits them
together:

- Controls show a **"mixed values"** state where instances differ.
- Any change you make applies to **all** selected instances.
- The header notes how many extra instances you're editing.

> **Outcome:** retune a whole group of identical effects in one pass.

---

## Pop a tab out into its own window

Want the Debug tab visible while you work in another inspector? Tear a tab off into its own dockable
window:

- **Right-click a tab** inside the inspector → **Open in new window**, **or**
- Use the component's **gear ▸ VFX Inspector ▸ \<Tab\>** menu.

The popup is a focused, single-tab view you can dock anywhere. The main inspector stays the "home" for
playback.

> 🖼️ *Screenshot — `13-tearoff.png`: a Debug tab popped out as its own docked window next to the Scene.*

> **Outcome:** keep live stats or the particle spreadsheet on screen while you do other work.

---

## Troubleshooting & FAQ

**The inspector looks like Unity's normal one.**
Make sure the object you selected actually has a **Visual Effect** component, and that the package is
installed. The custom inspector only appears for `VisualEffect` components.

**The Particles spreadsheet is empty / shows a help message.**
The graph isn't instrumented yet. Add the **Debug Readback** block to an **Update** or **Output**
context (not Initialize) and save. If you have multiple systems or instances, see
[Inspect individual particles](#inspect-individual-particles).

**Some Debug numbers show "—".**
Those figures (certain timings, attribute memory) become available only after the graph compiles and
while the effect is updating. Let it play/update and they'll populate.

**A property/gizmo I expected isn't there.**
Run **Tools ▸ VFX Inspector ▸ Diagnose Target** — it logs what the inspector could read from your
effect, which helps pin down graph or version mismatches.

**My torn-off tab reverted to the full inspector.**
A popup's single-tab filter is set when you open it and doesn't survive a script recompile — just
re-open it from the menu.

---

## Learn more

- **[README](../README.md)** — what the package is and how to install it.
- **[Architecture & Code Overview](../OVERVIEW.md)** — how it's built, for contributors and the curious.
- **[Project context / ground truth](VfxInspector.md)** — exhaustive developer notes.

---

*Screenshots in this guide are placeholders (filenames suggested in italics) — drop your images into
`Documentation~/Images/` and replace each placeholder line with `![alt](Images/<file>.png)`.*
