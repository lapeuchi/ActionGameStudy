using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private const float YMin = -7.0f;
    private const float YMax = 20.0f;

    private Transform cameraTarget;

    private float distance = 2f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    public float sensivity = 170.0f;

    void Start()
    {
        cameraTarget = GameObject.Find("CameraTarget").transform;
    }

    void LateUpdate()
    {
        currentX += Input.GetAxis("Mouse X") * sensivity * Time.deltaTime;
        currentY += Input.GetAxis("Mouse Y") * sensivity * 0.7f * Time.deltaTime;

        currentY = Mathf.Clamp(currentY, YMin, YMax);

        Vector3 Direction = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        transform.position = cameraTarget.position + rotation * Direction;

        transform.LookAt(cameraTarget.position);
    }
}
