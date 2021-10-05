using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private bool blink;
    private Renderer rendererMat;
    [SerializeField] private Material red;
    [SerializeField] private Material white;
    [SerializeField] private int Health_;
    private void Awake() {
        rendererMat = GetComponent<Renderer>();
    }
    public void TakeDamage(int amount) {

        StartCoroutine(Blink());
        Health_-= amount;
    }
    private IEnumerator Blink() {
        rendererMat.material = white;
        yield return new WaitForSeconds(.1f);
        rendererMat.material = red;
    }
    private void Update() {
        if(Health_ <= 0) {
            FindObjectOfType<Wallet>().AddMoney(Random.Range(1, 6));
            Destroy(gameObject);
        }
    }
}