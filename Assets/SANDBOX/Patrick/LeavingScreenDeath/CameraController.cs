using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LeavingScreenDeath
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float cameraSpeed;

        private Transform endPoint;


        // Start is called before the first frame update
        void Start()
        {
            endPoint = GameManager.instance.endPoint;
        }

        // Update is called once per frame
        void Update()
        {
            if (transform.position.z < endPoint.position.z)
            {
                transform.Translate(Vector3.right * cameraSpeed * Time.deltaTime);
            }
        }
    }
}