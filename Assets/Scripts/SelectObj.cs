using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObj : MonoBehaviour
{   
    [HideInInspector]public bool canSelect;
    private bool hasSelectedObj;
   [SerializeField]private Camera mainCamera;
   private LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {
        canSelect = true;
        layerMask = gameObject.GetComponent<Building>().layerMask;
    }

    // Update is called once per frame
    void Update()
    {
        if(canSelect){
            if(Input.GetKeyDown(KeyCode.Mouse0)){
                if(hasSelectedObj){
                    foreach (GameObject turret in GameObject.FindGameObjectsWithTag("Turret"))
                    {
                        turret.GetComponent<Turret>().DisableSelect();
                    }
                    hasSelectedObj = false;
                }
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, ~layerMask)) {
                    if(raycastHit.collider.gameObject.CompareTag("Turret")){
                        raycastHit.collider.gameObject.GetComponent<Turret>().SelectTurret();
                        hasSelectedObj = true;
                    }
                }
            }
        }
    }
    public void DeselectAll(){
        foreach (GameObject turret in GameObject.FindGameObjectsWithTag("Turret"))
        {
            turret.GetComponent<Turret>().DisableSelect();
        }
    }
}
