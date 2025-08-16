using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Adding proper Mouvement to the player
public class PlayerMouvement : MonoBehaviour
{
    [SerializeField] Animator animator;
    // Movement variables
    public float moveSpeed;
    public Transform orientation;
    public float groundDrag;
    public float jumpForce;
    public float jumpCd;
    public bool jumpReady;
    public float airMulti;
    public KeyCode jumpKey = KeyCode.Space;
   

    // Checking geound variables
    public float playerHeight;
    public LayerMask isGround;
    public bool grounded;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;
    // Determine the moving object and prevent its rotation
    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    public void Update()
    {
        // ground checking update
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, isGround);
        MyInput();

        // Applying drag
        //if (grounded)
            //rb.lineaerlocity = groundDrag;
        
        
       // else
         //rb.linearDamping = 0;
       

    }

    public void FixedUpdate()
    {
        MovePlayer();
    }
    // Get movement input
    public void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //Jump when space is pressed
        if (Input.GetKey(jumpKey) && grounded)
        {
            
            Jump();
            jumpReady = false;

            Invoke(nameof(ResetJump), jumpCd);

        }

    }
    // calculate movement direction 
    public void MovePlayer()
    {
        
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        
        // On ground
        if(grounded) 
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f);
        
        
        // in air
      else if (!grounded)
              rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMulti, ForceMode.Force);
        
    }
    public void Jump()
    {
        // Speed and jump height
        //rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f,rb.linearVelocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    public void ResetJump()
    {
        jumpReady = true;
    }
}
