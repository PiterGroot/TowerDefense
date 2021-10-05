using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBar : MonoBehaviour
{
    [HideInInspector]public bool canToggleUI;
    [HideInInspector]public bool barState;
    [SerializeField] public Animator barAnim;
    [SerializeField]private Building buildingScript;
    private void Awake() {
        canToggleUI = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(canToggleUI){
            if (Input.GetKeyDown(KeyCode.E)) {
            barState = !barState;
            if (barState) {
                barAnim.SetTrigger("Appear");
                buildingScript.canBuild = false;
            }
            else {
                barAnim.SetTrigger("Disappear");
            }
        }
        }
    }
}
