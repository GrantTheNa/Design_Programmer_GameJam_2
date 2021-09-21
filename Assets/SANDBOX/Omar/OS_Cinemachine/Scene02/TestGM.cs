using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace SandBox.Staging.OS_Cinemachine
{
    public class TestGM : MonoBehaviour
    {
        [SerializeField] GameObject cinemachineGO;
        private CinemachineVirtualCamera cm;
        [SerializeField] GameObject playerPrefab;
        private GameObject player;

        // Start is called before the first frame update
        void Start()
        {
            cm = cinemachineGO.GetComponent<CinemachineVirtualCamera>();

            if (player == null) CreatePlayer();
        }

        // Update is called once per frame
        void Update()
        {


        }

        private void CreatePlayer()
        {
            //Vector3 playerStartPos = new Vector3(0, 1, 0);
            player = Instantiate(playerPrefab);

            if (player != null)
            {
                var playerT = player.transform;
                cm.LookAt = playerT;
                cm.Follow = playerT;
            }
        }
    }
}
