using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackStartPin : MonoBehaviour
{
    private bool bIsConnected = false;
    private bool bIsPowered = false;
    private bool bIsMinus = true;

    void Start()
    {
      //  blackEndPin = transform.Find("TIP_EBlack").gameObject;
    }

    // Set Functions
    public void SetIsConneted(bool bValue)
    {
        if(bValue != bIsConnected)
            bIsConnected = bValue;
        
    }

    public void SetIsPowered(bool bValue)
    {
        if(bValue != bIsPowered && bIsConnected)
            bIsPowered = bValue;

    }

    public void SetIsMinus(bool bValue)
    {
        bIsMinus = bValue;
   //     blackEndPin.GetComponent<BlackEndPin>().SetIsMinus(bIsMinus);
    }
    // Get Functions
    public bool GetIsConneted()
    {
        return bIsConnected;
    }

    public bool GetIsPowered()
    {
        return bIsPowered;
    }

    public bool GetIsMinus()
    {
        return bIsMinus;
    }
}
