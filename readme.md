# Basic Modular ML-Agents Template

The most basic, modular [ML-Agents](https://github.com/Unity-Technologies/ml-agents) experiment template.

* Designed to be modular to any character prefab - in this case, the CharacterController prefab included in [Unity's Standard Assets](https://assetstore.unity.com/packages/essentials/asset-packs/standard-assets-for-unity-2018-4-32351)
* PlayerAgent wraps around a given CharacterController and pipes player inputs through `(Optional) Heuristic() -> OnActionReceived() -> ThirdPersonUserControl.cs horizontalInput/verticalInput -> FixedUpdate()`
* PlayerAgent learns to grab the powerup
  * Using `Ray Perception Sensor`



## Requirements

* [ml-agents](https://github.com/Unity-Technologies/ml-agents)
* [Standard Assets](https://assetstore.unity.com/packages/essentials/asset-packs/standard-assets-for-unity-2018-4-32351)



## Notes

* The entire Standard Assets library is included so that you can play around with the other assets (vehicles, ball character, etc) if you want