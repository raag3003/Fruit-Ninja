using UnityEngine;

public class Blade : MonoBehaviour
{
    public float sliceForce = 5f;
    public float minSliceVelocity = 0.01f;

    private bool isTouching;
    private bool isNotTouching;
    private Camera mainCamera;
    private Collider sliceCollider;
    private TrailRenderer sliceTrail;

    public Vector3 direction { get; private set; }
    public bool slicing { get; private set; }

    private void Awake()
    {
        mainCamera = Camera.main;
        sliceCollider = GetComponent<Collider>();
        sliceTrail = GetComponentInChildren<TrailRenderer>();
    }

    private void OnEnable()
    {
        StopSlice();
    }

    private void OnDisable()
    {
        StopSlice();
    }

    private void Update()
    {
        isTouching = Input.touchCount > 0; // Sætter isTouching til true, hvis minimum en finger rører skærmen.
        isNotTouching = Input.touchCount == 0; // Sætter isNotTouching til true, hvis og kun hvis 0 fingere rører skærmen.

        if (Input.GetMouseButtonDown(0) || isTouching)
        {
            StartSlice();
        }
        else if (Input.GetMouseButtonUp(0) || isNotTouching)
        {
            StopSlice();
        }
        else if (slicing)
        {
            ContinueSlice();
        }
    }

    private void StartSlice()
    {
        if (Input.touchCount > 0)
        {
            // Vector3 position = mainCamera.ScreenToWorldPoint(Input.mousePosition); // Virker kun med musemakøren ikke fingre.
            Vector3 position = mainCamera.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, mainCamera.nearClipPlane));
            position.z = 0f;
            transform.position = position;

            slicing = true;
            sliceCollider.enabled = true;
            // sliceTrail.enabled = true;
            // sliceTrail.Clear();
        }
    }

    private void StopSlice()
    {
        slicing = false;
        sliceCollider.enabled = false;
        // sliceTrail.enabled = false;
    }

    private void ContinueSlice()
    {
        if (Input.touchCount > 0)
        {
            // Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition); // Virker kun med musemakøren ikke fingre.
            Vector3 newPosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, mainCamera.nearClipPlane));
            newPosition.z = 0f;
            direction = newPosition - transform.position;

            float velocity = direction.magnitude / Time.deltaTime;
            sliceCollider.enabled = velocity > minSliceVelocity;

            transform.position = newPosition;
        }
    }

}
