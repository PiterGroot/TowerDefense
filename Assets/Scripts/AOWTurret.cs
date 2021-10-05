using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOWTurret : MonoBehaviour
{
    private bool canSelect;
    [HideInInspector] public bool isSelected;
    private float shootRate = 0.2f;
    private Transform Target;
    private float moveSpeed = 4f;
    [SerializeField] private float range = 15f;
    private string enemyTag = "Enemy";
    [SerializeField] public Tower TowerObj;
    
    // Start is called before the first frame update
    private void Start() {
        Invoke("EnableSelect", 1f);
        shootRate = TowerObj.shootRate;
        moveSpeed = TowerObj.moveSpeed;
        enemyTag = TowerObj.enemyTag;
        InvokeRepeating("UpdateTarget", 0f, shootRate);
    }

    private void UpdateTarget() {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
        foreach (Collider obj in hitColliders)
        {
            if(obj.gameObject.CompareTag("Enemy")){
                if(obj.GetComponent<Pathfinding>().AOWDamage){
                    obj.GetComponent<Pathfinding>().TakeDamage();
                }
            }
        }
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
