using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpLevelManager : LevelManager
{
    [Header("Camera")]
    public PU_CameraController camController;
    [SerializeField] private float waitBeforeCamMove = 3.0f;
    [SerializeField] private Transform endCameraPoint;
    [SerializeField] private float outOfViewTimer = 3.0f;

    [Header("Teleporter")]
    [SerializeField] private GameObject teleporterPrefab;
    private GameObject teleporter;
    [SerializeField] private float teleporterSpeed = 4.0f;
    [SerializeField] private int numBlinks = 20;
    [SerializeField] private float timeBetweenBlinks = 0.05f;
    private bool blinkingStarted = false;

    [Header("Start / End UI")]
    [SerializeField] private GameObject instructionsCanvas;
    [SerializeField] private GameObject endLevelCanvas;

    private float timer;

    public override void OnLevelLoad()
    {
        base.SwitchPlayerRenderer();
        base.OnLevelLoad();
        
        //UI canvas
        instructionsCanvas.SetActive(false);
        endLevelCanvas.SetActive(false);

        //teleporter
        Vector3 teleporterStartPos = new Vector3(playerStartPos.position.x, 20, playerStartPos.position.z);
        teleporter = Instantiate(teleporterPrefab, teleporterStartPos, Quaternion.identity);

        //set timer
        timer = outOfViewTimer;

        //set camera
        camController = Camera.main.GetComponent<PU_CameraController>();

        //player UI
        playerStats.SetPowerUpLevelUI();
    }

    public override void OnLevelUnload()
    {
        playerStats.ResetCounters();
        base.OnLevelUnload();
    }

    // Update is called once per frame
    void Update()
    {
        if (teleporter != null) TeleporterMovement();

        if (Camera.main.transform.position.z > endCameraPoint.position.z)
        {
            //Debug.Log("cam has passed end point");
            camController.EndCameraMovement();
            endLevelCanvas.SetActive(true);
            //MASTER_GameManager.Instance.GoToNextScene();
        }

        if (!playerController.isPlayerInView())
        {
            if (timer == outOfViewTimer) Debug.Log("timer has started");

            timer -= Time.deltaTime;
            if (timer < 0)
            {
                Debug.Log("timer has run out");
                camController.EndCameraMovement();
                MASTER_GameManager.Instance.GoToNextScene();
            }
        }
        else timer = outOfViewTimer;
    }

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

    public void StartPowerUpLevel()
    {
        instructionsCanvas.SetActive(false);
        base.ActivatePlayerMovement(true);
        camController.Invoke("BeginCameraMovement", waitBeforeCamMove);
    }

    private void SetInstructionCanvasActive()
    {
        instructionsCanvas.SetActive(true);
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
        SetInstructionCanvasActive();
    }
}