using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public int EnemiesAmount;
    public GameObject[] Enemy;
}
public class EnemySpawner : MonoBehaviour
{
    [HideInInspector]public bool canSpawn;
    [SerializeField]private Wave[] Waves;
    [SerializeField] private int CurrentWave;
    [SerializeField] private float SpawnTimer;
    [SerializeField] private float TimeBetweenWaves;
    [SerializeField] private Vector3 SpawnPos;
    // Start is called before the first frame update
    void Start()
    {
        canSpawn = true;
        CurrentWave = -1;
        Invoke("StartWaves", 3f);
    }
    private void StartWaves() {
        if(canSpawn){
            StartCoroutine(NextWave());
        }
    }
    private IEnumerator NextWave() {
        CurrentWave++;
        if(CurrentWave < Waves.Length) {
            print($"Spawning wave: {CurrentWave}");
            for (int i = 0; i < Waves[CurrentWave].EnemiesAmount; i++) {
                Instantiate(Waves[CurrentWave].Enemy[Random.Range(0, Waves[CurrentWave].Enemy.Length)], SpawnPos, Quaternion.identity);
                yield return new WaitForSeconds(SpawnTimer);
            }
            yield return new WaitForSeconds(TimeBetweenWaves);
            StartWaves();
        }
    }
}
