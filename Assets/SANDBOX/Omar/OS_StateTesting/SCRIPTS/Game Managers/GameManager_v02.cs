using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace SandBox.Staging.OS_StateTesting
{
    public class GameManager_v02 : MonoBehaviour
    {
        #region Singelton

        public static GameManager_v02 instance;

        // Start is called before the first frame update
        void Awake()
        {
            instance = this;
        }

        #endregion

        private int sceneIndex;
        private int nextSceneIndex;

        public LevelManager levelManager;
        //public GameObject player;
        public PlayerController playerController;

        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(gameObject);
            
            sceneIndex = SceneManager.GetActiveScene().buildIndex;
            nextSceneIndex = sceneIndex + 1;
        }

        public void GoToNextScene()
        {
            if (levelManager != null)
            {
                levelManager.OnLevelUnload();
                levelManager = null;
                Debug.Log("level manager set to null");
            }
            
            //go to next scene
            SceneManager.LoadScene(nextSceneIndex);
            sceneIndex = nextSceneIndex;
            nextSceneIndex = sceneIndex + 1;   
        }


        // Update is called once per frame
        void Update()
        {

        }
    }
}