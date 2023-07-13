using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemyPrefabs;
    [SerializeField]
    private GameObject bossPrefab;
    [SerializeField]
    private float spawnRange = 9;
    [SerializeField]
    private int enemiesToSpawn=3;
    [SerializeField]
    private GameObject[] powerUp;
    public int enemyCount;
    private int _bossRound = 2;
    private int _waveNumber;
    private bool _generate=true;

    private void Start()
    {
        SpawnEnemyWave(enemiesToSpawn);
        InvokeRepeating(nameof(SpawnRandomPowerUp),0,20f);
    }

    private void OnEnable()
    {
        PlayerController.OnGameOver += StopGeneration;
    }
    private void OnDisable()
    {
        PlayerController.OnGameOver -= StopGeneration;
    }

    private void Update()
    {
        if (_generate)
        {
            enemyCount = FindObjectsOfType<Enemy>().Length;

            if (enemyCount == 0)
            {
                ++_waveNumber;
                enemiesToSpawn++;
                SpawnEnemyWave(enemiesToSpawn);
                SpawnRandomPowerUp();

                if (_waveNumber % _bossRound == 0)
                {
                    SpawnBoss();
                }

            }
        }
    }
    

    private void StopGeneration()
    {
        _generate = false;
    }

    private void SpawnEnemyWave(int spawnCount)
    {
        for (int i = 0; i < spawnCount; i++)
        {
            int index = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[index], GenerateSpawnPoint(), enemyPrefabs[index].transform.rotation);
        }
    }

    private void SpawnBoss()
    {
        Instantiate(bossPrefab, GenerateSpawnPoint(), bossPrefab.transform.rotation);
    }
    
    private void SpawnRandomPowerUp()
    {
        int randomPowerUp = Random.Range(0, powerUp.Length);
        Instantiate(powerUp[randomPowerUp], GenerateSpawnPoint(), powerUp[randomPowerUp].transform.rotation);
    }

    private Vector3 GenerateSpawnPoint()
    {
        float randomX = Random.Range(-spawnRange, spawnRange);
        float randomZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(randomX, 0, randomZ);
        return randomPos;
    }
}
