using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffMain : MonoBehaviour
{
    private bool bIsPowered = false;
    PowerSupply powerSupply;
    Vector3 SwitchRotation = new Vector3(-90, 0, 180);
    // Start is called before the first frame update
    void Start()
    {
        powerSupply = transform.parent.gameObject.GetComponent<PowerSupply>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
    }

    public void OnInteract()
    {
        SetIsPowered(!bIsPowered);
        transform.localRotation = Quaternion.Euler(SwitchRotation);
    }

    public void SetIsPowered(bool bValue)
    {
        bIsPowered = bValue;
        
        powerSupply.SetIsMainOn(bIsPowered);
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
