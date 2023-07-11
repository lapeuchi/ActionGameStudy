using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 0;
    [SerializeField] float originSpeed = 3;
    [SerializeField] float maxSpeed = 10;
    [SerializeField] float minSpeed = 2;

    [SerializeField] float jumpForce = 15f;
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

    float InputVector;

    bool isRun;
    bool isWalk;
    bool isJump;
    bool isAttack;

    private void Awake()
    {
        playerCamera = GetComponent<CameraController>();
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
        rigidbody.useGravity = false;
        playerCamera = GameObject.Find("PlayerCamera").GetComponent<CameraController>();
        speed = 0;
        isJump = false;
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Update()
    {
        InputKey();
        Attack();
        SetAnim();
    }

    void InputKey()
    {
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");
        isJump = Input.GetButtonDown("Jump");

        isAttack = Input.GetButtonDown("Fire1");
        if (Input.GetButton("Run"))
        {
            isRun = true;
            isWalk = false;
        }
        else if (Input.GetButton("Walk"))
        {
            isWalk = true;
            isRun = false;
        }
        else
        {
            isWalk = false;
            isRun = false;
        }
    }

    bool IsNotAxisZero() { return (hAxis != 0 || vAxis != 0); }

    void SetAnim()
    {
        if (isGrounded)
        {
            if (IsNotAxisZero())
            {
                animator.SetBool("isMove", true);
                animator.SetFloat("moveSpeed", (speed / maxSpeed));
            }
            else
            {
                animator.SetFloat("moveSpeed", (speed / maxSpeed));
                animator.SetBool("isMove", false);
            }
            if(isJump)
            {
                animator.SetTrigger("isJump");
            }
            //animator.SetFloat("yVelocity", 0);
            animator.SetBool("isGrounded", false);
        }
        else
        {
            animator.SetFloat("yVelocity", yVelocity);
        }
        
    }

    void Move()
    {
        if (isGrounded)
        {
            if (IsNotAxisZero())
            {
                if (isRun)
                {
                    speed = Mathf.Lerp(speed, maxSpeed, 0.07f);
                }
                else if (isWalk)
                {
                    speed = Mathf.Lerp(speed, minSpeed, 0.1f);
                }
                else
                {
                    speed = Mathf.Lerp(speed, originSpeed, 0.1f);
                }
            }
            else
            {
                speed = Mathf.Lerp(speed, 0, 0.5f);
            }
            Vector3 movement = (playerCamera.transform.right * hAxis + playerCamera.transform.forward * vAxis).normalized * speed;
            rigidbody.velocity = new Vector3(movement.x, rigidbody.velocity.y, movement.z);

            Quaternion CamRotation = playerCamera.transform.rotation;
            CamRotation.x = 0f;
            CamRotation.z = 0f;

            transform.rotation = Quaternion.Lerp(transform.rotation, CamRotation, 0.1f);

            if (isJump)
            {
                rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }

        }
        else
        {
            yVelocity += gravityScale * Time.deltaTime;
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, yVelocity, rigidbody.velocity.z);
        }
    }

    void Attack()
    {
        if (isAttack)
        {
            Debug.Log("Attack!");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            yVelocity = 0;
        }
    }
    
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJump = false;
            isGrounded = false;
        }
    }

    public void Foot(float volume)
    {
        Debug.Log("step");
    }
}