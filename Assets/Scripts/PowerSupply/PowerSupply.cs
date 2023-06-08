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
    private bool bPowered = false;
    private bool bIsMainOn = false;
    private bool bIsSwapped = false;
    private bool bRedCon = false, bBlackCon = false, bAllCon = false; // 전선 연결 확인용
    private bool bIsUp = false;
    private float Ampere = 0;
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
        if(needleAnim == null)
        { Debug.Log("needleAnim == null"); }
        crookesCross = CrookesCrossObj.GetComponent<CrookesCross>();
        if(crookesCross == null)
        { Debug.Log("crookesCross == null"); }
        crookesMagnetic = CrookesMagneticObj.GetComponent<CrookesMagnetic>();
        if(crookesMagnetic == null)
        { Debug.Log("crookesMagnetic == null"); }
        crookesPaddle = CrookesWheelObj.GetComponent<CrookesPaddle>();
        if(crookesPaddle == null)
        { Debug.Log("crookesPaddle == null"); }
        //needleAnim.SetNeedleAnim(false);
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
        if(crookesPaddle != null)
            crookesPaddle.SetIsPowered(bPowered);
        if(crookesCross != null)
            crookesCross.SetIsPowered(bPowered);
        if(crookesMagnetic != null)
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
        if(crookesPaddle != null)
            crookesPaddle.SetIsSwapped(bIsSwapped);
        if(crookesCross != null)
            crookesCross.SetIsSwapped(bIsSwapped);
        if(crookesMagnetic != null)
            crookesMagnetic.SetIsSwapped(bIsSwapped);
    }

    public void SetAmpere(float value)
    {
        Debug.Log("PowerSupply Ampere: " + Ampere);
        Ampere = value;
        NeedleUpChange(bIsUp);
        if(crookesCross != null)
            crookesCross.SetAmpere(Ampere);
        if(crookesPaddle != null)
            crookesPaddle.SetAmpere(Ampere);
        if(crookesMagnetic != null)
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

    public bool GetAllWireConnected()
    {
        return bAllCon;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.name == "Wire_Plus_Start")
        {
            collider.gameObject.transform.localPosition = new Vector3(0.059f, 0.11275f, -0.04788f);
           // collider.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            collider.gameObject.GetComponent<RedStartPin>().SetIsConneted(true);
            bRedCon = true;
            if(bBlackCon)
            { bAllCon = true; }
        }
        else if(collider.gameObject.name == "Wire_Minus_Start")
        {
            collider.gameObject.transform.localPosition = new Vector3(0.059f, 0.11275f, 0.041f);
          //  collider.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            collider.gameObject.GetComponent<BlackStartPin>().SetIsConneted(true);
            bBlackCon = true;
            if(bRedCon)
            { bAllCon = true; }
        }
    }
}
