// 크룩스관 회전차입의 날개 동작 스크립트
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrookesWheel : MonoBehaviour
{
    float baseRotate = 300f;
    float baseSpeed = 0.04f;
    Rigidbody WheelRb;
    float Ampere = 0;
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
                if(transform.localPosition.z < 0.044f) // 크룩스관 탈출 방지
                {
                    WheelRb.MovePosition(WheelRb.position + new Vector3(0, 0, 1) * baseSpeed * Ampere * Time.deltaTime);
                }
                WheelRb.MoveRotation(WheelRb.rotation * Quaternion.Euler(Vector3.left * baseRotate * Ampere * Time.deltaTime));
                // 검정 핀 방향으로 구르면서 이동
            }
            else
            {
                if(transform.localPosition.z > -0.045f) // 크룩스관 탈출 방지
                {
                    WheelRb.MovePosition(WheelRb.position + new Vector3(0, 0, 1) * -baseSpeed * Ampere * Time.deltaTime);
                }
                WheelRb.MoveRotation(WheelRb.rotation * Quaternion.Euler(Vector3.right * baseRotate * Ampere * Time.deltaTime));
                // 빨간 핀 방향으로 구르면서 이동
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

    public void SetAmpere(float value)
    {
        Ampere = value;
    }
}
