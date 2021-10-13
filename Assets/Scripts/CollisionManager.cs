using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    [SerializeField]private GameObject[] Colliders;

    // Update is called once per frame
    private void Update()
    {
        for (int i = 0; i < Colliders.Length; i++)
        {
            if(Colliders[i].transform.position.y+1 > transform.position.y){
                transform.Translate(0, 1, 0, Space.World);
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.name == "Tower"){
            transform.position += -transform.forward * 3;
        }
    }
}
