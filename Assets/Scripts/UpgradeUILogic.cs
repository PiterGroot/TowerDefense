using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeUILogic : MonoBehaviour
{
    [HideInInspector] public bool isActive;
    [SerializeField]private SelectObj selectObj;
    [SerializeField] private Building buildingScript;
    [SerializeField] private Button damageButton, fireButton;
    [SerializeField]private TextMeshProUGUI Kills, KillsShadow;
    public void EnableUI() {
        isActive = true;
        if (selectObj.CurrentState.GetComponent<Upgrades>().hasDamage) {
            damageButton.interactable = false;
        }
        else {
            damageButton.interactable = true;
        }
        if (selectObj.CurrentState.GetComponent<Upgrades>().hasFirerate) {
            fireButton.interactable = false;
        }
        else {
            fireButton.interactable = true;
        }
        if(FindObjectOfType<Wallet>().currentBalance < selectObj.CurrentState.GetComponent<Upgrades>().damagePrice){
            damageButton.interactable = false;
        }
        if(FindObjectOfType<Wallet>().currentBalance < selectObj.CurrentState.GetComponent<Upgrades>().fireRatePrice){
            fireButton.interactable = false;
        }
    }
    public void SellCurentTower() {
        selectObj.DeselectAll();
        if(selectObj.State == 0) {
            //turret
            FindObjectOfType<Wallet>().AddMoney(75);
            PlaySound("Coin");
        }
        else if (selectObj.State == 1) {
            //aow
            FindObjectOfType<Wallet>().AddMoney(150);
            PlaySound("Coin");
        }
        else if (selectObj.State == 2) {
            //gold
            FindObjectOfType<Wallet>().AddMoney(225);
            PlaySound("Coin");
        }
        Destroy(selectObj.CurrentState);
    }
    public void MoreDamage() {
        if (selectObj.State == 0) {
            //turret
            if (FindObjectOfType<Wallet>().currentBalance >= selectObj.CurrentState.GetComponent<Upgrades>().damagePrice) {
                selectObj.CurrentState.GetComponent<Upgrades>().BuyDamage();
                damageButton.interactable = false;
                var newColorBlock = damageButton.colors;
                newColorBlock.disabledColor = Color.gray;
                damageButton.colors = newColorBlock;
                PlaySound("Coin");
            }
        }
    }
    public void MoreFireRate() {
        if (selectObj.State == 0) {
            //turret
            if (FindObjectOfType<Wallet>().currentBalance >= selectObj.CurrentState.GetComponent<Upgrades>().fireRatePrice) {
                selectObj.CurrentState.GetComponent<Upgrades>().BuyFireRate();
                fireButton.interactable = false;
                PlaySound("Coin");
            }
        }
    }
    private void Update() {
        if (isActive) {
            switch (selectObj.State) {
                case 0:
                    //turret
                    Kills.text = selectObj.CurrentState.GetComponent<Turret>().Kills.ToString();
                    KillsShadow.text = selectObj.CurrentState.GetComponent<Turret>().Kills.ToString();
                    break;
                case 1:
                    break;
                case 2:
                    break;
            }
        }
    }
    private void PlaySound(string name) {
        FindObjectOfType<AudioManager>().Play(name);
    }
}
