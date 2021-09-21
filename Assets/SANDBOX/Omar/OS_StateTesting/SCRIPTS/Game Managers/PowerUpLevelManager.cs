using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SandBox.Staging.OS_StateTesting
{
    public class PowerUpLevelManager : LevelManager 
    {
        [SerializeField] private float waitBeforeCamMove = 3.0f;
        [SerializeField] private Transform endCameraPoint;

        public override void OnLevelLoad()
        {
            base.OnLevelLoad();
            camController.Invoke("BeginCameraMovement", waitBeforeCamMove);
            Debug.Log("cam moving in 3");

            // to stuff
            
            // do stuff
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
                Debug.Log("cam has passed end point");
                camController.EndCameraMovement();
                GameManager_v02.instance.GoToNextScene();
            }
        }
    }
}