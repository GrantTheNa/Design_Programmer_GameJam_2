using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class DestroyLevelManager : LevelManager
{
    [Header("Cinemachine")]
    [SerializeField] GameObject cinemachineGO;
    private CinemachineVirtualCamera cm;

    [Header("Level Timer")]
    [SerializeField] private float levelTimerMax = 10.0f;
    [SerializeField] private TMP_Text timerText; 
    private float levelTimer;

    [Header("Buildings Count")]
    [SerializeField] private TMP_Text buildingsNum;
    [SerializeField] private int buildingCount = 0;

    [Header("Game Over Prompt")]
    [SerializeField] private GameObject gameOverPrompt;

    private ShootBuilding[] shootBuildingControllers;
    private BuildingScript3D[] buildings;


    public override void OnLevelLoad()
    {
        base.OnLevelLoad();
            
        //set timer
        levelTimer = levelTimerMax;

        //connect cinemachine with player
        cm = cinemachineGO.GetComponent<CinemachineVirtualCamera>();

        buildings = FindObjectsOfType<BuildingScript3D>();
        buildingCount = buildings.Length;
        buildingsNum.text = buildingCount.ToString();

        shootBuildingControllers = FindObjectsOfType<ShootBuilding>();

        if (MASTER_GameManager.Instance.player != null)
        {
            var playerT = MASTER_GameManager.Instance.player.transform;
            cm.LookAt = playerT;
            cm.Follow = playerT;

            foreach(var shootBuildingScript in shootBuildingControllers)
            {
                shootBuildingScript.playerPos = playerT;
            }
        }

        playerStats.destroyLevelManager = this;

        gameOverPrompt.SetActive(false);
        playerStats.SetDestroyWorldUI();
    }

    // Update is called once per frame
    void Update()
    {
        levelTimer -= Time.deltaTime;
        timerText.text = ((int)levelTimer + 1).ToString();

        if (levelTimer < 0) 
            MASTER_GameManager.Instance.GoToNextScene();

        if (buildingCount == 0)
            MASTER_GameManager.Instance.GoToNextScene();
    }

    public override void OnLevelUnload()
    {
        base.OnLevelUnload();
    }

    public void ReduceBuildingCount()
    {
        buildingCount--;
        buildingsNum.text = buildingCount.ToString();
    }

    public void SetGameOverPrompt()
    {
        gameOverPrompt.SetActive(true);
    }

}