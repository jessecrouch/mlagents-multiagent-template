# Basic Modular ML-Agents Template

The most basic, modular ML-Agents experiment template.

* Designed to be modular to any character prefab - in this case, the CharacterController prefab included in [Unity's Standard Assets](https://assetstore.unity.com/packages/essentials/asset-packs/standard-assets-for-unity-2018-4-32351)
* PlayerAgent wraps around a given CharacterController and pipes player inputs through `Heuristic() -> OnActionReceived() -> ThirdPersonUserControl.cs horizontalInput/verticalInput -> FixedUpdate()`
* PlayerAgent learns to grab the powerup



## Requirements

* ml-agents
* Standard Assets