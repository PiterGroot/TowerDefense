using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{   
    [HideInInspector]public bool isEmpty;
    [HideInInspector]public bool isGrappled;
    [HideInInspector]public bool isDetaching;
    [SerializeField] private Transform anchorPoint;
    [SerializeField]private GameObject grappled, detaching, empty;
    private void Start() {
        isGrappled = false;
        isDetaching = false;
        isEmpty = true;
    }
    private void OnCollisionEnter(Collision collision) {
        if(collision.collider.CompareTag("Crate")) {
            collision.collider.GetComponent<Crate>().Attatch(anchorPoint);
            isEmpty = false;
            isGrappled = true;
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
