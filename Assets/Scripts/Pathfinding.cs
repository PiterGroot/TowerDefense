using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    private Tower AOWSettings;
    private bool CanMove;
    public bool AOWDamage;
    [SerializeField] List<Transform> Waypoints = new List<Transform>();
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveSpeed1;
    [SerializeField] private Transform CurrentWayPoint;
    [SerializeField] private int WaypointCounter;
    private void Awake() {
        AOWSettings = Resources.Load<Tower>("Tower/Normal");
    }
    // Start is called before the first frame update
    private void Start(){
        foreach (Transform transform in GameObject.Find("Waypoints").GetComponentInChildren<Transform>()){
            Waypoints.Add(transform);
        }
        GotoWayPoint();
        CanMove = true;
    }
    private void NextWaypoint() {
        WaypointCounter++;
        GotoWayPoint();
    }
    private void GotoWayPoint() {
        if (CheckArray()) {
            CurrentWayPoint = Waypoints[WaypointCounter];
        }
    }
    private bool CheckArray() {
        bool result = false;
        if (WaypointCounter >= Waypoints.Count && CanMove) {
            CanMove = false;
            result = false;
            FindObjectOfType<PlayerHealth>().RemoveHealth(Random.Range(1, 6));
            Destroy(gameObject);
        }
        else {
            result = true;
        }
        return result;
    }
    private void Update() {
        if (CanMove) {
            float dist = Vector3.Distance(transform.position, CurrentWayPoint.transform.position);
            if (dist <= .1f) {
                NextWaypoint();
            }
            else {
                var targetRotation = Quaternion.LookRotation(CurrentWayPoint.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, moveSpeed1 * Time.deltaTime);
                transform.position += transform.forward * Time.deltaTime * moveSpeed;
            }
        }
    }
    private void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("AOW")) {
            AOWDamage = true;
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("AOW")){
            AOWDamage = false;
        }
    }
    public void TakeDamage() {
        if(AOWDamage){
            GetComponent<Health>().TakeDamage(3);
        }
    }

}
