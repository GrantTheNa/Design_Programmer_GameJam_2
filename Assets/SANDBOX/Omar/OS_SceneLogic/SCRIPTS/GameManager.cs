using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SandBox.Staging.OS_SceneManagement
{
    public class GameManager : MonoBehaviour
    {
        #region Singelton

        public static GameManager instance;

        // Start is called before the first frame update
        void Awake()
        {
            instance = this;
        }

        #endregion

        [Header("Level")]
        [SerializeField] List<GameObject> levels;
        private int currentSceneIndex;

        [Header("Player")]
        [SerializeField] private GameObject player;
        private PlayerController playerController;
        [SerializeField] private Transform playerStartPoint;

        [Header("Camera")]
        private Camera cam;
        private CameraController camController;
        [SerializeField] private float waitBeforeNextScene = 3.0f;
        //[SerializeField] private Transform puCamStartPoint; //needed?
        //[SerializeField] private Transform puCamEndPoint;   //needed?
        //[SerializeField] private Transform hwCamStartPoint; //needed?


        //private bool levelActive = false;

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            
            playerController = player.GetComponent<PlayerController>();

            cam = Camera.main;
            camController = cam.GetComponent<CameraController>();
            
            currentSceneIndex = 0;
            //levelActive = true;
        }

        private void GoToNextLevel()
        {
            currentSceneIndex++;  
            SceneManager.LoadScene(currentSceneIndex);

            playerController.ResetPlayer(playerStartPoint);
            //levelActive = true;
        }


        // Update is called once per frame
        void Update()
        {
            if (cam == null) cam = Camera.main;            

            if (Input.GetKeyDown(KeyCode.Return))
            {
                //levelActive = false;
                GoToNextLevel();
            }

            //if (cam != null && levelActive == true && cam.transform.position.z > puCamEndPoint.position.z)
            //{
            //    Debug.Log("camera reached level end point");
                
            //    levelActive = false;
            //    camController.EndCameraMovement();
            //    Invoke("GoToNextLevel", waitBeforeNextScene);
            //}
        }
    }
}