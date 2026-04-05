using UnityEngine;
using UnityEngine.Rendering;

public class NoFogCamera : MonoBehaviour
{
    Camera cam;


    private void OnEnable()
    {
        cam = GetComponent<Camera>();
        RenderPipelineManager.beginCameraRendering += OnBeginCamera;
        RenderPipelineManager.endCameraRendering += OnEndCamera;
    }

    private void OnDisable()
    {
        RenderPipelineManager.beginCameraRendering -= OnBeginCamera;
        RenderPipelineManager.endCameraRendering -= OnEndCamera;
    }

    void OnBeginCamera(ScriptableRenderContext context, Camera camera)
    {
        if (camera == cam)
            RenderSettings.fog = false;
    }

    void OnEndCamera(ScriptableRenderContext context, Camera camera)
    {
        if (camera == cam)
            RenderSettings.fog = true;
    }
}
