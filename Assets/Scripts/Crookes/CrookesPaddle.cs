// 크룩스관 회전차입 스크립트
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrookesPaddle : MonoBehaviour
{
    public GameObject PowerSupply;
    public GameObject PlusWire;
    public GameObject MinusWire;

    public bool bIsPowered = false;
    public bool bIsSwapped = false;
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
