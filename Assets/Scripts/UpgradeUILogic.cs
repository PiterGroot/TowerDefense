using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeUILogic : MonoBehaviour
{
    public void SellCurentTower() {
        FindObjectOfType<SelectObj>().DeselectAll();
        if(FindObjectOfType<SelectObj>().State == 0) {
            //turret
            FindObjectOfType<Wallet>().AddMoney(75);
            PlaySound("Coin");
        }
        else if (FindObjectOfType<SelectObj>().State == 1) {
            //aow
            FindObjectOfType<Wallet>().AddMoney(150);
            PlaySound("Coin");
        }
        else if (FindObjectOfType<SelectObj>().State == 2) {
            //gold
            FindObjectOfType<Wallet>().AddMoney(225);
            PlaySound("Coin");
        }
        Destroy(FindObjectOfType<SelectObj>().CurrentState);
    }

    private void PlaySound(string name) {
        FindObjectOfType<AudioManager>().Play(name);
    }
}
