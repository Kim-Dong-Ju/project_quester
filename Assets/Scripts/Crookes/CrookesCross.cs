using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrookesCross : MonoBehaviour
{
    public GameObject PowerSupply;
    public GameObject PlusWire;
    public GameObject MinusWire;
    public GameObject CathodeRay;
    public bool bIsPowered = false;
    private bool bIsSwapped = false;
    public float volt;
    
    // Start is called before the first frame update
    void Start()
    {
        volt = PowerSupply.GetComponent<PowerSupply>().GetVoltage();
        CathodeRay.GetComponent<Light>().intensity = 0;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        volt = PowerSupply.GetComponent<PowerSupply>().GetVoltage();
        if(bIsPowered)
        {
            CathodeRay.GetComponent<Light>().intensity = volt;
        }
        else
        {
            CathodeRay.GetComponent<Light>().intensity = 0;
        }
    }
    public void SetIsPowered(bool bValue)
    {
        bool bPConnect = PlusWire.GetComponent<RedEndPin>().GetIsConneted();
        bool bMConnect = MinusWire.GetComponent<BlackEndPin>().GetIsConneted();

        if(bPConnect && bMConnect)
            bIsPowered = bValue;

        if(bIsPowered)
        {
            CathodeRay.GetComponent<Light>().intensity = volt;
        }
        else
        {
            CathodeRay.GetComponent<Light>().intensity = 0;
        }
    }
    
    public void SetIsSwapped(bool bValue)
    {
        bool bPConnect = PlusWire.GetComponent<RedEndPin>().GetIsConneted();
        bool bMConnect = MinusWire.GetComponent<BlackEndPin>().GetIsConneted();

        if(bPConnect && bMConnect)
            bIsSwapped = bValue;
    }

    public void SetVoltage(float fVolt)
    {
        volt = fVolt;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.name == "TIP_ERed")
        {
            collider.gameObject.transform.SetParent(this.transform);
            collider.gameObject.transform.localPosition = new Vector3(-0.0026f, 0.1706f, 0.1105f);
            collider.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
            collider.gameObject.GetComponent<RedEndPin>().SetIsConneted(true);
        }
        else if(collider.gameObject.name == "TIP_EBlack")
        {
            collider.gameObject.transform.SetParent(this.transform);
            collider.gameObject.transform.localPosition = new Vector3(-0.0026f, 0.284f, 0.311f);
            collider.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
            collider.gameObject.GetComponent<BlackEndPin>().SetIsConneted(true);
        }
    }
}
