using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SandBox.Staging.OS_StateTesting
{


    public class LevelManager : MonoBehaviour
    {
        [Header("Camera Controls")]
        public CameraController camController;
        //[SerializeField] private GameObject playerPrefab;
        [SerializeField] private Transform playerStartPos;
        //private GameObject player;
        private PlayerController playerController;
        
        // Start is called before the first frame update
        void Start()
        {
            GameManager_v02.instance.levelManager = this;
            Debug.Log("level manager set");

            camController = Camera.main.GetComponent<CameraController>();

            playerController = GameManager_v02.instance.playerController;

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
}
