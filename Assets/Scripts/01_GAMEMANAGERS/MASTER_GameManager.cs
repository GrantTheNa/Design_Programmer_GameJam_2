using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MASTER_GameManager : MonoBehaviour
{
    #region Singelton

    private static MASTER_GameManager _instance;

    public static MASTER_GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("MASTER_GameManager");
                go.AddComponent<MASTER_GameManager>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
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

        //if (player == null)
        //    player = Resources.Load("TEMP_PLAYER") as GameObject;
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
            playerPrefab = Resources.Load("TEMP_PLAYER") as GameObject;
            player = Instantiate(playerPrefab);
            Debug.Log("Player created");
        }

        return player;
    }

    //ORIGINAL SINGLETON
    //#region Singelton

    //public static MASTER_GameManager instance;

    //// Start is called before the first frame update
    //void Awake()
    //{
    //    instance = this;
    //}

    //#endregion

}