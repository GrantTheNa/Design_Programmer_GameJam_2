using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] public Transform playerStartPos;
    public GameObject player;
    public PlayerController playerController;
    public PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        MASTER_GameManager.Instance.levelManager = this;
        //Debug.Log("level manager set");

        //assign player / create player if null
        player = MASTER_GameManager.Instance.AssignPlayer();
        playerController = player.GetComponent<PlayerController>();
        playerStats = player.GetComponent<PlayerStats>();
        //if (player != null) Debug.Log("player set");
        //if (playerController != null) Debug.Log("playerController set");

        OnLevelLoad();
    }

    public virtual void OnLevelLoad()
    {
        playerController.ResetPlayer(playerStartPos);
    }

    public virtual void OnLevelUnload()
    {

    }

    public virtual void SwitchPlayerRenderer()
    {
        var renderers = player.GetComponentsInChildren<Renderer>();
        if (renderers[1].enabled) renderers[1].enabled = !renderers[1].enabled;
        else renderers[1].enabled = true;
    }

    public virtual void RestartGame()
    {
        MASTER_GameManager.Instance.GoToLevelOne();
        Debug.Log("lvl mgr - Restart game called");
    }

    public virtual void GoToHighScoreScreen()
    {
        MASTER_GameManager.Instance.GoToHighScoresScreen();
        Debug.Log("lvl mgr - High score screen called");
    }

    public virtual void GoToMenuScreen()
    {
        MASTER_GameManager.Instance.GoToMenuScreen();
        Debug.Log("lvl mgr - menu screen called");
    }

    public virtual void Quit()
    {
        MASTER_GameManager.Instance.QuitGame();
        Debug.Log("lvl mgr - quit game called");
    }
}