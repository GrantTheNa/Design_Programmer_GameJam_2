using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace SandBox.Staging.OS_StateTesting
{

    //enum states

    //create player if player == null

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

        public GameObject player;
        [SerializeField] private GameObject playerPrefab;

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


        public GameObject AssignPlayer()
        {
            if (player == null)
            {
                player = Instantiate(playerPrefab);
                Debug.Log("Player created");
            }

            return player;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}