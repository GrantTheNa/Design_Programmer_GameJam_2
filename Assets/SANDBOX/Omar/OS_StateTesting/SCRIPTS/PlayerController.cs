using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SandBox.Staging.OS_StateTesting
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float playerSpeed = 2.0f;
        [SerializeField] LayerMask whatCanBeClickedOn;

        private NavMeshAgent myAgent;
        private Vector3 destination;

        // Start is called before the first frame update
        void Awake()
        {
            GameManager_v02.instance.playerController = this;
            
            DontDestroyOnLoad(gameObject);
            
            myAgent = GetComponent<NavMeshAgent>();
            myAgent.speed = playerSpeed;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                if (Physics.Raycast(myRay, out hitInfo, 100, whatCanBeClickedOn))
                {
                    myAgent.SetDestination(hitInfo.point);
                    destination = hitInfo.point;
                }
            }
        }

        public void ResetPlayer(Transform playerStartPos)
        {
            myAgent.ResetPath();
            myAgent.Warp(playerStartPos.position);

            if (!myAgent.isOnNavMesh)
            {
                myAgent.transform.position = playerStartPos.position;
                myAgent.enabled = false;
                myAgent.enabled = true;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, destination);
        }


    }

}