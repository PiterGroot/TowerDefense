using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{   
    public bool isEmpty;
    public bool isGrappled;
    public bool isDetaching;
    [SerializeField] private Transform anchorPoint;
    [SerializeField]private GameObject grappled, detaching, empty;
    private void Start() {
        isGrappled = false;
        isDetaching = false;
        isEmpty = true;
    }
    private void OnCollisionEnter(Collision collision) {
        if(collision.collider.CompareTag("Crate")) {
            if (!isGrappled) {
                collision.collider.GetComponent<Crate>().Attatch(anchorPoint);
                isEmpty = false;
                isDetaching = false;
                isGrappled = true;
            }
        }
    }
    private void Update() {
        if(isDetaching){
            detaching.SetActive(true);
            grappled.SetActive(false);
            empty.SetActive(false);
        }
        if(isGrappled){
            grappled.SetActive(true);
            detaching.SetActive(false);
            empty.SetActive(false);
        }
        if(isEmpty){
            grappled.SetActive(false);
            detaching.SetActive(false);
            empty.SetActive(true);
        }

    }
}
