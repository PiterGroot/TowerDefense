using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private bool blink;
    private Renderer rendererMat;
    [SerializeField] private Material startMaterial;
    [SerializeField] private Material white;
    [SerializeField] public int Health_;
    private void Awake() {
        rendererMat = GetComponent<Renderer>();
        startMaterial = rendererMat.material;
    }
    public void TakeDamage(int amount) {
        StartCoroutine(Blink());
        Health_-= amount;
        UpdateHealth();
    }
    private IEnumerator Blink() {
        rendererMat.material = white;
        yield return new WaitForSeconds(.1f);
        rendererMat.material = startMaterial;
    }
    private void UpdateHealth() {
        if(Health_ <= 0) {
            FindObjectOfType<Wallet>().AddMoney(Random.Range(1, 6));
            Destroy(gameObject);
        }
    }
}
