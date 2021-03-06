using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

namespace SandBox.Staging.OS_StateTesting
{
    public class DestroyLevelManager : LevelManager
    {
        [Header("Cinemachine")]
        [SerializeField] GameObject cinemachineGO;
        private CinemachineVirtualCamera cm;

        [Header("Level Timer")]
        [SerializeField] private float levelTimerMax = 10.0f;
        [SerializeField] private TMP_Text timerText; 
        private float levelTimer;

        public override void OnLevelLoad()
        {
            base.OnLevelLoad();
            
            //set timer
            levelTimer = levelTimerMax;

            //connect cinemachine with player
            cm = cinemachineGO.GetComponent<CinemachineVirtualCamera>();

            if (GameManager_v02.instance.player != null)
            {
                var playerT = GameManager_v02.instance.player.transform;
                cm.LookAt = playerT;
                cm.Follow = playerT;
            }
        }

        // Update is called once per frame
        void Update()
        {
            levelTimer -= Time.deltaTime;
            timerText.text = ((int)levelTimer + 1).ToString();

            if (levelTimer < 0) GameManager_v02.instance.GoToNextScene();
        }

        public override void OnLevelUnload()
        {
            base.OnLevelUnload();
        }
    }
}