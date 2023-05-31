using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSupply : MonoBehaviour
{
    // GameObject Wire_Plus_Start;
    // GameObject Wire_Minus_Start;
    // GameObject OnOffSubButton;
    // GameObject OnOffMainSwitch;
    public GameObject Needle;
    public GameObject CrookesWheelObj;
    public GameObject CrookesMagneticObj;
    public GameObject CrookesCrossObj;
   // bool bPlusStartConnected = false;
   // bool bMinusStartConnected = false;
    protected bool bPowered = false;
    private bool bIsMainOn = false;
    protected bool bIsSwapped = false;
    protected bool bIsUp = false;
    protected float Ampere = 0;
    NeedleAnim needleAnim;
    RedStartPin redStartPin;
    BlackStartPin blackStartPin;
    OnOffMain onOffMain;
    OnOffSub onOffSub;
    CrookesCross crookesCross;
    CrookesMagnetic crookesMagnetic;
    CrookesPaddle crookesPaddle;
    
    // Start is called before the first frame update
    void Start()
    {
        redStartPin = transform.Find("Wire_Plus_Start").gameObject.GetComponent<RedStartPin>();    
        blackStartPin = transform.Find("Wire_Minus_Start").gameObject.GetComponent<BlackStartPin>();
        onOffSub = transform.Find("ON_OFF_Sub").gameObject.GetComponent<OnOffSub>();
        onOffMain = transform.Find("ON_OFF_Main").gameObject.GetComponent<OnOffMain>();
        needleAnim = Needle.GetComponent<NeedleAnim>();
        crookesCross = CrookesCrossObj.GetComponent<CrookesCross>();
        crookesMagnetic = CrookesMagneticObj.GetComponent<CrookesMagnetic>();
        crookesPaddle = CrookesWheelObj.GetComponent<CrookesPaddle>();
        needleAnim.SetNeedleAnim(false);
    }

    // Update is called once per frame
    // void Update()
    // {

    // }

    public void SetIsPowered(bool bValue)
    {
        bPowered = bValue;

        redStartPin.SetIsPowered(bPowered);
        blackStartPin.SetIsPowered(bPowered);
        SetNeedleAnim(bPowered);
        crookesPaddle.SetIsPowered(bPowered);
        crookesCross.SetIsPowered(bPowered);
        crookesMagnetic.SetIsPowered(bPowered);
    }

    public void SetIsMainOn(bool bValue)
    {
        bIsMainOn = bValue;
        SetIsPowered(bValue);
    }

    public void SetSwap(bool bValue)
    {
        bIsSwapped = bValue;

        redStartPin.SetIsPlus(bIsSwapped);
        blackStartPin.SetIsMinus(bIsSwapped);
        crookesPaddle.SetIsSwapped(bIsSwapped);
        crookesCross.SetIsSwapped(bIsSwapped);
        crookesMagnetic.SetIsSwapped(bIsSwapped);
    }

    public void SetAmpere(float value)
    {
        Debug.Log("PowerSupply Ampere: " + Ampere);
        Ampere = value;
        NeedleUpChange(bIsUp);
        crookesCross.SetAmpere(Ampere);
        crookesPaddle.SetAmpere(Ampere);
        crookesMagnetic.SetAmpere(Ampere);
    }

    public void SetNeedleAnim(bool value)
    {
        needleAnim.SetNeedleAnim(value);
    }

    private void NeedleUpChange(bool value)
    {
        needleAnim.AmpereUpAnim(value);
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

    public float GetAmpere()
    {
        return Ampere;
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
