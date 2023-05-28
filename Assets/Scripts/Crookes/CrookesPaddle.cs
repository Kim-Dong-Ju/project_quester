// 크룩스관 회전차입 스크립트
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrookesPaddle : MonoBehaviour
{
    public GameObject PowerSupply;
    public GameObject PlusWire;
    public GameObject MinusWire;
    GameObject CathodeRayMinus;
    GameObject CathodeRayPlus;
    GameObject pinWheel;
    bool bIsPowered = false;
    bool bIsSwapped = false;
    float volt = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        CathodeRayMinus = transform.Find("CathodeRay_Minus").gameObject;
        CathodeRayPlus = transform.Find("CathodeRay_Plus").gameObject;
        pinWheel = transform.Find("PinWheel").gameObject;
        volt = PowerSupply.GetComponent<PowerSupply>().GetVoltage();
        CathodeRayMinus.GetComponent<Light>().intensity = 0;
        CathodeRayPlus.GetComponent<Light>().intensity = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetIsPowered(bool bValue)
    {
        bool bPConnect = PlusWire.GetComponent<RedEndPin>().GetIsConneted();
        bool bMConnect = MinusWire.GetComponent<BlackEndPin>().GetIsConneted();

        if(bPConnect && bMConnect)
            bIsPowered = bValue;

        SetLight();
        pinWheel.GetComponent<CrookesWheel>().SetPinWheel(bIsPowered, bIsSwapped);
    }
    
    public void SetIsSwapped(bool bValue)
    {
        bool bPConnect = PlusWire.GetComponent<RedEndPin>().GetIsConneted();
        bool bMConnect = MinusWire.GetComponent<BlackEndPin>().GetIsConneted();

        if(bPConnect && bMConnect)
            bIsSwapped = bValue;
        
        SetLight();
        pinWheel.GetComponent<CrookesWheel>().SetPinWheel(bIsPowered, bIsSwapped);
    }

    public void SetVoltage(float fVolt)
    {
        volt = fVolt;
        pinWheel.GetComponent<CrookesWheel>().SetVoltage(volt);
    }

    private void SetLight()
    {
        if(bIsPowered)
        {
            if(bIsSwapped) // Toggle을 눌렀을 때. 즉 검정핀이 Plus극이, 빨간핀이 Minus극이 되었을 때
            {
                CathodeRayPlus.GetComponent<Light>().intensity = volt;
                CathodeRayMinus.GetComponent<Light>().intensity = 0;
            }
            else
            {
                CathodeRayMinus.GetComponent<Light>().intensity = volt;
                CathodeRayPlus.GetComponent<Light>().intensity = 0;
            }
        }
        else
        {
            CathodeRayMinus.GetComponent<Light>().intensity = 0;
            CathodeRayPlus.GetComponent<Light>().intensity = 0;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.name == "TIP_ERed")
        {
            collider.gameObject.transform.SetParent(this.transform);
            collider.gameObject.transform.localPosition = new Vector3(0.006499985f, 0.1861666f, -0.10425f);
            collider.gameObject.GetComponent<RedEndPin>().SetIsConneted(true);
        }
        else if(collider.gameObject.name == "TIP_EBlack")
        {
            collider.gameObject.transform.SetParent(this.transform);
            collider.gameObject.transform.localPosition = new Vector3(0.006474752f, 0.1862269f, 0.09510541f);
            collider.gameObject.GetComponent<BlackEndPin>().SetIsConneted(true);
        }
    }
}
