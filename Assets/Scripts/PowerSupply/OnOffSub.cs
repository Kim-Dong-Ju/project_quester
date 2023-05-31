using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffSub : MonoBehaviour
{
    private bool bIsPowered = false;
    PowerSupply powerSupply;
    // Start is called before the first frame update
    void Start()
    {
        powerSupply = transform.parent.gameObject.GetComponent<PowerSupply>();
    }

    // // Update is called once per frame
    // void Update()
    // {
    //     // if(Input.touchCount > 0)
    //     // {
    //     //     Debug.Log("Touch Count: " + Input.touchCount);
    //     //     if(Input.GetTouch(0).phase == TouchPhase.Began)
    //     //     {
    //     //         Debug.Log("Touch Position: " + Input.GetTouch(0).position);   
    //     //     }
    //     // }
    // }

    public void OnInteract()
    {
        SetIsPowered(true);
        transform.localPosition = new Vector3(-0.0749f, 0.106f, -0.05275f);
    }

    public void OffInteract()
    {
        SetIsPowered(false);
        transform.localPosition = new Vector3(-0.0749f, 0.1106f, -0.05275f);
    }

    public void SetIsPowered(bool bValue)
    {
        //if(!ParentObj.GetComponent<PowerSupply>().GetIsPowered())
       // {
        if(bValue != bIsPowered)
            bIsPowered = bValue;

        if(!powerSupply.GetIsMainOn())
            powerSupply.SetIsPowered(bIsPowered);
      //  }
    }

    public bool GetIsPowered()
    {
        return bIsPowered;
    }
}
