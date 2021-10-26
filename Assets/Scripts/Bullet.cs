using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int DamageAmount;
    [HideInInspector]public Shooting ParentTower;
    [SerializeField] private float DieTimer;
    private void Awake() {
        Invoke("Die", DieTimer);
    }
    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.CompareTag("Enemy")) {
            collision.collider.GetComponent<Health>().TakeDamage(DamageAmount);
            if(collision.gameObject.GetComponent<Health>().Health_ <= 0){
                ParentTower.AddKill();
            }
            Die();
        }
        else {
            Die(); 
        }
    }
    private void Die() {
        Destroy(gameObject);
    }
}
