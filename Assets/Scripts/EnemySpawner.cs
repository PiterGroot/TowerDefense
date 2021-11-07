using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Wave
{
    public int EnemiesAmount;
    public GameObject[] Enemy;
}
public class EnemySpawner : MonoBehaviour
{
    private bool spawnNewWave;
    public bool isSpawning;
    [HideInInspector]public bool canSpawn;
    [SerializeField]private Wave[] Waves;
    [SerializeField] private int CurrentWave;
    [SerializeField] private float SpawnTimer;
    [SerializeField] private float TimeBetweenWaves;
    [SerializeField] private Vector3 SpawnPos;
    [SerializeField] private TextMeshProUGUI WavetText, ShadowWaveText, isSpawningtext, isSpawningtextShadow;
    // Start is called before the first frame update
    void Start()
    {
        WavetText.text = "0";
        ShadowWaveText.text = "0";
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
        WavetText.text = CurrentWave.ToString();
        ShadowWaveText.text = CurrentWave.ToString();
        if (CurrentWave < Waves.Length) {
            print($"Spawning wave: {CurrentWave}");
            int randint = Random.Range(1, 6);
            spawnNewWave = false;
            switch(randint){
                case 1:
                break;
                case 2:
                FindObjectOfType<CrateSpawner>().SpawnCrate();
                break;
                case 3:
                FindObjectOfType<CrateSpawner>().SpawnCrate();
                FindObjectOfType<CrateSpawner>().SpawnCrate();
                break;
                case 4:
                FindObjectOfType<CrateSpawner>().SpawnCrate();
                FindObjectOfType<CrateSpawner>().SpawnCrate();
                FindObjectOfType<CrateSpawner>().SpawnCrate();
                break;
                case 5:
                break;
            }
            isSpawning = true;
            isSpawningtext.text = isSpawning.ToString().ToUpper();
            isSpawningtextShadow.text = isSpawning.ToString().ToUpper();
            isSpawningtext.color = Color.green;
            for (int i = 0; i < Waves[CurrentWave].EnemiesAmount; i++) {
                Instantiate(Waves[CurrentWave].Enemy[Random.Range(0, Waves[CurrentWave].Enemy.Length)], SpawnPos, Quaternion.identity);
                yield return new WaitForSeconds(SpawnTimer);
            }
            isSpawning = false;
            isSpawningtext.text = isSpawning.ToString().ToUpper();
            isSpawningtextShadow.text = isSpawning.ToString().ToUpper();
            isSpawningtext.color = Color.red;
            //yield return new WaitForSeconds(TimeBetweenWaves);
            yield return new WaitUntil(() => spawnNewWave == true);
            StartWaves();
        }
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.R) && !isSpawning){
            spawnNewWave = true;
        }
    }
}
