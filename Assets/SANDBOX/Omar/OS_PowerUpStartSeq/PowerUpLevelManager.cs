using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Omar.PUSequenceLogic
{
    public class PowerUpLevelManager : MonoBehaviour
    {
        [Header("Camera")]
        public PU_CameraController camController;
        [SerializeField] private float waitBeforeCamMove = 10.0f;
        [SerializeField] private Transform endCameraPoint;
        [SerializeField] private float outOfViewTimer = 3.0f;

        [Header("Teleporter")]
        [SerializeField] private GameObject teleporterPrefab;
        private GameObject teleporter;
        [SerializeField] private float teleporterSpeed = 4.0f;
        [SerializeField] private int numBlinks = 20;
        [SerializeField] private float timeBetweenBlinks = 0.05f;
        private bool blinkingStarted = false;
        //[SerializeField] Transform teleporterStartPos; // get from level manager
        [SerializeField] Transform playerStartPos; // get from level manager

        [Header("Instructions")]
        [SerializeField] private GameObject instructionsCanvas;


        private float timer;

        // Start is called before the first frame update
        void Start()
        {
            //===
            //NEW
            //===
            instructionsCanvas.SetActive(false);
            
            Vector3 teleporterStartPos = new Vector3(playerStartPos.position.x, 20, playerStartPos.position.z);
            teleporter = Instantiate(teleporterPrefab, teleporterStartPos, Quaternion.identity);
            //===


            //set timer
            timer = outOfViewTimer;



            //set camera
            camController = Camera.main.GetComponent<PU_CameraController>();
            

            Debug.Log("cam moving in 10");
        }

        private IEnumerator BlinkTeleporter()
        {
            var teleporterRenderer = teleporter.GetComponent<Renderer>();

            int i = 0;
            while (i <= numBlinks)
            {
                teleporterRenderer.enabled = !teleporterRenderer.enabled;
                yield return new WaitForSeconds(timeBetweenBlinks);
                i++;
                Debug.Log(i);
            }

            teleporterRenderer.enabled = true;

            Destroy(teleporter);
            Invoke("SetInstructionCanvasActive", 2.0f);

            //instructionsCanvas.SetActive(true);
        }

        // Update is called once per frame
        void Update()
        {
            //move cylinder down
            
            if (teleporter != null) TeleporterMovement();
            


            if (Camera.main.transform.position.z > endCameraPoint.position.z)
            {
                //Debug.Log("cam has passed end point");
                camController.EndCameraMovement();
                MASTER_GameManager.Instance.GoToNextScene();
            }


            //PLAYER DIES CONDITION
            //if (!playerController.isPlayerInView())
            //{
            //    if (timer == outOfViewTimer) Debug.Log("timer has started");

            //    timer -= Time.deltaTime;
            //    if (timer < 0)
            //    {
            //        Debug.Log("timer has run out");
            //        camController.EndCameraMovement();
            //        MASTER_GameManager.Instance.GoToNextScene();
            //    }
            //}
            //else timer = outOfViewTimer;
        }

        private void TeleporterMovement()
        {
            if (teleporter.transform.position.y > 8)
                teleporter.transform.Translate(Vector3.down * teleporterSpeed * Time.deltaTime);
            else if (!blinkingStarted) //teleporter.transform.position.y <= 8
            {
                teleporterSpeed = 0;
                //Debug.Log("Coroutine started");

                StartCoroutine(BlinkTeleporter());
                blinkingStarted = true;
            }
        }

        public void StartPowerUpLevel()
        {
            instructionsCanvas.SetActive(false);
            camController.Invoke("BeginCameraMovement", waitBeforeCamMove);
        }

        private void SetInstructionCanvasActive()
        {
            instructionsCanvas.SetActive(true);
        }
    }

}
