# NVIDIA Reflex implementation package

## Integrating into your Unity application

1. Copy the `NVIDIA Reflex implementation package` into your projects Packages (or CustomPackages, or basically anywhere in the project) directory.<br>
<img src="./Images/PackagesDirectory.png" width="1000"><br><br>
<img src="./Images/PackagesDirectoryWithTARBall.png" width="1000"><br><br>
2. Open your project in Unity and select the Package Manger from 'Window'.<br>
<img src="./Images/PackageManager.png" width="1000"><br><br>
3. Select `Add package from tarball...`<br>
<img src="./Images/PackageManagerAddTARBall.png" width="1000"><br><br>
4. Navigate to the folder you placed the `NVIDIA Reflex implementation package` into.<br>
<img src="./Images/PackagesDirectoryWithTARBallSelected.png" width="1000"><br><br>
5. You should now see that the package has been added to your package list under `Custom` in the package manager.<br>
<img src="./Images/PackageManagerAddedReflex.png" width="1000"><br><br>
6. Select the main camera and on the inspector click `Add Component`<br>
<img src="./Images/MainCameraAddComponent.png" width="1000"><br><br>
7. You will find the scripts under a main label `NVIDIA`<br>
<img src="./Images/AddComponentNVIDIA.png"><br><br>
8. Add the component `Reflex` and optionally `Reflex Debug`. `Reflex Debug` is an example implimention for controlling the DLSS settings and operation that will be replaced by the applications own user interface for settings.<br>
<img src="./Images/AddComponentNVIDIAReflex.png"><br><br>
9. You should now see the component(s) added to your main camera.<br>
<img src="./Images/MainCameraReflexAdded.png" width="1000"><br><br>
10. Restart Unity so that the low level native plugin DLL may be initialised correctly.
11. Press play and you may control Reflex by changing options on the `Reflex` component, or if you added the optional `Reflex Debug` component, using keyboard input with GUI feedback (F1/F2/F3).<br>
<img src="./Images/MainCameraReflexAddedPlay.png" width="1000"><br><br>

## Supporting multiple cameras

By default when attaching the `Reflex` component to a camera, both `Is First Camera` and `Is Last Camera` will be enabled. Multiple `Reflex` components may exist in a project, but only one should be enabled and be selected as `Is First Camera` and only one should be enabled and be selected as `Is Last Camera` (for a single camera workflow this is the same component).
The configuration of inputs to Reflex (`intervalUs`, `isLowLatencyMode` and `isLowLatencyBoost`) come from the single `Reflex` component that is enabled and `Is First Camera`.

## Max Queued Frames

Unity defaults to a Max Queued Frames of 2 on the PC, this tries to balance frame rate and latency. However with `Reflex` you may see a latency reduction by reducing this to 1.

https://docs.unity3d.com/ScriptReference/QualitySettings-maxQueuedFrames.html

## Adding trigger flash support

To add the Reflex marker to trigger a flash, simply call the following Reflex monobehavior method in the applications input handler once it has detected that the players weapon should fire.

		TriggerFlash()

## When events are sent to Reflex

* NvReflex_LATENCY_MARKER_TYPE.SIMULATION_START is sent on event `FixedUpdate` if a fixed update is required or else it will be sent on event `Update`.
* NvReflex_LATENCY_MARKER_TYPE.SIMULATION_END is sent on event `LateUpdate`.
* NvReflex_LATENCY_MARKER_TYPE.INPUT_SAMPLE is sent on event `Update`.
* NvReflex_LATENCY_MARKER_TYPE.RENDERSUBMIT_START is sent via a command buffer on camera event `BeforeDepthTexture` for forward rendering, on camera event `BeforeGBuffer` for deferred rendering and from the render pipelines `beginFrameRendering` on the scriptable render pipeline.
* NvReflex_LATENCY_MARKER_TYPE.RENDERSUBMIT_END is sent via a command buffer on the camera event `AfterEverything` and from the render pipelines `endFrameRendering` on the scriptable render pipeline
* NvReflex_LATENCY_MARKER_TYPE.RENDERSUBMIT_START and NvReflex_LATENCY_MARKER_TYPE.RENDERSUBMIT_END command buffers are always updated with the latest frame ID during `LateUpdate`.

Note: For accuracy *_START should be processed before other scripts/events/command buffers and *_END should be processed after other scripts/events/command buffers.

https://docs.unity3d.com/Manual/ExecutionOrder.html

https://docs.unity3d.com/Manual/GraphicsCommandBuffers.html


