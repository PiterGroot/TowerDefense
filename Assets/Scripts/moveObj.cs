using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveObj : MonoBehaviour
{
    private bool Direction;
    [SerializeField] private float timer = 6f;
    [SerializeField]private float moveSpeed;
    private void Awake() {
        InvokeRepeating("ChangeDirection", 0f, timer);
    }
    // Update is called once per frame
    void Update(){
        if (Direction) {
            //right
            transform.position += new Vector3(0, 0, moveSpeed * Time.deltaTime);
        }
        else {
            //left
            transform.position += new Vector3(0, 0, -moveSpeed * Time.deltaTime);
        }
    }
    private void ChangeDirection() {
        Direction = !Direction;
    }
}
