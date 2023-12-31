using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float hAxis;
    float vAxis;
    float moveSpeed = 2.5f;
    float runSpeed = 5f;
    float walkSpeed = 2.5f;
    float rotateSpeed = 7f;
    bool isRun = false;
    bool isShoot = false;

    private float shotTimer = 0;
    private float shotTime = 0.2f;

    private Rigidbody _rigidbody;
    private Animator _animator;
    private Transform firePoint;

    GameObject bulletPrefab;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        firePoint = GameObject.Find("FirePoint").transform;
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
    }
    
    void Update()
    {
        MoveInput();
        AnimSetBool();
        ShootAnim();
        Shoot();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void MoveInput()
    {
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");
        isRun = Input.GetKey(KeyCode.LeftShift);
        isShoot = Input.GetButton("Fire1");
    }

    void AnimSetBool()
    {
        if (isRun)
        {
            _animator.SetFloat("Horizontal", hAxis);
            _animator.SetFloat("Vertical", vAxis);
        }
        else
        {
            _animator.SetFloat("Horizontal", hAxis * 0.5f);
            _animator.SetFloat("Vertical", vAxis * 0.5f);
        }
    }
    void ShootAnim()
    {
        if (isShoot)
            _animator.SetBool("isShoot", true);
        else
            _animator.SetBool("isShoot", false);
    }

    void Shoot()
    {
        shotTimer += Time.deltaTime;
        if (isShoot && shotTimer >= shotTime)
        {
            shotTimer = 0;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            
            bullet.GetComponent<Bullet>().Shoot(transform.forward);
        }
    }

    void Move() 
    {
        Vector3 moveInputVec = new Vector3(hAxis, 0, vAxis).normalized;
        Vector3 moveMent = moveInputVec * moveSpeed;

        _rigidbody.velocity = new Vector3(moveMent.x, _rigidbody.velocity.y, moveMent.z);

        if (moveInputVec != Vector3.zero)
        {   
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveInputVec), Time.deltaTime * rotateSpeed);

            if (isRun)
                moveSpeed = runSpeed;
            else
                moveSpeed = walkSpeed;
        }
        
    }

}
