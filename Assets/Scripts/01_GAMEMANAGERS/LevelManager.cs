using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Transform playerStartPos;
    private GameObject player;
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
}