// 크룩스관 슬릿입 스크립트
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrookesMagnetic : MonoBehaviour
{
    public GameObject PowerSupply;
    public GameObject PlusWire;
    public GameObject MinusWire;
    private bool bIsPowered = false;
    private bool bIsSwapped = false;
    private float volt = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
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
            collider.gameObject.transform.localPosition = new Vector3(0.2729f, 0.2451f, -0.0024f);
            collider.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 90, -90));
            collider.gameObject.GetComponent<RedEndPin>().SetIsConneted(true);
        }
        else if(collider.gameObject.name == "TIP_EBlack")
        {
            collider.gameObject.transform.SetParent(this.transform);
            collider.gameObject.transform.localPosition = new Vector3(-0.2648999f, 0.2475f, -0.002125005f);
            collider.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, -90, 90));
            collider.gameObject.GetComponent<BlackEndPin>().SetIsConneted(true);
        }
    }
}
