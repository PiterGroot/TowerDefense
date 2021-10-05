using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Building : MonoBehaviour
{   
    private bool isGreen;
    private bool isRed;
    private RaycastHit hit;
    [HideInInspector]public bool canBuild;
    [SerializeField]public LayerMask layerMask;
    [SerializeField] private Camera mainCamera;
    [SerializeField]private GameObject CurrentTower, ShadowTowerGreen, ShadowTowerRed;
    [SerializeField] private GameObject[] Turrets;
    [Header("Buying")]
    [SerializeField] private int[] Prices;
    [SerializeField] private TextMeshProUGUI[] PriceSigns;
    [SerializeField] private TextMeshProUGUI[] ShadowPriceSigns;

    private void Awake() {
        for (int i = 0; i < PriceSigns.Length; i++) {
            PriceSigns[i].text = Prices[i].ToString();
            ShadowPriceSigns[i].text = Prices[i].ToString();
        }
    }
    // Update is called once per frame
    void Update()
    {   
        if(Input.GetKeyDown(KeyCode.Mouse1)){
            if(FindObjectOfType<GameManager>().Towermode){
                canBuild = !canBuild;
                if(canBuild){
                    GetComponent<SelectObj>().canSelect = false;
                }
                else{
                    GetComponent<SelectObj>().canSelect = true;
                }
            }
        }
        if(canBuild){
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, ~layerMask)) {
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
                        if (Input.GetKeyDown(KeyCode.Mouse0)) {
                            Instantiate(CurrentTower, hit.point, Quaternion.identity);
                            canBuild = false;
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
            }
        }
        else{
            ShadowTowerGreen.SetActive(false);
            ShadowTowerRed.SetActive(false);
        }
    }
    public void SwitchToTurret() {
        if(FindObjectOfType<Wallet>().currentBalance >= Prices[0]){
            print("Bought turret");
            FindObjectOfType<Wallet>().RemoveMoney(Prices[0]);
            Invoke("EnableBuilding", .1f);
            CurrentTower = Turrets[6];
            ShadowTowerGreen = Turrets[7];
            ShadowTowerRed = Turrets[8];
        }
        else {
            print("Not enough coins");
        }
    }
    public void SwitchToAOW() {
        if (FindObjectOfType<Wallet>().currentBalance >= Prices[2]) {
            print("Bought aow");
            FindObjectOfType<Wallet>().RemoveMoney(Prices[2]);
            Invoke("EnableBuilding", .1f);
            CurrentTower = Turrets[0];
            ShadowTowerGreen = Turrets[1];
            ShadowTowerRed = Turrets[2];
        }
        else {
            print("Not enough coins");
        }
    }
    public void SwitchToBuff() {
        if (FindObjectOfType<Wallet>().currentBalance >= Prices[1]) {
            print("Bought buff");
            FindObjectOfType<Wallet>().RemoveMoney(Prices[1]);
            Invoke("EnableBuilding", .1f);
            CurrentTower = Turrets[3];
            ShadowTowerGreen = Turrets[4];
            ShadowTowerRed = Turrets[5];
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
