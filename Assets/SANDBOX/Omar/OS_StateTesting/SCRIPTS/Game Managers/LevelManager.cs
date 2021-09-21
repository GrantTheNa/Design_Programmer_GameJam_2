using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SandBox.Staging.OS_StateTesting
{


    public class LevelManager : MonoBehaviour
    {
        [Header("Player")]
        [SerializeField] private Transform playerStartPos;
        private GameObject player;
        public PlayerController playerController;
        
        // Start is called before the first frame update
        void Start()
        {
            GameManager_v02.instance.levelManager = this;
            //Debug.Log("level manager set");

            //camController = Camera.main.GetComponent<CameraController>();

            //assign player / create player if null
            player = GameManager_v02.instance.AssignPlayer();
            playerController = player.GetComponent<PlayerController>();
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
}
