using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldMine : MonoBehaviour
{
    private bool canSelect;
    [HideInInspector] public bool isSelected;
    public bool canMine;
    private void Start() {
        RandomInvoke();
        Invoke("EnableSelect", 1f);
    }
    private void RandomInvoke(){
        if(canMine){
            Invoke("MineGold", Random.Range(5, 11));
        }
    }
    private void MineGold(){
        FindObjectOfType<Wallet>().AddMoney(25);
        RandomInvoke();
    }
    public void SelectTurret() {
        //TODO UI STUFF WHEN SELECTED
        if (canSelect) {
            isSelected = !isSelected;
        }
    }
    private void EnableSelect() {
        canSelect = true;
    }
    public void DisableSelect() {
        isSelected = false;
        transform.GetChild(1).gameObject.SetActive(false);
    }
}
