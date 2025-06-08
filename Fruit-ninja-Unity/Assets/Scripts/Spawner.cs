using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

[RequireComponent(typeof(Collider))]
public class Spawner : MonoBehaviour
{
    private TrackingGPS gps;
    private Collider spawnArea;

    private Transform arCamera; // For the transform of the phone
    public float spawnRadius = 2f; // distance from player

    public GameObject[] Emdrup;
    public GameObject[] fruitPrefabs;
    public GameObject bombPrefab;
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
        arCamera = Camera.main.transform;
    }

    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2f);

        while (enabled)
        {
            GameObject prefab;

            // makes the fruit spawn in a circle around the player
            float angle = Random.Range(0f, Mathf.PI * 2f);
            Vector3 offset = new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) * spawnRadius;

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

            /*
            Vector3 position = new Vector3
            {
                

                 // The old spawn postion
                x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
                y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y),
                z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z)
                
            };
            */
            // New spawn positon
            Vector3 spawnPosition = new Vector3(
                arCamera.position.x + offset.x,
                arCamera.position.y - 0.2f,
                arCamera.position.z + offset.z
                );

            Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(minAngle, maxAngle));

            GameObject fruit = Instantiate(prefab, spawnPosition, rotation);
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
            maxSpawnDelay = maxSpawnDelay - 0.1f;
            bombChance = bombChance + 0.01f;
        }

    }
}
