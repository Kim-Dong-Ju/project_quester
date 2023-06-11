using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SecondAct : MonoBehaviour
{
    public int count = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if(count == 2 && Input.GetMouseButtonDown(0)){
            for(int i = 0; i < 19 ; i++)
            {
                GameObject.Find("WaterParent").transform.Find("Water").transform.localScale += new Vector3(0,0.2f,0);
                GameObject.Find("WaterParent").transform.Find("Water").transform.Translate(0,-0.1f,0);
            }
            count = 3;
        }    
    }
}
