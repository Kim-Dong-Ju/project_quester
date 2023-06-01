// 크룩스관 슬릿입 스크립트
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrookesMagnetic : MonoBehaviour
{
    public GameObject PowerSupply;
    RedEndPin PlusWire;
    BlackEndPin MinusWire;
    private bool bIsPowered = false;
    private bool bIsSwapped = false;
    private bool bPConnect = false, bMConnect = false;
    private float Ampere = 0.0f;
    LineRenderer CathodeRay;
    CathodeRayRenderer ray_renderer;

    // Start is called before the first frame update
    void Start()
    {
        CathodeRay = GameObject.Find("BazierLaser").GetComponent<LineRenderer>();
        ray_renderer = GameObject.Find("BazierLaser").GetComponent<CathodeRayRenderer>();
        CathodeRay.enabled = false;
    }

    // Update is called once per frame
    // void LateUpdate()
    // {
    //     if(bIsPowered)
    //     {
    //         if(!CathodeRay.enabled)
    //             CathodeRay.enabled = true;
    //     }
    //     else
    //     {
    //         if(CathodeRay.enabled)
    //             CathodeRay.enabled = false;
    //     }
    // }
    public void SetIsPowered(bool bValue)
    {
        if(PlusWire != null)
            bPConnect = PlusWire.GetIsConneted();
        if(MinusWire != null)
            bMConnect = MinusWire.GetIsConneted();
        

        if(bPConnect && bMConnect)
            bIsPowered = bValue;

        if(bIsPowered)
        {
            CathodeRay.enabled = true;
        }
        else
        {
            CathodeRay.enabled = false;
        }
    }
    
    public void SetIsSwapped(bool bValue)
    {
        if(PlusWire != null)
            bPConnect = PlusWire.GetIsConneted();
        if(MinusWire != null)
            bMConnect = MinusWire.GetIsConneted();

        if(bPConnect && bMConnect)
            bIsSwapped = bValue;
    }

    public void SetAmpere(float fAmphere)
    {
        Ampere = fAmphere;
        ray_renderer.SetIntensity(Ampere);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.TryGetComponent<RedEndPin>(out RedEndPin redEndPin))
        {
            PlusWire = redEndPin;
            collider.gameObject.transform.SetParent(this.transform);
            collider.gameObject.transform.localPosition = new Vector3(0.2729f, 0.2451f, -0.0024f);
            collider.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 90, -90));
            redEndPin.SetIsConneted(true);
        }
        else if(collider.gameObject.TryGetComponent<BlackEndPin>(out BlackEndPin blackEndPin))
        {
            MinusWire = blackEndPin;
            collider.gameObject.transform.SetParent(this.transform);
            collider.gameObject.transform.localPosition = new Vector3(-0.2648999f, 0.2475f, -0.002125005f);
            collider.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, -90, 90));
            blackEndPin.SetIsConneted(true);
        }
    }
}
