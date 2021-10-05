using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]private float StartHealth = 200;   
    [SerializeField]private float currentHealth;
    [SerializeField]private TextMeshProUGUI BalanceUI;
    [SerializeField]private bool sendAlerts;
    private void Awake() {
        SetHealth(StartHealth);
    }
    
    public void RemoveHealth(float amount){
        currentHealth -= amount;
        RefreshHealth();
    }
    public void AddHealth(float amount){
        currentHealth += amount;
        RefreshHealth();
    }
    public void SetHealth(float value){
        currentHealth = value;
        RefreshHealth();
    }
    private void RefreshHealth(){
        if(currentHealth > 0){
            PlayerPrefs.SetFloat("Health", currentHealth);
            BalanceUI.text = PlayerPrefs.GetFloat("Health").ToString();
            if(sendAlerts){
                print($"Health updated, Health: {currentHealth}");
            }
        }
        else{
            currentHealth = 0;
            Debug.LogWarning("Player died!");
            FindObjectOfType<EnemySpawner>().canSpawn = false;
            SceneManager.LoadScene("Mainmenu");
        }
    }
}
