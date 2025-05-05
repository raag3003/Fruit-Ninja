using System;
using UnityEngine;
using UnityEngine.Android;

public class CameraBackground : MonoBehaviour
{
    public Material woodMaterial;
    private WebCamTexture camTexture;

    void Start()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
            return;
        }

        WebCamDevice[] devices = WebCamTexture.devices;
        bool hasBackCamera = false;

        // Find the back-facing camera
        foreach (var device in devices)
        {
            if (!device.isFrontFacing) // Ensures only rear camera is used
            {
                camTexture = new WebCamTexture(device.name); // Assign correct camera
                GetComponent<Renderer>().material.mainTexture = camTexture;
                camTexture.Play();
                hasBackCamera = true;
                Debug.Log("There is a back-facing camara");
                break;
            }
        }
        if (!hasBackCamera)
        {
            GetComponent<Renderer>().material = woodMaterial;
            Debug.Log("No back-facing camera found");
        }
    }

    void OnDisable()
    {
        if (camTexture == null && camTexture.isPlaying)
        {
            camTexture.Stop();
        }
    }
}
