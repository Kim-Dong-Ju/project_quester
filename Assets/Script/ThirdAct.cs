using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdAct : MonoBehaviour
{
    public float rotationSpeed = 360.0f; // 회전 속도 (도/초)

    private bool isRotating = false;

    void Update()
    {
        if (isRotating)
        {
            // 회전 각도 계산
            float rotationAmount = rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.forward, rotationAmount);
        }
    }

    void OnMouseDown()
    {
        if (!isRotating)
        {
            isRotating = true;

            // 3초 후에 회전 정지
            Invoke("StopRotation", 3.0f);
        }
    }

    void StopRotation()
    {
        isRotating = false;
    }
}
