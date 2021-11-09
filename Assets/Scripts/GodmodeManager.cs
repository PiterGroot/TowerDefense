using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodmodeManager : MonoBehaviour
{
    public bool isGodmode;
    [SerializeField] private Animator GodAnim;
    private int state;
    // Update is called once per frame
    private void Awake() {
        PlayerPrefs.DeleteKey("DroneBoost");
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl)) {
            if (Input.GetKeyDown(KeyCode.G)) {
                state++;
                if(state == 1) {
                    print("GODMODE HEHEHE");
                    transform.GetChild(0).gameObject.SetActive(true);
                    isGodmode = true;
                    GodAnim.gameObject.SetActive(true);
                    PlayerPrefs.SetInt("DroneBoost", 500);
                    FindObjectOfType<Wallet>().SetMoney(5000);
                    FindObjectOfType<PlayerHealth>().SetHealth(500);
                }
                if(state == 2) {
                    state = 0;
                    isGodmode = false;
                    PlayerPrefs.SetInt("DroneBoost", 0);
                    GodAnim.gameObject.SetActive(false);
                }
            }
        }
    }
}
