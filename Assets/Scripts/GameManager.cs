using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [HideInInspector]public bool Towermode;
    private bool isDroneMode;
    private Building buildingScript;
    [SerializeField]private Text altitude, latitude, longitude;
    [SerializeField]private float RotSpeed;
    [SerializeField]private Transform Rotationpoint;
    [SerializeField]private Camera mainCam, droneCam;
    [SerializeField]private GameObject DronePrefab;
    private void Start() {
        buildingScript = GetComponent<Building>();
        Towermode = true;
    }

    public void ToggleDroneMode(){
        isDroneMode = !isDroneMode;
        if(isDroneMode){
            Towermode = false;
            BuildingUI();
            buildingScript.canBuild = false;
            droneCam.gameObject.SetActive(true);
            mainCam.gameObject.SetActive(false);
            GetComponent<SelectObj>().DeselectAll();
            GetComponent<SelectObj>().canSelect = false;
            DronePrefab.GetComponent<DroneController>().canControl = true;
        }
        else{
            if(FindObjectOfType<Rope>().isGrappled){
                foreach (GameObject crate in GameObject.FindGameObjectsWithTag("Crate"))
                {
                    if(crate.GetComponent<Crate>().isAttatched){
                        crate.GetComponent<Crate>().isAttatched = false;
                        FindObjectOfType<Rope>().isEmpty = true;
                        FindObjectOfType<Rope>().isDetaching = true;
                        FindObjectOfType<Rope>().isGrappled = false;
                    }
                }
            }
            Towermode = true;
            Cursor.visible = true;
            buildingScript.canBuild = false;
            mainCam.gameObject.SetActive(true);
            droneCam.gameObject.SetActive(false);
            GetComponent<SelectObj>().canSelect = true;
            FindObjectOfType<TurretBar>().canToggleUI = true;
        }
    }

    private void BuildingUI(){
        FindObjectOfType<TurretBar>().canToggleUI = false;
        if(FindObjectOfType<TurretBar>().barState){
            FindObjectOfType<TurretBar>().barState = false;
            FindObjectOfType<TurretBar>().barAnim.SetTrigger("Disappear");
        }
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Tab)){
            ToggleDroneMode();
        }
        if(Towermode){
            if(Input.GetKey(KeyCode.D)){
                //right
                Rotationpoint.transform.Rotate(new Vector3(0, -RotSpeed * Time.deltaTime, 0));
            }
            if(Input.GetKey(KeyCode.A)){
                //left
                Rotationpoint.transform.Rotate(new Vector3(0, RotSpeed * Time.deltaTime, 0));
            }
        }
        else{
            altitude.text = droneCam.transform.position.y.ToString();
            latitude.text = droneCam.transform.position.x.ToString();
            longitude.text = droneCam.transform.position.z.ToString();
        }
    }
}
