using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFlow : MonoBehaviour
{
    public float rotationSpeed = 50.0f; // 회전 속도 (도/초)
    public bool isRotating = false;
    public int count = 1;
    void Update()
    {
        // if (isRotating)
        // {
        //     // 회전 각도 계산
        //     float rotationAmount = rotationSpeed * Time.deltaTime;
        //     // transform.Rotate(Vector3.forward, rotationAmount);
        //     transform.Rotate(0,0 ,100.0f * Time.deltaTime);
        // }
        if(Input.touchCount > 0)
        {
            if(count == 1){
                // transform.position = Vector3.Lerp(transform.position, targetPosition, movementSpeed * Time.deltaTime);

                count = 2;
            }   
            else if(count == 2){
                // transform.position = Vector3.Lerp(transform.position, targetPosition, movementSpeed * Time.deltaTime);

                count = 3;
            }  
            else if(count == 3){
                transform.eulerAngles = new Vector3(0.0f, 0.0f, 180f);

                count = 4;
            }  
            else if(count == 4){
                transform.eulerAngles = new Vector3(0.0f, 0.0f, 90f);

                count = 5;
            }  
        }
    }

    void OnMouseDown()
    {
        isRotating = true;
        Invoke("StopRotation", 3.0f);
    }

    void StopRotation()
    {
        isRotating = false;
    }
}