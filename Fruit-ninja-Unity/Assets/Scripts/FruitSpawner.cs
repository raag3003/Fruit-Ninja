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
            float angle = i * (360 / fruitCount); // Divide the circle
            float radians = angle * Mathf.Deg2Rad;
            
            float x = Mathf.Cos(radians) * spawnRadius;
            float z = Mathf.Sin(radians) * spawnRadius;

            float randomHeight = Random.Range(0.2f, 1.0f); // toss height of the fruit

            Vector3 spawnPos = new Vector3(groundPos.x + x, groundPos.y + randomHeight, groundPos.z + z);
            GameObject fruit = Instantiate(fruitPrefab, spawnPos, Quaternion.identity);

            //Rotate outward from center
            Vector3 tossDirection = (spawnPos - groundPos).normalized;
            fruit.transform.rotation = Quaternion.LookRotation(tossDirection, Vector3.up);
        }
    }
}
