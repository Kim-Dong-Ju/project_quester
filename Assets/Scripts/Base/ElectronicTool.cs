using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronicTool : MonoBehaviour
{
    public GameObject PowerSupply;
    public GameObject PlusWire;
    public GameObject MinusWire;
    private bool bIsPowered = false;
    private bool bIsSwapped = false;
    private float volt = 0.0f;

    // virtual void SetIsPowered()
    // {

    // }
    
    // virtual void SetIsSwapped()
    // {

    // }

    public virtual void SetIsPowered(bool bValue)
    {
        bool bPConnect = PlusWire.GetComponent<RedEndPin>().GetIsConneted();
        bool bMConnect = MinusWire.GetComponent<BlackEndPin>().GetIsConneted();

        if(bPConnect && bMConnect)
            bIsPowered = bValue;
    }
    
    public virtual void SetIsSwapped(bool bValue)
    {
        bool bPConnect = PlusWire.GetComponent<RedEndPin>().GetIsConneted();
        bool bMConnect = MinusWire.GetComponent<BlackEndPin>().GetIsConneted();

        if(bPConnect && bMConnect)
            bIsSwapped = bValue;
    }
}
