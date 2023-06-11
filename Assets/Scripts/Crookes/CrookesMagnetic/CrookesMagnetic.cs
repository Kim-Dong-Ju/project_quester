// 크룩스관 슬릿입 스크립트
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrookesMagnetic : MonoBehaviour
{
    public GameObject PowerSupply;
    public GameObject BezierLaser, BezierPoint;
    RedEndPin PlusWire;
    BlackEndPin MinusWire;
    [SerializeField]
    private bool bIsPowered = false;
    private bool bIsSwapped = false;
    private bool bPConnect = false, bMConnect = false;
    [SerializeField]
    private float Ampere = 0.0f;
    LineRenderer CathodeRay;
    CathodeRayRenderer ray_renderer;
    Light CathodeRayMinus, CathodeRayPlus;
    // Start is called before the first frame update
    void Start()
    {
        CathodeRay = BezierLaser.GetComponent<LineRenderer>();
        ray_renderer = BezierLaser.GetComponent<CathodeRayRenderer>();
        CathodeRayMinus = transform.Find("CathodeRay_Minus").gameObject.GetComponent<Light>();
        CathodeRayPlus = transform.Find("CathodeRay_Plus").gameObject.GetComponent<Light>();
        CathodeRay.enabled = false;
        CathodeRayMinus.intensity = 0;
        CathodeRayPlus.intensity = 0;
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

        if(CathodeRay != null)
        {
            if(bIsPowered)
            {
                CathodeRay.enabled = true;
            }
            else
            {
                CathodeRay.enabled = false;
            }
        }
        SetLight();
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
    }

    public void SetAmpere(float fAmphere)
    {
        Ampere = fAmphere;
        if(ray_renderer != null)
            ray_renderer.SetIntensity(Ampere);
    }

    private void SetLight()
    {
        if(CathodeRayPlus != null && CathodeRayMinus != null && CathodeRay != null)
        {
            if(bIsPowered)
            {
                if(bIsSwapped) // Toggle을 눌렀을 때. 즉 검정핀이 Plus극이, 빨간핀이 Minus극이 되었을 때
                {
                    CathodeRayPlus.intensity = 0;
                    CathodeRayMinus.intensity = Ampere;
                    CathodeRay.enabled = false;
                }
                else
                {
                    CathodeRayMinus.intensity = 0;
                    CathodeRayPlus.intensity = Ampere;
                    CathodeRay.enabled = true;
                }
            }
            else
            {
                CathodeRayMinus.intensity = 0;
                CathodeRayPlus.intensity = 0;
            }
        }
    }

    public void SetMagnetic(GameObject obj)
    {
        BezierPoint.GetComponent<BezierPointController>().SetMagnetic(obj);
    }

    public void DeleteMagnetic()
    {
        BezierPoint.GetComponent<BezierPointController>().DeleteMagnetic();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.TryGetComponent<RedEndPin>(out RedEndPin redEndPin))
        {
            PlusWire = redEndPin;
            collider.gameObject.transform.SetParent(this.transform);
            collider.gameObject.transform.localPosition = new Vector3(-0.2648999f, 0.2475f, -0.002125005f);
            collider.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, -90, 90));
            redEndPin.SetIsConneted(true);
            bPConnect = true;
        }
        else if(collider.gameObject.TryGetComponent<BlackEndPin>(out BlackEndPin blackEndPin))
        {
            MinusWire = blackEndPin;
            collider.gameObject.transform.SetParent(this.transform);
            collider.gameObject.transform.localPosition = new Vector3(0.2729f, 0.2451f, -0.0024f);
            collider.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 90, -90));
            blackEndPin.SetIsConneted(true);
            bMConnect = true;
        }
    }
}
