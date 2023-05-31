using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedStartPin : MonoBehaviour
{
    private bool bIsConnected = false;
    private bool bIsPowered = false;
    private bool bIsPlus = true;

    void Start()
    {
      //  redEndPin = transform.Find("TIP_ERed").gameObject;
    }

    public void SetIsConneted(bool bValue)
    {
        if(bIsConnected != bValue)
            bIsConnected = bValue;
    }

    public void SetIsPowered(bool bValue)
    {
        if(bIsPowered != bValue && bIsConnected)
            bIsPowered = bValue;
        

      //  redEndPin.GetComponent<RedEndPin>().SetIsPowered(bIsPowered);
    }

    public void SetIsPlus(bool bValue)
    {
        bIsPlus = bValue;
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

    public bool GetIsPlus()
    {
        return bIsPlus;
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     Debug.Log(other.name + "감지 시작");
    // }

    // private void OnTriggerStay(Collider other)
    // {
    //     Debug.Log(other.name + "감지 중");
    // }
}
