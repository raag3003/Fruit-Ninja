using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

[RequireComponent(typeof(Collider))]
public class Spawner : MonoBehaviour
{
    private TrackingGPS gps;
    private Collider spawnArea;


    [Header("GameObjects")]
    public GameObject[] Emdrup;
    public GameObject[] fruitPrefabs;
    public GameObject bombPrefab;
    public GameObject[] ZooAnimals;

    [Header("Variables")]
    [Range(0f, 1f)]
    public float bombChance = 0.05f;

    public float minSpawnDelay = 0.25f;
    public float maxSpawnDelay = 1f;

    public float minAngle = -15f;
    public float maxAngle = 15f;

    public float minForce = 18f;
    public float maxForce = 22f;

    public float maxLifetime = 5f;
    public float spawnTime = 2f;

    public GameManager gameManager;
    public int lastDifficultyIncreaseScore = 0;

    private void Awake()
    {
        spawnArea = GetComponent<Collider>();
        gps = FindObjectOfType<TrackingGPS>();
    }

    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void Update()
    {


        if (gameManager.score > 0 && gameManager.score % 10 == 0 && gameManager.score != lastDifficultyIncreaseScore)
        {
            IncreaseDifficulty();
            lastDifficultyIncreaseScore = gameManager.score;
            Debug.Log("Difficulty Increased");
        }

    }
    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2f);

        while (enabled)
        {
            // Old version
            
            GameObject prefab;

            if (gps.IsWithinRadius(gps.userLat, gps.userLon, 55.72309f, 12.53921f, 1170))
            {
                prefab = Emdrup[Random.Range(0, Emdrup.Length)];
            } else
            {
                prefab = fruitPrefabs[Random.Range(0, fruitPrefabs.Length)];
            }

            if (Random.value < bombChance)
            {
                prefab = bombPrefab;
            }

            // old position version
            Vector3 position = new Vector3
            {
                x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
                y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y),
                z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z)
            };

            

            // old version
            Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(minAngle, maxAngle));

            GameObject fruit = Instantiate(prefab, position, rotation);
            Destroy(fruit, maxLifetime);

            float force = Random.Range(minForce, maxForce);
            fruit.GetComponent<Rigidbody>().AddForce(fruit.transform.up * force, ForceMode.Impulse);

            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
            



        }
    }

    void IncreaseDifficulty()
    {
        if(maxSpawnDelay > minSpawnDelay)
        {
            maxSpawnDelay = maxSpawnDelay - 0.3f;
        }
        
        if(bombChance < 0.34)
        {
            bombChance = bombChance + 0.1f;
        }
        

    }
}
