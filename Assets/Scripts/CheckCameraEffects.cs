using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CheckCameraEffects : MonoBehaviour
{
    [SerializeField]private PostProcessVolume effectLayer;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("CameraEffects") == 0) {
            //off
            effectLayer.weight = 0;
        }
        else {
            //on
            effectLayer.weight = 100;
        }
    }
}
