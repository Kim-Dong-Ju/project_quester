using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmpereWheel : MonoBehaviour
{
    // public float lastVolt = 0.0f;

    //float curVolt = 0.0f;
    PowerSupply powerSupply;
    private Quaternion rotate;
    private float wheelAngle;
    private bool bIsLower = false, bIsOver = false;
    float Ampere = 0.0f;
 //   float testAngle = 0.0f;
    // Start is called before the first frame update
    
    void Start()
    {
        rotate = transform.rotation;
        float curRotate = transform.localEulerAngles.y;
        //lastVolt = curRotate / 100;
        //lastVolt = (transform.rotation.z + 90.0f) / 600;
        powerSupply = transform.parent.GetComponent<PowerSupply>();
    }

    public void SetAngle(float fValue)
    {
       // if(lastVolt > 3.6f || lastVolt < 0.0f) return;
      // if(volt > 3.6f || volt < 0.0f) return;
        wheelAngle = fValue;
        Quaternion limitedRotation = Quaternion.AngleAxis(wheelAngle, Vector3.forward);
        transform.rotation = rotate * limitedRotation;

        SetAmpere();
    }

    public void SetLastRotate()
    {
        rotate = transform.rotation;
    }

    public void SetAmpere()
    {
        float curRotate = transform.localEulerAngles.y;
        Debug.Log(curRotate);
        float checkVolt = curRotate / 100;
        if(Ampere == 3.59f && checkVolt == 0.0f) bIsOver = true;
        if(Ampere == 0.0f && checkVolt == 3.59f) bIsLower = true;
        powerSupply.SetIsUp((checkVolt >= Ampere)? true : false);
        //if(lastVolt == 3.59f && checkVolt == 0.0f) bIsOver = true;
       // if(lastVolt == 0.0f && checkVolt == 3.59f) bIsLower = true;
        if(bIsOver || bIsLower) return;
       // lastVolt = checkVolt;
        //transform.parent.gameObject.GetComponent<PowerSupply>().SetVoltage(lastVolt);

        Ampere = checkVolt;
        powerSupply.SetAmpere(Ampere);
        // base.SetVoltage();
    }
    // public void SetCurPos(Vector3 vTouchPos)
    // {
    //     vCurPos = vTouchPos;
    // }

    // public void SetLastPos()
    // {
    //     vLastPos = vCurPos;
    // }

    // public Vector3 GetCurPos()
    // {
    //     return vCurPos;
    // }

    // public Vector3 GetLastPos()
    // {
    //     return vLastPos;
    // }
    public bool GetIsOver()
    {
        return bIsOver;
    }

    public bool GetIsLower()
    {
        return bIsLower;
    }
}