using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateSpawner : MonoBehaviour
{
    [SerializeField]private GameObject Crate;
    public Vector2 spawnBorderX;
    public Vector2 spawnBorderZ;
    public float yPos;

    public void SpawnCrate(){
        float xPos = Random.Range(spawnBorderX.x, spawnBorderX.y);
        float zPos = Random.Range(spawnBorderZ.x, spawnBorderZ.y);
        Instantiate(Crate, new Vector3(xPos, yPos, zPos), Quaternion.identity);
    }
}
