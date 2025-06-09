using System.Collections;
using UnityEngine;
// using UnityEngine.XR.ARFoundation;

public class FruitSpawner : MonoBehaviour
{
    public GameObject fruitPrefab;
    public int fruitCount = 5;
    public float spawnRadius = 10f;

    public float minForce = 18f;
    public float maxForce = 22f;

    // public ARPlaneManager planeManager;

    void Start()
    {
        SpawnFruits();
        //StartCoroutine(WaitForPlaneAndSpawn());
    }

    IEnumerator WaitForPlaneAndSpawn()
    {
        // Wait until at least one detected plane exist
        /*while (planeManager.trackables.count == 0)
        {
            yield return null;
        }*/

        // wait a little extra just to be safe
        yield return new WaitForSeconds(1f);

        
    }

    private void SpawnFruits()
    {
        Vector3 playerPos = Camera.main.transform.position;
        Vector3 groundPos = new Vector3(playerPos.x, playerPos.y - 3f, playerPos.z);

        for (int i = 0; i < fruitCount; i++)
        {
            float angle = i * (360 / fruitCount); // Divide the circle
            float radians = angle * Mathf.Deg2Rad;
            
            float x = Mathf.Cos(radians) * spawnRadius;
            float z = Mathf.Sin(radians) * spawnRadius;

            float randomHeight = Random.Range(0.2f, 1.0f); // toss height of the fruit

            Vector3 spawnPos = new Vector3(groundPos.x + x, groundPos.y + randomHeight, groundPos.z + z);
            GameObject fruit = Instantiate(fruitPrefab, spawnPos, Quaternion.identity);

            float force = Random.Range(minForce, maxForce);
            fruit.GetComponent<Rigidbody>().AddForce(fruit.transform.up * force, ForceMode.Impulse);

            /*
            //Rotate outward from center
            Vector3 tossDirection = (spawnPos - groundPos).normalized;
            fruit.transform.rotation = Quaternion.LookRotation(tossDirection, Vector3.up);
            */
        }
    }
}
