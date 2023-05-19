using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmpereWheel : MonoBehaviour
{
    public Vector3 vLastPos = new Vector3(1f,1f,1f);
    public Vector3 vCurPos = new Vector3(1f,1f,1f);
    private Vector3 vRotation;
    private bool bIsChange = false;
    float fRotateSpeed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        //vLastRotation = transform.localRotation;
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
    public void OnInteract()
    {
        bIsChange = true;
        Vector3 vOffset = vCurPos - vLastPos;

        vRotation.z = (vOffset.x + vOffset.y) * Time.deltaTime * fRotateSpeed;
        if(vRotation.z >= 360.0f) vRotation.z = 359.0f;
        if(vRotation.z < -90.0f) vRotation.z = -90.0f;

        transform.Rotate(vRotation);

    }

    public void OffInteract()
    {
        SetLastPos();
        bIsChange = false;
    }

    public void SetCurPos(Vector3 vTouchPos)
    {
        vCurPos = vTouchPos;
    }

    public void SetLastPos()
    {
        vLastPos = vCurPos;
    }

    public Vector3 GetCurPos()
    {
        return vCurPos;
    }

    public Vector3 GetLastPos()
    {
        return vLastPos;
    }

    public bool GetIsChange()
    {
        return bIsChange;
    }
}