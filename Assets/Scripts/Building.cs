using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Building : MonoBehaviour
{
    private SelectObj selectOBJ;
    private int CurrentPrice;
    private bool isGreen;
    private bool isRed;
    private RaycastHit hit;
    [HideInInspector]public bool canBuild;
    [SerializeField]public LayerMask ignoredLayer;
    [SerializeField] private Camera mainCamera;
    [SerializeField]private GameObject CurrentTower, ShadowTowerGreen, ShadowTowerRed;
    [SerializeField] private GameObject[] Turrets;
    [Header("Buying")]
    [SerializeField] private int[] Prices;
    [SerializeField] private TextMeshProUGUI[] PriceSigns;
    [SerializeField] private TextMeshProUGUI[] ShadowPriceSigns;

    private void Awake() {
        selectOBJ = gameObject.GetComponent<SelectObj>();
        for (int i = 0; i < PriceSigns.Length; i++) {
            PriceSigns[i].text = Prices[i].ToString();
            ShadowPriceSigns[i].text = Prices[i].ToString();
        }
    }
    // Update is called once per frame
    void Update()
    {   
        if(canBuild){
            selectOBJ.canSelect = false;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, ~ignoredLayer)) {
                hit = raycastHit;
                if(isGreen){
                    ShadowTowerRed.SetActive(false);
                    ShadowTowerGreen.SetActive(true);
                    ShadowTowerGreen.transform.position = raycastHit.point;
                }
                else if(isRed){
                    ShadowTowerGreen.SetActive(false);
                    ShadowTowerRed.SetActive(true);
                    ShadowTowerRed.transform.position = raycastHit.point;
                }
                try{
                    if (hit.collider.gameObject.layer == 8) {
                        isRed = false;
                        isGreen = true;
                        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse0)) {
                            switch(CurrentPrice){
                                case 200:
                                    if(FindObjectOfType<Wallet>().currentBalance >= Prices[0]){
                                        FindObjectOfType<Wallet>().RemoveMoney(Prices[0]);
                                        Instantiate(CurrentTower, hit.point, Quaternion.identity);
                                    }
                                    else{
                                        canBuild = false;
                                        selectOBJ.canSelect = true;
                                    }
                                break;
                                case 500:
                                    if(FindObjectOfType<Wallet>().currentBalance >= Prices[1]){
                                        FindObjectOfType<Wallet>().RemoveMoney(Prices[1]);
                                        Instantiate(CurrentTower, hit.point, Quaternion.identity);
                                    }
                                    else{
                                        canBuild = false;
                                        selectOBJ.canSelect = true;
                                    }
                                break;
                                case 750:
                                    if(FindObjectOfType<Wallet>().currentBalance >= Prices[2]){
                                        FindObjectOfType<Wallet>().RemoveMoney(Prices[2]);
                                        Instantiate(CurrentTower, hit.point, Quaternion.identity);
                                    }
                                    else{
                                        canBuild = false;
                                        selectOBJ.canSelect = true;
                                    }
                                break;
                            }   
                        }
                    }else {
                        isGreen = false;
                        isRed = true;
                    }
                }catch (System.Exception)
                {
                    return;
                }
            }
            else{
                ShadowTowerGreen.SetActive(false);
                ShadowTowerRed.SetActive(false);
                selectOBJ.canSelect = true;
            }
        }
        else{
            ShadowTowerGreen.SetActive(false);
            ShadowTowerRed.SetActive(false);
            selectOBJ.canSelect = true;
        }
    }
    public void SwitchToTurret() {
        if(FindObjectOfType<Wallet>().currentBalance >= Prices[0]){
            print("Bought turret");
            Invoke("EnableBuilding", .1f);
            CurrentTower = Turrets[6];
            ShadowTowerGreen = Turrets[7];
            ShadowTowerRed = Turrets[8];
            CurrentPrice = Prices[0];
        }
        else {
            print("Not enough coins");
        }
    }
    public void SwitchToAOW() {
        if (FindObjectOfType<Wallet>().currentBalance >= Prices[2]) {
            print("Bought aow");
            Invoke("EnableBuilding", .1f);
            CurrentTower = Turrets[0];
            ShadowTowerGreen = Turrets[1];
            ShadowTowerRed = Turrets[2];
            CurrentPrice = Prices[2];
        }
        else {
            print("Not enough coins");
        }
    }
    public void SwitchToBuff() {
        if (FindObjectOfType<Wallet>().currentBalance >= Prices[1]) {
            print("Bought buff");
            Invoke("EnableBuilding", .1f);
            CurrentTower = Turrets[3];
            ShadowTowerGreen = Turrets[4];
            ShadowTowerRed = Turrets[5];
            CurrentPrice = Prices[1];
        }
        else {
            print("Not enough coins");
        }
    }
    private void EnableBuilding(){
        canBuild = true;
        FindObjectOfType<TurretBar>().barState = false;
        FindObjectOfType<TurretBar>().barAnim.SetTrigger("Disappear");
    }
}
