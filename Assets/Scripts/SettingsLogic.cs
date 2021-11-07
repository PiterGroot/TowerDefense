using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SettingsLogic : MonoBehaviour
{
    private void Start() {
        PlayerPrefs.SetInt("CameraEffects", 1);
        if( PlayerPrefs.GetInt("CameraEffects") == 0){
            Camera.main.gameObject.GetComponent<PostProcessVolume>().weight = 0;
        }
        else {
            Camera.main.gameObject.GetComponent<PostProcessVolume>().weight = 100;
        }
    }
}
