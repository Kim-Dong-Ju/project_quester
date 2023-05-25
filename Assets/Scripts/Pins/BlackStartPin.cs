using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackStartPin : MonoBehaviour
{
    private bool bIsConnected = false;
    private bool bIsPowered = false;
    private bool bIsMinus = true;
    public GameObject BlackEnd;

    void Start()
    {
      //  blackEndPin = transform.Find("TIP_EBlack").gameObject;
    }
    // public void OnInteract()
    // {
    //     if(!bIsConnected)
    //     {
    //         // Plug a Black Pin into a Power Supply 
    //         transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
    //         transform.localPosition = new Vector3(0.059f, 0.11275f, 0.041f);
            
    //         SetIsConneted(true);
    //     }
    // }

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
        
        if(BlackEnd)
        { 
            BlackEnd.GetComponent<BlackEndPin>().SetIsPowered(bIsPowered);
        }
    }

    public void SetIsMinus(bool bValue)
    {
        bIsMinus = bValue;
        if(BlackEnd)
        { 
            BlackEnd.GetComponent<BlackEndPin>().SetIsMinus(bIsMinus);
        }
   //     blackEndPin.GetComponent<BlackEndPin>().SetIsMinus(bIsMinus);
    }

    public void SetChild()
    {
        BlackEnd = transform.Find("TIP_EBlack").gameObject;
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
