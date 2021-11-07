using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOWTurret : MonoBehaviour
{
    [HideInInspector]public int Kills;
    private bool canSelect;
    public float shootRate = 0.2f;
    private string enemyTag = "Enemy";
    public int damageAmount;
    [HideInInspector] public bool isSelected;
    [SerializeField] private float range = 15f;
    [SerializeField] public Tower TowerObj;
    
    // Start is called before the first frame update
    private void Start() {
        Invoke("EnableSelect", 1f);
        shootRate = TowerObj.shootRate;
        enemyTag = TowerObj.enemyTag;
        InvokeRepeating("UpdateTarget", 0f, shootRate);
    }

    private void UpdateTarget() {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
        foreach (Collider obj in hitColliders)
        {
            if(obj.gameObject.CompareTag(enemyTag)){
                if(obj.GetComponent<Pathfinding>().AOWDamage){
                    obj.GetComponent<Pathfinding>().TakeDamage(this, damageAmount);
                }
            }
        }
    }
    public void AddKill(){
        Kills++;
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    private void EnableSelect() {
        canSelect = true;
    }
    public void DisableSelect() {
        isSelected = false;
    }
    public void SelectTurret() {
        //TODO UI STUFF WHEN SELECTED
        if (canSelect) {
            isSelected = !isSelected;
        }
    }
}
