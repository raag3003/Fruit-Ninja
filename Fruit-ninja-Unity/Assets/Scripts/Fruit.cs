
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameObject whole;
    public GameObject sliced;

    public Spawner spawner;

    private Rigidbody fruitRigidbody;
    private Collider fruitCollider;
    private ParticleSystem juiceEffect;

    public int points = 1;

    private void Awake()
    {
        fruitRigidbody = GetComponent<Rigidbody>();
        fruitCollider = GetComponent<Collider>();
        juiceEffect = GetComponentInChildren<ParticleSystem>();
    }

    private void Slice(Vector3 direction, Vector3 position, float force)
    {
        GameManager.Instance.IncreaseScore(points);

        // Disable the whole fruit
        fruitCollider.enabled = false;
        whole.SetActive(false);

        // Enable the sliced fruit
        sliced.SetActive(true);
        juiceEffect.Play();

        // Rotate the sliced parent to match the slice angle
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        sliced.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Rigidbody[] slices = sliced.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody slice in slices)
        {
            // Copy velocity from whole fruit
            slice.linearVelocity = fruitRigidbody.linearVelocity;

            // Add slice force at cut position
            slice.AddForceAtPosition(direction * force, position, ForceMode.Impulse);

            // Add angular velocity to spin slice
            Vector3 sliceToCut = slice.position - position;
            Vector3 torqueAxis = Vector3.Cross(direction.normalized, sliceToCut.normalized);
            float spinStrength = force * 5f;  // tweak this multiplier for spin intensity
            slice.angularVelocity = torqueAxis * spinStrength;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Blade blade = other.GetComponent<Blade>();
            Slice(blade.direction, blade.transform.position, blade.sliceForce);
        }
    }

}
