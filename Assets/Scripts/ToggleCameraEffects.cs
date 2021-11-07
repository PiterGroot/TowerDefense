using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class ToggleCameraEffects : MonoBehaviour
{
    [SerializeField]private Sprite ShowcaseOn, ShowcaseOff;
    [SerializeField] private Image image;
    private Toggle effectsToggle;
    private void Awake() {
        effectsToggle = GetComponent<Toggle>();
    }
    public void OnToggleValueChange() {
        var newColorBlock1 = effectsToggle.colors;
        newColorBlock1.normalColor = Color.white;
        effectsToggle.colors = newColorBlock1;
        if (effectsToggle.isOn) {
            var newColorBlock = effectsToggle.colors;
            newColorBlock.normalColor = Color.green;
            newColorBlock.selectedColor = Color.green;
            effectsToggle.colors = newColorBlock;
            PlayerPrefs.SetInt("CameraEffects", 1);
            Camera.main.gameObject.GetComponent<PostProcessVolume>().weight = 100;
            image.sprite = ShowcaseOn;

        } else {
            var newColorBlock = effectsToggle.colors;
            newColorBlock.normalColor = Color.white;
            newColorBlock.selectedColor = Color.white;
            effectsToggle.colors = newColorBlock;
            PlayerPrefs.SetInt("CameraEffects", 0);
            Camera.main.gameObject.GetComponent<PostProcessVolume>().weight = 0;
            image.sprite = ShowcaseOff;
        }
    }
}
