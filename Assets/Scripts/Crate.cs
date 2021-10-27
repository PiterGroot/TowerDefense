using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    [HideInInspector]public bool isAttatched;
    [SerializeField] private Vector3 Spawnpos;
    [SerializeField] private ParticleSystem explosion;
    private Transform targetTransform;
    private bool canAttatch;
    private Rigidbody rb;
    private void Start() {
        Spawnpos = new Vector3(3.1f, 35, -67f);
        rb = gameObject.GetComponent<Rigidbody>();
        //rb.mass = 100f;
        canAttatch = true;
    }
    public void Attatch(Transform transform) {
        if (canAttatch) {
            canAttatch = false;
            FindObjectOfType<AudioManager>().Play("Crate");
            targetTransform = transform;
            isAttatched = true;
        }
    }
    public void Detatch() {
        FindObjectOfType<Rope>().isGrappled = false;
        FindObjectOfType<Rope>().isDetaching = true;
        Invoke("DetachingUI", 0.5f);
        canAttatch = false;
        isAttatched = false;
        rb.velocity = new Vector3(0, -30, 0);
        Invoke("CanAttach", .75f);
    }
    private void DetachingUI(){
        FindObjectOfType<Rope>().isEmpty = true;
        FindObjectOfType<Rope>().isDetaching = true;
        FindObjectOfType<Rope>().isGrappled = false;
    }
    private void CanAttach() {
        canAttatch = true;
        FindObjectOfType<Rope>().isDetaching = false;
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.collider.name == "CrateDeposit") {
            print("Crate recevied");
            Instantiate(explosion, Spawnpos, Quaternion.identity);
            FindObjectOfType<Rope>().isEmpty = true;
            FindObjectOfType<Rope>().isDetaching = true;
            FindObjectOfType<Rope>().isGrappled = false;
            Destroy(gameObject);
        } 
    }
    // Update is called once per frame
    void Update()
    {
        if (isAttatched) {
            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                Detatch();
            }
            transform.position = targetTransform.position;
        }
    }
}
