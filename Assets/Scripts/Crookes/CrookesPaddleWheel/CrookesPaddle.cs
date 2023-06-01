// 크룩스관 회전차입 스크립트
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrookesPaddle : MonoBehaviour
{
    public GameObject PowerSupply;
    // public GameObject PlusWire;
    // public GameObject MinusWire;
    Light CathodeRayPlus, CathodeRayMinus;
    CrookesWheel pinWheel;
    RedEndPin PlusWire;
    BlackEndPin MinusWire;
    bool bIsPowered = false;
    bool bIsSwapped = false;
    bool bPConnect = false, bMConnect = false;
    float Ampere = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        CathodeRayMinus = transform.Find("CathodeRay_Minus").gameObject.GetComponent<Light>();
        CathodeRayPlus = transform.Find("CathodeRay_Plus").gameObject.GetComponent<Light>();
        pinWheel = transform.Find("PinWheel").gameObject.GetComponent<CrookesWheel>();
        Ampere = PowerSupply.GetComponent<PowerSupply>().GetAmpere();
        CathodeRayMinus.GetComponent<Light>().intensity = 0;
        CathodeRayPlus.intensity = 0;
    }

    // Update is called once per frame
    

    public void SetIsPowered(bool bValue)
    {
        if(PlusWire != null)
            bPConnect = PlusWire.GetIsConneted();
        if(MinusWire != null)
            bMConnect = MinusWire.GetIsConneted();
        // NULL 체크

        if(bPConnect && bMConnect)
            bIsPowered = bValue;

        SetLight();
        pinWheel.SetPinWheel(bIsPowered, bIsSwapped);
    }
    
    public void SetIsSwapped(bool bValue)
    {
        if(PlusWire != null)
            bPConnect = PlusWire.GetIsConneted();
        if(MinusWire != null)
            bMConnect = MinusWire.GetIsConneted();

        if(bPConnect && bMConnect)
            bIsSwapped = bValue;
        
        SetLight();
        pinWheel.SetPinWheel(bIsPowered, bIsSwapped);
    }

    public void SetAmpere(float fAmphere)
    {
        Ampere = fAmphere;
        pinWheel.SetAmpere(Ampere);
    }

    private void SetLight()
    {
        if(bIsPowered)
        {
            if(bIsSwapped) // Toggle을 눌렀을 때. 즉 검정핀이 Plus극이, 빨간핀이 Minus극이 되었을 때
            {
                CathodeRayPlus.intensity = 0;
                CathodeRayMinus.intensity = Ampere;
            }
            else
            {
                CathodeRayMinus.intensity = 0;
                CathodeRayPlus.intensity = Ampere;
            }
        }
        else
        {
            CathodeRayMinus.intensity = 0;
            CathodeRayPlus.intensity = 0;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.TryGetComponent<RedEndPin>(out RedEndPin redEndPin))
        {
            PlusWire = redEndPin;
            collider.gameObject.transform.SetParent(this.transform);
            collider.gameObject.transform.localPosition = new Vector3(0.006499985f, 0.1861666f, -0.10425f);
            redEndPin.SetIsConneted(true);
        }
        else if(collider.gameObject.TryGetComponent<BlackEndPin>(out BlackEndPin blackEndPin))
        {
            MinusWire = blackEndPin;
            collider.gameObject.transform.SetParent(this.transform);
            collider.gameObject.transform.localPosition = new Vector3(0.006474752f, 0.1862269f, 0.09510541f);
            blackEndPin.SetIsConneted(true);
        }
    }
}
