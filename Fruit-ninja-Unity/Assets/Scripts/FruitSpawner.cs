using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    public GameObject fruitPrefab;
    public int fruitCount = 5;
    public float spawnRadius = 2f;

    void Start()
    {
        SpawnFruits();
    }

    private void SpawnFruits()
    {
        Vector3 playerPos = Camera.main.transform.position;
        Vector3 groundPos = new Vector3(playerPos.x, playerPos.y - 0.2f, playerPos.z);

        for (int i = 0; i < fruitCount; i++)
        {
            float angle = 1 * Mathf.PI * 2 / fruitCount;
            float x = Mathf.Cos(angle) * spawnRadius;
            float z = Mathf.Sin(angle) * spawnRadius;

            Vector3 spawnPos = new Vector3(groundPos.x + x, groundPos.y, groundPos.z + z);
            Instantiate(fruitPrefab, spawnPos, Quaternion.identity);
        }
    }
}
