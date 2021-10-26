using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    private Turret turret;
    private AOWTurret AOWturret;
    private GoldMine Goldmine;
    [SerializeField] private int state;
    public bool hasDamage;
    public bool hasFirerate;
    public float damagePrice;
    public float fireRatePrice;
    private void Awake() {
        switch (state) {
            case 0:
                //turret
                turret = gameObject.GetComponent<Turret>();
                break;
            case 1:
                //aowturret
                AOWturret = gameObject.GetComponent<AOWTurret>();
                break;
            case 2:
                //goldmine
                Goldmine = gameObject.GetComponent<GoldMine>(); 
                break;
        }
    }
    public void BuyDamage() {
        print("BUYING DAMAGE");
        hasDamage = true;
        FindObjectOfType<Wallet>().RemoveMoney(damagePrice);
        if(state == 0) {
            turret.damageAmount += 4;
            turret.EnableScope();
        }
    }
    public void BuyFireRate() {
        print("BUYING FIRERATE");
        hasFirerate = true;
        FindObjectOfType<Wallet>().RemoveMoney(fireRatePrice);
        if (state == 0) {
            turret.EnableScope();
            turret.shootRate = 0.3f;
        }
    }
}
