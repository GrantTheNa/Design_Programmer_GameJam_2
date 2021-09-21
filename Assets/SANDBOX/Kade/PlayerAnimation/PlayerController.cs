using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Sandbox.Kade.PlayerAnimation
{
    public class PlayerController : MonoBehaviour
    {
        //VARS
        public float playerSpeed = 2.0f;
        public LayerMask whatCanBeClickedOn;
        private NavMeshAgent myAgent;
        private Vector3 destination;

        public Animator animator;

        // Start is called before the first frame update
        void Start()
        {
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

            if (Input.GetKey(KeyCode.Alpha0))
            {
                animator.SetInteger("level", 0);
            }
            if (Input.GetKey(KeyCode.Alpha1))
            {
                animator.SetInteger("level", 1);
            }
            if (Input.GetKey(KeyCode.Alpha2))
            {
                animator.SetInteger("level", 2);
            }
            if (Input.GetKey(KeyCode.Alpha3))
            {
                animator.SetInteger("level", 3);
            }
            if (Input.GetKey(KeyCode.E))
            {
                animator.SetTrigger("attack");
            }

        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, destination);
        }


    }

}