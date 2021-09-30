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
    private ShootBuilding[] shootBuildingControllers;
    private BuildingScript3D[] buildings;


    [Header("Game Over Prompt")]
    [SerializeField] private GameObject gameOverPrompt;



    //NEW -- IN LEVEL MANAGER
    [Header("Teleporter")]
    [SerializeField] private GameObject teleporterPrefab;
    private GameObject teleporter;
    [SerializeField] private float teleporterSpeed = 4.0f;
    [SerializeField] private int numBlinks = 20;
    [SerializeField] private float timeBetweenBlinks = 0.05f;
    private bool blinkingStarted = false;

    //NEW -- KEPT IN THIS CS
    [Header("Start / End UI")]
    [SerializeField] private GameObject instructionsCanvas;
    [SerializeField] private GameObject endLevelCanvas_allBuildingsDestroyed;
    [SerializeField] private GameObject endLevelCanvas_buildingsRemain;
    [SerializeField] private GameObject nextLevelBtn;

    public override void OnLevelLoad()
    {
        base.OnLevelLoad();

        //turn player and UI off
        base.SwitchPlayerRenderer();
        instructionsCanvas.SetActive(false);
        endLevelCanvas_allBuildingsDestroyed.SetActive(false);
        endLevelCanvas_buildingsRemain.SetActive(false);
        nextLevelBtn.SetActive(false);
        gameOverPrompt.SetActive(false);
        playerStats.SetDestroyWorldUI();

        //teleporter
        Vector3 teleporterStartPos = new Vector3(playerStartPos.position.x, 20, playerStartPos.position.z);
        teleporter = Instantiate(teleporterPrefab, teleporterStartPos, Quaternion.identity);

        //===

        //set timer
        levelTimer = levelTimerMax;

        //connect cinemachine with player
        cm = cinemachineGO.GetComponent<CinemachineVirtualCamera>();

        //buildings
        buildings = FindObjectsOfType<BuildingScript3D>();
        buildingCount = buildings.Length;
        buildingsNum.text = buildingCount.ToString();
        shootBuildingControllers = FindObjectsOfType<ShootBuilding>();

        //set Camera
        if (MASTER_GameManager.Instance.player != null)
        {
            var playerT = MASTER_GameManager.Instance.player.transform;
            cm.LookAt = playerT;
            cm.Follow = playerT;
        }
            
        playerStats.destroyLevelManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        //NEW

        if (teleporter != null) TeleporterMovement();

        //===

        levelTimer -= Time.deltaTime;
        timerText.text = ((int)levelTimer + 1).ToString();

        if (levelTimer < 0)
            EndLevelCanvasFail();
        //MASTER_GameManager.Instance.GoToNextScene();

        if (buildingCount == 0)
            EndLevelCanvasSuccess();
            //MASTER_GameManager.Instance.GoToNextScene();
    }

    private void EndLevelCanvasFail()
    {
        endLevelCanvas_buildingsRemain.SetActive(true);
        nextLevelBtn.SetActive(true);
        SetEnemyTargetToNull();
    }

    private void EndLevelCanvasSuccess()
    {
        endLevelCanvas_allBuildingsDestroyed.SetActive(true);
        nextLevelBtn.SetActive(true);
        SetEnemyTargetToNull();
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


    //NEW
    private void TeleporterMovement()
    {
        if (teleporter.transform.position.y > 8)
            teleporter.transform.Translate(Vector3.down * teleporterSpeed * Time.deltaTime);
        else if (!blinkingStarted)
        {
            teleporterSpeed = 0;
            base.SwitchPlayerRenderer();

            StartCoroutine(BlinkTeleporter());
            blinkingStarted = true;
        }
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
            //Debug.Log(i);
        }

        teleporterRenderer.enabled = true;

        Destroy(teleporter);
        SetInstructionCanvasActive();
    }

    private void SetInstructionCanvasActive()
    {
        instructionsCanvas.SetActive(true);
    }

    public void StartDestroyLevel()
    {
        instructionsCanvas.SetActive(false);
        base.ActivatePlayerMovement(true);
        SetEnemyTargetAsPlayer();
    }

    private void SetEnemyTargetAsPlayer()
    {
        if (MASTER_GameManager.Instance.player != null)
        {
            var playerT = MASTER_GameManager.Instance.player.transform;

            foreach (var shootBuildingScript in shootBuildingControllers)
            {
                shootBuildingScript.playerPos = playerT;
                Debug.Log("Player target set");
            }
        }
    }

    private void SetEnemyTargetToNull()
    {
        if (MASTER_GameManager.Instance.player != null)
        {
            foreach (var shootBuildingScript in shootBuildingControllers)
            {
                shootBuildingScript.playerPos = null;
                Debug.Log("Player target set to null");
            }
        }
    }

}