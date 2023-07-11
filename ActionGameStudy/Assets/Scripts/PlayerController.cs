using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 0;
    [SerializeField] float runSpeed = 10;
    [SerializeField] float walkSpeed = 5;

    [SerializeField] float jumpForce = 6;
    [SerializeField] float hp = 1;
    [SerializeField] float atk = 1;

    CameraController playerCamera;
    Rigidbody rigidbody;
    Animator animator;
    
    float gravityScale = -9.8f;
    [SerializeField] float yVelocity = 0;
    [SerializeField] bool isGrounded;

    float hAxis;
    float vAxis;

    private void Awake()
    {
        playerCamera = GetComponent<CameraController>();
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
        rigidbody.useGravity = false;
        playerCamera = GameObject.Find("PlayerCamera").GetComponent<CameraController>();
    }

    void Start()
    {
        
    }

    void InputKey()
    {
        hAxis = Input.GetAxis("Horizontal") * speed;
        vAxis = Input.GetAxis("Vertical") * speed;
    }

    void Move()
    {

    }

    private void FixedUpdate()
    {
        if (isGrounded)
        {
            Vector3 movement = playerCamera.transform.right * hAxis + playerCamera.transform.forward * vAxis;
            rigidbody.velocity = new Vector3(movement.x, 0, movement.z);
            if (Input.GetButton("Run"))
            {
                speed = runSpeed;
                animator.SetFloat("Velocity", 1f);
            }
            else
            {
                speed = walkSpeed;
                animator.SetFloat("Velocity", 0.5f);
            }

            transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * playerCamera.GetComponent<CameraController>().sensivity * Time.deltaTime);
            if (movement.magnitude != 0f)
            {
                if (!animator.GetBool("Moving"))
                {
                    animator.SetBool("Moving", true);
                }

                Quaternion CamRotation = playerCamera.transform.rotation;
                CamRotation.x = 0f;
                CamRotation.z = 0f;
                
                transform.rotation = Quaternion.Lerp(transform.rotation, CamRotation, 0.1f);
            }
            else
            {
                if (animator.GetBool("Moving"))
                {
                    animator.SetBool("Moving", false);
                    animator.SetFloat("Velocity", 0);
                }
            }

            if (Input.GetButtonDown("Jump"))
            {
                yVelocity = jumpForce;
                
                animator.SetInteger("Jumping", 1);
      
            }

        }
        else
        {
            yVelocity += gravityScale * Time.deltaTime;
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, yVelocity, rigidbody.velocity.z);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }

    void Update()
    {
        InputKey();
    }

    void Attack()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            yVelocity = 0;
            animator.SetInteger("Jumping", 0);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
