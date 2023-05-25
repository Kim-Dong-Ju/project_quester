using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseToggle : MonoBehaviour
{
    private bool bIsSwapped = false;
    GameObject ParentObj;
    ToggleAnim anim;
    // // Start is called before the first frame update
    void Start()
    {
        ParentObj = transform.parent.gameObject;
        anim = GetComponent<ToggleAnim>();
        anim.SetToggleAnim(false);
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
    public void OnInteract()
    {
        SetIsSwap(!bIsSwapped);

        ParentObj.GetComponent<PowerSupply>().SetSwap(bIsSwapped);
        anim.SetToggleAnim(bIsSwapped);

        // if(bIsSwapped)
        // {
        //     transform.localRotation = Quaternion.Euler(new Vector3(75, 0, 0));
        // }
        // else
        // {
        //     transform.localRotation = Quaternion.Euler(new Vector3(105, 0, 0));
        // }
    }

    public void SetIsSwap(bool bValue)
    {
        bIsSwapped = bValue;
        
    }

    public bool GetIsSwap()
    {
        return bIsSwapped;
    }
}
