using UnityEngine;

/*
 *  This player move class will allow the gameobject to move based on CharacterController
 */ 

public class FPSMovement : MonoBehaviour
{
    // VARS
    public UnityEngine.CharacterController m_charController; // Ref to character controller
    public float m_movementSpeed = 12; // walk speed
    public float m_runSpeed = 1.5f; // run speed

    public float m_gravity = -9.81f; // stores the gravity amount to push our player down when jumping
    public float m_jumpHeight = 3f; // jumps 3 meters high. 1 unit = 1 meter in Unity
    private Vector3 m_velocity; // speed change due to gravity

    public Transform m_groundCheckPoint; // sphere collider origin
    public float m_groundDistance = 0.4f; // sphere collider radius
    public LayerMask m_groundMask; // sphere collider collider layer - IF NOT SET IN INSPECTOR IT WONT JUMP
    private bool m_isGrounded; // flag for checking if on the ground or not

    public KeyCode m_forward; // key bindings
    public KeyCode m_back; // key bindings
    public KeyCode m_left; // key bindings
    public KeyCode m_right; // key bindings
    public KeyCode m_sprint; // key bindings
    public KeyCode m_jump; // key bindings

    private float m_finalSpeed = 0f; // final speed after walking and running calculations are completed

    // Start is called before the first frame update
    void Awake()
    {
        m_finalSpeed = m_movementSpeed; // Set final speed to movement speed at the start
    }

    // Update is called once per frame
    void Update()
    {
        m_isGrounded = HitGroundCheck(); // Checks touching the ground every frame
        MoveInputCheck(); // Check movement input
    }

    // Check if a button is pressed
    void MoveInputCheck()
    {
        float x = Input.GetAxis("Horizontal"); // get the x value for the GameObject vector
        float z = Input.GetAxis("Vertical"); // get the z value for the GameObject vector

        Vector3 move = Vector3.zero;

        if (Input.GetKey(m_forward) || Input.GetKey(m_back) || Input.GetKey(m_left) || Input.GetKey(m_right)) // check input for the keys defined in VARS
        {
            move = transform.right * x + transform.forward * z; // calculate the move vector (direction)
        }

        MovePlayer(move); // Move the player player based on the move vector
        RunCheck(); // Checks the input for run
        JumpCheck(); // Checks if we can jump
    }

    // MovePlayer
    void MovePlayer(Vector3 move)
    {
        m_charController.Move(move * m_finalSpeed * Time.deltaTime); // Moves the GameObject using the Character Controller
        m_velocity.y += m_gravity * Time.deltaTime; // Gravity effects the jump velocity
        m_charController.Move(m_velocity * Time.deltaTime); // Actually move the player up
    }

    // Player run
    void RunCheck()
    {
        if (Input.GetKeyDown(m_sprint)) // if key is down - m_sprint key
        {
            m_finalSpeed = m_movementSpeed * m_runSpeed; // multiply movementSpeed by runSpeed and store it in final speed - this is the speed we will move the player
        }
        else if (Input.GetKeyUp(m_sprint)) // if key is up - m_sprint key
        {
            m_finalSpeed = m_movementSpeed; // when run button is released set finalSpeed to the walking speed (movementSpeed)
        }
    }

    // Ground check
    bool HitGroundCheck()
    {
        bool isGrounded = Physics.CheckSphere(m_groundCheckPoint.position, m_groundDistance, m_groundMask);

        //Gravity ground check
        if (isGrounded && m_velocity.y < 0)
        {
            m_velocity.y = -2f;
        }

        return isGrounded;
    }

    // Jump Check
    void JumpCheck()
    {
        if (Input.GetKeyDown(m_jump))
        {
            if (m_isGrounded == true)
            {
                m_velocity.y = Mathf.Sqrt(m_jumpHeight * -2f * m_gravity);
            }
        }
    }
}
