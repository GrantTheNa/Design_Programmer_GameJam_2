using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PU_CameraController : MonoBehaviour
{
    [SerializeField] private float cameraSpeed = 5.0f;
    private bool shouldCameraMove = false;

    // Update is called once per frame
    void Update()
    {
        if (shouldCameraMove) transform.Translate(Vector3.right * cameraSpeed * Time.deltaTime);
    }

    public void BeginCameraMovement()
    {
        shouldCameraMove = true;
    }

    public void EndCameraMovement()
    {
        shouldCameraMove = false;
    }
}