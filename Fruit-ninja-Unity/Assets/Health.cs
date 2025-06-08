using UnityEngine;

public class Health : MonoBehaviour
{
    public GameManager hp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fruit"))
        {
            hp.health = hp.health - 1;
            Debug.Log("lost 1 health");
        }
    }
}
