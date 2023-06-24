using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseToggle : MonoBehaviour
{
    private bool bIsSwapped = false;
    PowerSupply powerSupply;
    ToggleAnim anim;
    // // Start is called before the first frame update
    void Start()
    {
        powerSupply = transform.parent.gameObject.GetComponent<PowerSupply>();
        anim = GetComponent<ToggleAnim>();
        anim.SetToggleAnim(false);
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
    public void OnInteract()
    {
        bIsSwapped = !bIsSwapped;

        powerSupply.SetSwap(bIsSwapped);
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
}
