using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[ExecuteInEditMode]
public class DOFFocusOnObject : MonoBehaviour
{
    private float desiredValue;
    [SerializeField] private Transform cam;
    [SerializeField] private PostProcessVolume postProcessingVolume;
    PostProcessProfile PostProfile => postProcessingVolume?.profile;
    [SerializeField, Range(0.1f, 32f)] private float aperture;
    [SerializeField, Range(1, 300)] private int focalLength = 20;
    [SerializeField]private LayerMask layerMask;

    float smoothTime = 0.35f;
    float yVelocity = 0.0f;
    private void Update() {
        if (PostProfile == null || cam == null) return;
        
        Ray ray = cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, ~layerMask)) {
            var dof = PostProfile.GetSetting<DepthOfField>();
            desiredValue = raycastHit.distance;
            dof.focusDistance.value = Mathf.SmoothDamp(dof.focusDistance.value, desiredValue, ref yVelocity, smoothTime);
            dof.aperture.value = aperture;
            dof.focalLength.value = focalLength;
        }
        else {
            var dof = PostProfile.GetSetting<DepthOfField>();
            dof.focusDistance.value = Mathf.SmoothDamp(dof.focusDistance.value, 100, ref yVelocity, smoothTime);
        }
    }
}
