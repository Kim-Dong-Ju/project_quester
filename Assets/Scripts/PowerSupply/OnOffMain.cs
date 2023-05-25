using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffMain : MonoBehaviour
{
    private bool bIsPowered = false;
    GameObject ParentObj;
    GameObject WireRed;
    GameObject WireBlack;
    Vector3 SwitchRotation = new Vector3(-90, 0, 180);
    // Start is called before the first frame update
    void Start()
    {
        ParentObj = transform.parent.gameObject;
        WireRed = ParentObj.transform.Find("Wire_Plus_Start").gameObject;
        WireBlack = ParentObj.transform.Find("Wire_Minus_Start").gameObject;
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    public void OnInteract()
    {
        SetIsPowered(!bIsPowered);
        transform.localRotation = Quaternion.Euler(SwitchRotation);
    }

    public void SetIsPowered(bool bValue)
    {
        bIsPowered = bValue;
        
        ParentObj.GetComponent<PowerSupply>().SetIsMainOn(bIsPowered);
        if(bIsPowered) SwitchRotation.x = -105;
        else SwitchRotation.x = -90;
        // WireRed.GetComponent<RedPin>().SetIsPowered(bIsPowered);
        // WireRed.GetComponent<RedPin>().SetIsPowered(bIsPowered);
    }

    public bool GetIsPowered()
    {
        return bIsPowered;
    }
}
