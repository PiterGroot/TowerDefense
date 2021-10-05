using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float DieTimer;
    private void Awake() {
        Invoke("Die", DieTimer);
    }
    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.CompareTag("Enemy")) {
            collision.collider.GetComponent<Health>().TakeDamage(Random.Range(1, 6));
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
