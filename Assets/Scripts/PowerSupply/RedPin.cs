using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPin : MonoBehaviour
{
    private bool bIsConnected = false;
    private bool bIsPowered = false;
    private bool bIsPlus = true;

    public void OnInteract()
    {
        if(!bIsConnected)
        {
            // Plug a Red Pin into a Power Supply 
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            transform.localPosition = new Vector3(0.059f, 0.11275f, -0.04788f);
           
            SetIsConneted(true);
        }
    }

    // Set Functions
    public void SetIsConneted(bool bValue)
    {
        if(bIsConnected != bValue)
            bIsConnected = bValue;
    }

    public void SetIsPowered(bool bValue)
    {
        if(bIsPowered != bValue && bIsConnected)
            bIsPowered = bValue;
        
        if(bIsConnected)
        {
            if(bIsPowered) Debug.Log("Red Pin on");
            else Debug.Log("Red Pin off");
        }
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
