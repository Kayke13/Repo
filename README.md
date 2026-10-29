# PatchZone Internal • Unity Mono Ingame Overlay & Modding Framework

A lightweight in-game GUI overlay framework written in C# utilizing Unity IMGUI, designed for software reverse engineering research, runtime memory inspection, and dynamic reflection (.NET Reflection) on the Unity Engine.

---

## 📌 Project Overview

This repository demonstrates internal tooling development within Unity (Mono runtime) environments. The implementation injects an interactive dark-themed interface directly into the engine's render loop, using runtime reflection to discover, cache, and interact with component instances without modifying game binaries on disk.

### 🛠️ Core Technologies & Concepts

- **C# / .NET Framework 4.x / Mono Runtime**
- **Unity IMGUI (`OnGUI`, `GUI.Window`, `GUILayout`)**: Custom draggable window containers (`GUI.DragWindow`), procedural textures, styled cards, and high-DPI scaling.
- **Advanced Dynamic Reflection (`System.Reflection`)**: Runtime assembly inspection with cached `FieldInfo`, `PropertyInfo`, and `MethodInfo` handles to eliminate runtime lookup overhead.
- **World-to-Screen Projection (W2S)**: 3D-to-2D matrix coordinate conversion with render-texture compensation and frustum culling.
- **Input Pipeline Interception**: Hardware cursor state management (`Cursor.lockState`, `Cursor.visible`) and aim-controller overrides to ensure seamless UI navigation without interfering with camera look angles.

---

## ⚡ Architecture & Modules

The codebase is engineered around a clean, self-contained lifecycle:

### 1. Runtime Host & Lifecycle (`Loader`)
- Static lifecycle handlers (`Init`, `Load`, `Unload`).
- Instantiates a persistent host `GameObject` preserved across scene transitions via `Object.DontDestroyOnLoad`.
- Safe teardown routine (`Loader.Unload`) restoring original game states, restoring cursor locks, and unhooking listeners cleanly.

### 2. GUI Engine & Presentation
- Vertical tab navigation layout with an eye-friendly dark aesthetic.
- Modern charcoal/slate palette accented with deep electric blue.
- Dynamic runtime language switching (English / Portuguese).
- Layer sorting control (`GUI.depth`) preventing ESP overlay elements from clipping through modal dialogs.

### 3. Spatial Awareness & Visuals (ESP / W2S)
- Adaptive 2D bounding boxes scaled by camera distance and target dimensions.
- Target health indicators with proportional color interpolation.
- Frustum culling using camera forward dot product (`Vector3.Dot`) to drop behind-the-lens objects before drawing calls.
- Euclidean distance calculation with configurable maximum range cutoffs.

### 4. Memory & Reflection Engine
- Safe type resolution across loaded domain assemblies without compile-time class dependencies.
- Controlled interaction with kinematic fields (`Rigidbody.velocity`), player health structures, and local avatar movement multipliers.

---

## 🚀 Building from Source

Compile the project directly via CLI using the native Microsoft C# Compiler or Roslyn:

```bash
csc.exe /target:library /out:"CheatMenu.dll" \
  /r:"UnityEngine.dll" \
  /r:"UnityEngine.CoreModule.dll" \
  /r:"UnityEngine.IMGUIModule.dll" \
  /r:"UnityEngine.InputLegacyModule.dll" \
  /r:"UnityEngine.PhysicsModule.dll" \
  /r:"UnityEngine.TextRenderingModule.dll" \
  menu.cs
```

---

## 👤 Developer & Contact

- **Lead Developer:** Kayke
- **YouTube:** [@PatchZoneBR](https://www.youtube.com/@PatchZoneBR)
- **Discord:** `kayke_v1`

---

## ⚖️ Disclaimer

> **Educational & Security Research Purposes Only:**  
> This project is published strictly for educational purposes, software engineering portfolio demonstration, and research into game engine architecture and .NET runtime mechanics. The author neither encourages nor condones deploying this code in multiplayer servers or public matchmaking environments.
