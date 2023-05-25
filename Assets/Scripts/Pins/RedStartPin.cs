using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedStartPin : MonoBehaviour
{
    private bool bIsConnected = false;
    private bool bIsPowered = false;
    private bool bIsPlus = true;
    public GameObject RedEnd;

    void Start()
    {
      //  redEndPin = transform.Find("TIP_ERed").gameObject;
    }

    // public void OnInteract()
    // {
    //     if(!bIsConnected)
    //     {
    //         // Plug a Red Pin into a Power Supply 
    //         transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
    //         transform.localPosition = new Vector3(0.059f, 0.11275f, -0.04788f);
           
    //         SetIsConneted(true);
    //     }
    // }

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
        
        if(RedEnd)
        { 
            RedEnd.GetComponent<RedEndPin>().SetIsPowered(bIsPowered);
        }

      //  redEndPin.GetComponent<RedEndPin>().SetIsPowered(bIsPowered);
    }

    public void SetIsPlus(bool bValue)
    {
        bIsPlus = bValue;

        if(RedEnd)
        { 
            RedEnd.GetComponent<RedEndPin>().SetIsPlus(bIsPlus);
        }
    }

    public void SetChild()
    {
        RedEnd = transform.Find("TIP_ERed").gameObject;
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
