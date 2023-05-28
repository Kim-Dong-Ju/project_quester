using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSupply : MonoBehaviour
{
    GameObject Wire_Plus_Start;
    GameObject Wire_Minus_Start;
    GameObject OnOffSubButton;
    GameObject OnOffMainSwitch;
    public GameObject Needle;
    public GameObject CrookesWheel;
    public GameObject CrookesMagnetic;
    public GameObject CrookesCross;
   // bool bPlusStartConnected = false;
   // bool bMinusStartConnected = false;
    protected bool bPowered = false;
    private bool bIsMainOn = false;
    protected bool bIsSwapped = false;
    protected bool bIsUp = false;
    protected float volt = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        Wire_Plus_Start = transform.Find("Wire_Plus_Start").gameObject;    
        Wire_Minus_Start = transform.Find("Wire_Minus_Start").gameObject;
        OnOffSubButton = transform.Find("ON_OFF_Sub").gameObject;
        OnOffMainSwitch = transform.Find("ON_OFF_Main").gameObject;
        Needle.GetComponent<NeedleAnim>().SetNeedleAnim(false);
    }

    // Update is called once per frame
    void Update()
    {
        // bPowered = OnOffSubButton.GetComponent<OnOffSub>().GetIsPowered();
        // if(Wire_Minus_Start.GetComponent<BlackStartPin>().GetIsConneted())
        //     Wire_Minus_Start.GetComponent<BlackStartPin>().SetIsPowered(bPowered);

        // if(Wire_Plus_Start.GetComponent<RedStartPin>().GetIsConneted())
        //     Wire_Plus_Start.GetComponent<RedStartPin>().SetIsPowered(bPowered);
    }

    public void SetIsPowered(bool bValue)
    {
        bPowered = bValue;

        Wire_Plus_Start.GetComponent<RedStartPin>().SetIsPowered(bPowered);
        Wire_Minus_Start.GetComponent<BlackStartPin>().SetIsPowered(bPowered);
        SetNeedleAnim(bPowered);
        CrookesWheel.GetComponent<CrookesPaddle>().SetIsPowered(bPowered);
        CrookesCross.GetComponent<CrookesCross>().SetIsPowered(bPowered);
        CrookesMagnetic.GetComponent<CrookesMagnetic>().SetIsPowered(bPowered);
    }

    public void SetIsMainOn(bool bValue)
    {
        bIsMainOn = bValue;
        SetIsPowered(bValue);
    }

    public void SetSwap(bool bValue)
    {
        bIsSwapped = bValue;

        Wire_Plus_Start.GetComponent<RedStartPin>().SetIsPlus(bIsSwapped);
        Wire_Minus_Start.GetComponent<BlackStartPin>().SetIsMinus(bIsSwapped);
        CrookesWheel.GetComponent<CrookesPaddle>().SetIsSwapped(bIsSwapped);
        CrookesCross.GetComponent<CrookesCross>().SetIsSwapped(bIsSwapped);
        CrookesMagnetic.GetComponent<CrookesMagnetic>().SetIsSwapped(bIsSwapped);
    }

    public void SetVoltage(float value)
    {
        Debug.Log("PowerSupply volt: " + volt);
        volt = value;
        SetNeedleAnim(bIsUp);
        CrookesWheel.GetComponent<CrookesPaddle>().SetVoltage(volt);
        CrookesCross.GetComponent<CrookesCross>().SetVoltage(volt);
        CrookesMagnetic.GetComponent<CrookesMagnetic>().SetVoltage(volt);
    }

    public void SetNeedleAnim(bool value)
    {
        Needle.GetComponent<NeedleAnim>().SetNeedleAnim(value);
    }

    public void SetIsUp(bool value)
    {
        bIsUp = value;
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

    public float GetVoltage()
    {
        return volt;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.name == "Wire_Plus_Start")
        {
            collider.gameObject.transform.localPosition = new Vector3(0.059f, 0.11275f, -0.04788f);
           // collider.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            collider.gameObject.GetComponent<RedStartPin>().SetIsConneted(true);
        }
        else if(collider.gameObject.name == "Wire_Minus_Start")
        {
            collider.gameObject.transform.localPosition = new Vector3(0.059f, 0.11275f, 0.041f);
          //  collider.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            collider.gameObject.GetComponent<BlackStartPin>().SetIsConneted(true);
        }
    }
}
