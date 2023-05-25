using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEndPin : MonoBehaviour
{
    public GameObject RedStart;
    bool bIsConnected = false;
    //bool bRotate = false;
    bool bIsPowered = false;
    bool bIsPlus = true;

    public void SetIsConneted(bool bValue)
    {
        bIsConnected = bValue;
        // if(bIsConnected)
        // {
        //     transform.SetParent(RedStart.transform);
        //     RedStart.GetComponent<RedStartPin>().SetChild();
        // }
    }

    public void SetIsPowered(bool bValue)
    {
        bIsPowered = bValue;
    }

    public void SetIsPlus(bool bValue)
    {
        bIsPlus = bValue;
    }

    public bool GetIsConneted()
    {
        return bIsConnected;
    }

    public bool GetIsPowered()
    {
        return bIsPowered;
    }

    public bool GetIsPlus()
    {
        return bIsPlus;
    }
}
