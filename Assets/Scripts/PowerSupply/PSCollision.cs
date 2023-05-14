using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSCollision : MonoBehaviour
{
    GameObject Wire_Plus_Start;
    GameObject Wire_Minus_Start;
    GameObject OnOffSubButton;
    GameObject OnOffMainSwitch;
    GameObject Needle;
    bool bPlusStartConnected = false;
    bool bMinusStartConnected = false;
    bool bPowered = false;
    bool bIsMainOn = false;
    bool bIsSwapped = false;
    // Start is called before the first frame update
    void Start()
    {
        Wire_Plus_Start = transform.Find("Wire_Plus_Start").gameObject;    
        Wire_Minus_Start = transform.Find("Wire_Minus_Start").gameObject;
        OnOffSubButton = transform.Find("ON_OFF_Sub").gameObject;
        OnOffMainSwitch = transform.Find("ON_OFF_Main").gameObject;
        Needle = transform.Find("Needle").gameObject;
        Needle.GetComponent<NeedleAnim>().SetNeedleAnim(false);

    }

    // Update is called once per frame
    void Update()
    {
        // bPowered = OnOffSubButton.GetComponent<OnOffSub>().GetIsPowered();
        // if(Wire_Minus_Start.GetComponent<BlackPin>().GetIsConneted())
        //     Wire_Minus_Start.GetComponent<BlackPin>().SetIsPowered(bPowered);

        // if(Wire_Plus_Start.GetComponent<RedPin>().GetIsConneted())
        //     Wire_Plus_Start.GetComponent<RedPin>().SetIsPowered(bPowered);
    }

    public void SetIsPowered(bool bValue)
    {
        bPowered = bValue;

        Wire_Plus_Start.GetComponent<RedPin>().SetIsPowered(bPowered);
        Wire_Minus_Start.GetComponent<BlackPin>().SetIsPowered(bPowered);
        Needle.GetComponent<NeedleAnim>().SetNeedleAnim(bPowered);
    }

    public void SetIsMainOn(bool bValue)
    {
        bIsMainOn = bValue;
        SetIsPowered(bValue);
    }

    public void SetSwap(bool bValue)
    {
        bIsSwapped = bValue;

        Wire_Plus_Start.GetComponent<RedPin>().SetIsPlus(bIsSwapped);
        Wire_Minus_Start.GetComponent<BlackPin>().SetIsMinus(bIsSwapped);
    }

    public bool GetIsPowered()
    {
        return bPowered;
    }

    public bool GetIsMainOn()
    {
        return bIsMainOn;
    }

    public bool GetIsSwap()
    {
        return bIsSwapped;
    }
}
