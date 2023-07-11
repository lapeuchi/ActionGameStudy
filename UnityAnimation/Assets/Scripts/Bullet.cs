using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10.0f;
    private Rigidbody rigid;

    public void Shoot(Vector3 dir)
    {
        rigid = GetComponent<Rigidbody>();
        rigid.AddForce(dir * bulletSpeed, ForceMode.Impulse);
        DestroyBullet();
    }

    void DestroyBullet()
    {
        Destroy(gameObject, 3f);
    }
}
