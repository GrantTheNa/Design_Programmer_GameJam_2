using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SandBox.Staging.OS_StateTesting
{
    public class PowerUpLevelManager : LevelManager 
    {
        [Header("Camera")]
        public CameraController camController;
        [SerializeField] private float waitBeforeCamMove = 3.0f;
        [SerializeField] private Transform endCameraPoint;
        [SerializeField] private float outOfViewTimer = 3.0f;

        private float timer;

        public override void OnLevelLoad()
        {
            base.OnLevelLoad();

            //set timer
            timer = outOfViewTimer;

            //set camera
            camController = Camera.main.GetComponent<CameraController>();
            camController.Invoke("BeginCameraMovement", waitBeforeCamMove);
            Debug.Log("cam moving in 3");
        }

        public override void OnLevelUnload()
        {
            //base.OnLevelUnload();
        }

        // Update is called once per frame
        void Update()
        {
            if (Camera.main.transform.position.z > endCameraPoint.position.z)
            {
                //Debug.Log("cam has passed end point");
                camController.EndCameraMovement();
                GameManager_v02.instance.GoToNextScene();
            }

            if (!playerController.isPlayerInView())
            {
                if (timer == outOfViewTimer) Debug.Log("timer has started");

                timer -= Time.deltaTime;
                if (timer < 0)
                {
                    Debug.Log("timer has run out");
                    camController.EndCameraMovement();
                    GameManager_v02.instance.GoToNextScene();
                }
            }
            else timer = outOfViewTimer;
        }
    }
}