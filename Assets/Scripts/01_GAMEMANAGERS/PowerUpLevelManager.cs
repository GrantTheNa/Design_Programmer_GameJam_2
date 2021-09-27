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

    //===
    //NEW
    //===

    [Header("Teleporter")]
    [SerializeField] private GameObject teleporterPrefab;
    private GameObject teleporter;
    [SerializeField] private float teleporterSpeed = 4.0f;
    [SerializeField] private int numBlinks = 20;
    [SerializeField] private float timeBetweenBlinks = 0.05f;
    private bool blinkingStarted = false;

    [Header("Instructions")]
    [SerializeField] private GameObject instructionsCanvas;
    //===


    private float timer;

    public override void OnLevelLoad()
    {
        base.SwitchPlayerRenderer();
        base.OnLevelLoad();

        //===
        //NEW
        //===
        //instructions canvas
        instructionsCanvas.SetActive(false);

        

        //teleporter
        Vector3 teleporterStartPos = new Vector3(playerStartPos.position.x, 20, playerStartPos.position.z);
        teleporter = Instantiate(teleporterPrefab, teleporterStartPos, Quaternion.identity);
        //===

        //set timer
        timer = outOfViewTimer;

        //set camera
        camController = Camera.main.GetComponent<PU_CameraController>();
        //camController.Invoke("BeginCameraMovement", waitBeforeCamMove);

        playerStats.SetPowerUpLevelUI();
    }

    public override void OnLevelUnload()
    {
        //base.OnLevelUnload();
        playerStats.ResetCounters();
    }

    // Update is called once per frame
    void Update()
    {
        //NEW
        if (teleporter != null) TeleporterMovement();

        if (Camera.main.transform.position.z > endCameraPoint.position.z)
        {
            //Debug.Log("cam has passed end point");
            camController.EndCameraMovement();
            MASTER_GameManager.Instance.GoToNextScene();
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

    //NEW
    private void TeleporterMovement()
    {
        if (teleporter.transform.position.y > 8)
            teleporter.transform.Translate(Vector3.down * teleporterSpeed * Time.deltaTime);
        else if (!blinkingStarted)
        {
            teleporterSpeed = 0;
            //SwitchPlayerRenderer();
            base.SwitchPlayerRenderer();

            StartCoroutine(BlinkTeleporter());
            blinkingStarted = true;
        }
    }

    public void StartPowerUpLevel()
    {
        instructionsCanvas.SetActive(false);
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
        Debug.Log("cam moving in " + waitBeforeCamMove);
        Invoke("SetInstructionCanvasActive", waitBeforeCamMove);
    }

    //public override void SwitchPlayerRenderer()
    //{
    //    base.SwitchPlayerRenderer();
    //}

    //private void SwitchPlayerRenderer()
    //{
    //    var renderers = player.GetComponentsInChildren<Renderer>();
    //    if (renderers[1].enabled) renderers[1].enabled = !renderers[1].enabled;
    //    else renderers[1].enabled = true;
    //}
}