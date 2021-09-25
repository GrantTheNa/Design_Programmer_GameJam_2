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
            //playerPrefab = Resources.Load("TEMP_PLAYER") as GameObject;
            playerPrefab = Resources.Load("player") as GameObject;
            //playerPrefab = Resources.Load("Pat_Player") as GameObject;
            player = Instantiate(playerPrefab);
            Debug.Log("Player created");
        }

        return player;
    }

    public void GoToGameOverScene()
    {
        SceneManager.LoadScene("09_GameOver");
    }

    public void GoToLevelOne()
    {
        SceneManager.LoadScene(1);
        Debug.Log("Master Game Manager - scene 1 loaded");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToHighScoresScreen()
    {
        SceneManager.LoadScene(7);
        Debug.Log("Master Game Manager - highscore screen loaded");
    }

    public void GoToMenuScreen()
    {
        SceneManager.LoadScene(0);
        Debug.Log("Master Game Manager - menu screen loaded");
    }
}