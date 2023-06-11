// 크룩스관 +자입 스크립트
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrookesCross : MonoBehaviour
{
    public GameObject PowerSupply;
    // public GameObject PlusWire;
    // public GameObject MinusWire;
    Light CathodeRay;
    RedEndPin PlusWire;
    BlackEndPin MinusWire;
    public bool bIsPowered = false;
    private bool bIsSwapped = false;
    public float Ampere;
    [SerializeField]
    bool bPConnect = false, bMConnect = false;
    
    // Start is called before the first frame update
    void Start()
    {
        Ampere = PowerSupply.GetComponent<PowerSupply>().GetAmpere();
        CathodeRay = transform.Find("CathodeRay").GetComponent<Light>();
        CathodeRay.intensity = 0;
        PlusWire = null;
        MinusWire = null;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Ampere = PowerSupply.GetComponent<PowerSupply>().GetAmpere();
        if(bIsPowered)
        {
            CathodeRay.intensity = Ampere;
        }
        else
        {
            CathodeRay.intensity = 0;
        }
    }
    public void SetIsPowered(bool bValue)
    {
        // if(PlusWire != null)
        //     bPConnect = PlusWire.GetIsConneted();
        // if(MinusWire != null)
        //     bMConnect = MinusWire.GetIsConneted();

        if(bPConnect && bMConnect)
            bIsPowered = bValue;

        if(CathodeRay != null)
        {
            if(bIsPowered)
            {
                CathodeRay.intensity = Ampere;
            }
            else
            {
                CathodeRay.intensity = 0;
            }
        }
    }
    
    public void SetIsSwapped(bool bValue)
    {
        // if(PlusWire != null)
        //     bPConnect = PlusWire.GetIsConneted();
        // if(MinusWire != null)
        //     bMConnect = MinusWire.GetIsConneted();

        if(bPConnect && bMConnect)
            bIsSwapped = bValue;
    }

    public void SetAmpere(float fAmphere)
    {
        Ampere = fAmphere;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.TryGetComponent<RedEndPin>(out RedEndPin redEndPin))
        {
            PlusWire = redEndPin;
            collider.gameObject.transform.SetParent(this.transform);
            collider.gameObject.transform.localPosition = new Vector3(-0.0026f, 0.1706f, 0.1105f);
            collider.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
            redEndPin.SetIsConneted(true);
            bPConnect = true;
        }
        else if(collider.gameObject.TryGetComponent<BlackEndPin>(out BlackEndPin blackEndPin))
        {
            MinusWire = blackEndPin;
            collider.gameObject.transform.SetParent(this.transform);
            collider.gameObject.transform.localPosition = new Vector3(-0.0026f, 0.284f, 0.311f);
            collider.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
            blackEndPin.SetIsConneted(true);
            bMConnect = true;
        }
    }
}
