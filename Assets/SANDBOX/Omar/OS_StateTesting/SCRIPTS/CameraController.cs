using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SandBox.Staging.OS_StateTesting
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float cameraSpeed = 5.0f;
        private bool shouldCameraMove = false;

        // Start is called before the first frame update
        void Start()
        {

        }

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

        //public void ResetCamera(Transform camStartPoint)
        //{
        //    transform.position = camStartPoint.position;
        //}
    }
}