using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PlayerController : MonoBehaviour
{
    [SerializeField] public float playerSpeed = 2.0f;
    [SerializeField] LayerMask whatCanBeClickedOn;

    private NavMeshAgent myAgent;
    private Vector3 destination;

    public Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        MASTER_GameManager.Instance.player = gameObject;

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

            Debug.Log(myRay);
            

            Debug.Log("mouse was clicked");

            if (Physics.Raycast(myRay, out hitInfo, 100, whatCanBeClickedOn))
            {
                Debug.Log("Click hit the ground");
                myAgent.SetDestination(hitInfo.point);
                destination = hitInfo.point;
            }
        }

       
        //transform.LookAt(Camera.main.transform.position, Vector3.up);

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

    public bool isPlayerInView()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);

        if (screenPoint.x > 0 && screenPoint.x < 1
            && screenPoint.y > 0 && screenPoint.y < 1)
        {
            return true;
        }

        return false;
    }
}
