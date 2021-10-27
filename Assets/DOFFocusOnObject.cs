using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

/// <summary>
/// this class is intended to be put on the focus object itself
/// </summary>
[ExecuteInEditMode]
public class DOFFocusOnObject : MonoBehaviour
{
    [SerializeField] private Transform cam;
    [SerializeField] private PostProcessVolume postProcessingVolume;
    PostProcessProfile PostProfile => postProcessingVolume?.profile;

    [SerializeField, Range(0.1f, 32f)] private float aperture;
    [SerializeField, Range(1, 300)] private int focalLength = 20;
    [SerializeField]private LayerMask layerMask;

    void Update() {
        if (PostProfile == null || cam == null) return;
        
        Ray ray = cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, ~layerMask)) {
            var dof = PostProfile.GetSetting<DepthOfField>();
            dof.focusDistance.value = raycastHit.distance;
            dof.aperture.value = aperture;
            dof.focalLength.value = focalLength;
        }
        else {
            var dof = PostProfile.GetSetting<DepthOfField>();
            dof.focusDistance.value = 100;
        }
    }
}
