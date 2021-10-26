using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private int damageAmount;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float timer = 0.3f;
    public float bulletForce = 20f;
    private Transform target;
    
    public void Shoot(Transform target, int damage){
        this.target = target;
        damageAmount = damage;
        Invoke("Commit", timer);
    }

    public void Commit(){
        if(target != null) {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            bullet.GetComponent<Bullet>().ParentTower = this;
            bullet.GetComponent<Bullet>().DamageAmount = damageAmount;
            rb.AddForce(firePoint.forward * bulletForce, ForceMode.VelocityChange);
        }
    }
    public void AddKill(){
        gameObject.GetComponent<Turret>().Kills++;
    }
}
