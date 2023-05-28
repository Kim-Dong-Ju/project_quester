using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrookesWheel : MonoBehaviour
{
    float baseRotate = 300f;
    float baseSpeed = 0.02f;
    Rigidbody WheelRb;
    float voltage = 0;
    bool powered = false, swapped = false;
    // Start is called before the first frame update
    void Start()
    {
        WheelRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // WheelRb.MovePosition(WheelRb.position + new Vector3(0, 0, 1) * 0.072f * Time.deltaTime);
        // WheelRb.MoveRotation(WheelRb.rotation * Quaternion.Euler(Vector3.left * 30f * 36f * Time.deltaTime));
        // test용 코드
        if(powered) // 전원 On
        {
            if(swapped) // 극 전환 상태
            {
                WheelRb.MovePosition(WheelRb.position + new Vector3(0, 0, 1) * -baseSpeed * voltage * Time.deltaTime);
                WheelRb.MoveRotation(WheelRb.rotation * Quaternion.Euler(Vector3.right * baseRotate * voltage * Time.deltaTime));
            }
            else
            {
                WheelRb.MovePosition(WheelRb.position + new Vector3(0, 0, 1) * baseSpeed * voltage * Time.deltaTime);
                WheelRb.MoveRotation(WheelRb.rotation * Quaternion.Euler(Vector3.left * baseRotate * voltage * Time.deltaTime));
            }
        }
        // else
        // {
            
        // }
        
    }

    public void SetPinWheel(bool bPowered, bool bSwapped)
    {
        powered = bPowered;
        swapped = bSwapped;
    }

    public void SetVoltage(float value)
    {
        voltage = value;
    }
}
