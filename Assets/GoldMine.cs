using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldMine : MonoBehaviour
{
    public bool canMine;
    private void Start() {
        RandomInvoke();
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
}
