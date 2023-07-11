using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform target;
    Vector3 offset = new Vector3(0, 8, -7f);
    void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    void LateUpdate()
    {
        Vector3 fixedPos = target.position + offset;
        transform.position = fixedPos;
        transform.LookAt(target);
    }
}
