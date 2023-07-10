using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5;
    [SerializeField] float jumpForce = 3;
    [SerializeField] float hp = 1;
    [SerializeField] float atk = 1;

    CameraController playerCamera;
    Rigidbody rigidbody;
    Animator animator;
    
    float gravityScale = -9.8f;
    [SerializeField] float yVelocity = 0;
    [SerializeField] bool isGrounded;

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

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal") * speed;
        float vertical = Input.GetAxis("Vertical") * speed;

        Vector3 movement = playerCamera.transform.right * horizontal + playerCamera.transform.forward * vertical;

        if (isGrounded)
        {
            rigidbody.velocity = new Vector3(movement.x, 0, movement.z);
        }
        else
        {
            yVelocity += gravityScale * Time.deltaTime;
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, yVelocity, rigidbody.velocity.z);
        }
        
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * playerCamera.GetComponent<CameraController>().sensivity * Time.deltaTime);
        if (movement.magnitude != 0f)
        {
            Quaternion CamRotation = playerCamera.transform.rotation;
            CamRotation.x = 0f;
            CamRotation.z = 0f;

            transform.rotation = Quaternion.Lerp(transform.rotation, CamRotation, 0.1f);
            
        }

        if (Input.GetButtonDown("Jump"))
        {
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            animator.SetInteger("Jumping", 1);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Attack();    
        }
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
