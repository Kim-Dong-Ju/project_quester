using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmpereWheel : MonoBehaviour
{
    public Vector3 vLastPos = new Vector3(1f,1f,1f);
    public Vector3 vCurPos = new Vector3(1f,1f,1f);

    public float lastVolt = 0.0f;

    //float curVolt = 0.0f;
    private Vector3 vRotation, vCurRotate;
    private bool bIsChange = false;
    public int Rcnt = 0, Lcnt = 0;
    float fRotateSpeed = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        //vLastRotation = transform.localRotation;
       // vCurRotate = transform.eularAngles;
        lastVolt = (transform.rotation.z + 90.0f) / 600;
        transform.parent.GetComponent<PowerSupply>().SetVoltage(lastVolt);
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
    public void OnInteract()
    {
        PowerSupply psColScript = transform.parent.GetComponent<PowerSupply>();
        bIsChange = true;
        Vector3 vOffset = (Vector3)(vCurPos - vLastPos);

       // vRotation.z = (vOffset.x + vOffset.y) * Time.deltaTime * fRotateSpeed;
        vRotation.z = vOffset.x * Time.deltaTime * fRotateSpeed;
        vRotation.z = Mathf.Clamp(vRotation.z, -180, 180);
        if(vRotation.z < -90)
        {
            lastVolt = (float)(vRotation.z + 450.0f) / 600;
        } // -90 ~ -180 => 270 ~ 360
        else
        {
            lastVolt = (float)(vRotation.z + 90.0f) / 600;
        } // -90 ~ 180 => 0 ~ 270

        //vRotation.z = (vRotation.z < 0) ? vRotation.z + 360 : vRotation.z;
        psColScript.SetVoltage(lastVolt);
        psColScript.Needle.GetComponent<NeedleAnim>().SetNeedleAnim(psColScript.GetIsPowered());

        transform.Rotate(vRotation);
    }

    public void OffInteract()
    {
        PowerSupply psColScript = transform.parent.GetComponent<PowerSupply>();
        SetLastPos();
        psColScript.SetVoltage(lastVolt);
        psColScript.Needle.GetComponent<NeedleAnim>().SetNeedleAnim(psColScript.GetIsPowered());
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