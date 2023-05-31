using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackEndPin : MonoBehaviour
{
    // Start is called before the first frame update
    bool bIsConnected = false;
    bool bIsPowered = false;
    bool bIsMinus = true;
   
    public void SetIsConneted(bool bValue)
    {
        bIsConnected = bValue;
        // if(bIsConnected)
        // {
        //     transform.SetParent(BlackStart.transform);
        //     BlackStart.GetComponent<BlackStartPin>().SetChild();
        // }
    }

    public void SetIsPowered(bool bValue)
    {
        bIsPowered = bValue;
    }

    public void SetIsMinus(bool bValue)
    {
        bIsMinus = bValue;
    }

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
