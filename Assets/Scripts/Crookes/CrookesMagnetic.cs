// 크룩스관 슬릿입 스크립트
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrookesMagnetic : MonoBehaviour
{
    public GameObject PowerSupply;
    public GameObject PlusWire;
    public GameObject MinusWire;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider collider)
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
