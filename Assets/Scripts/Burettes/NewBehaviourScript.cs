using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Vector3 targetPosition;
    public float movementSpeed = 5.0f; // 이동 속도 조절

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = new Vector3(23f, 0f, 0f);
    }
    public int count = 1;
    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            if(count == 1){
                // transform.position = Vector3.Lerp(transform.position, targetPosition, movementSpeed * Time.deltaTime);

                Vector3 pos = transform.position;
                pos.x += 0.791f;
                pos.z -= 0.15f;
                transform.position = pos;
                GameObject.Find("WaterParent").transform.Find("WaterFirst").gameObject.SetActive(true);
                GameObject.Find("WaterParent").transform.Find("Cylinder").gameObject.SetActive(true);
                count = 2;
            }   
            else if(count == 2){
                // transform.position = Vector3.Lerp(transform.position, targetPosition, movementSpeed * Time.deltaTime);

                GameObject.Find("WaterParent").transform.Find("WaterFirst").gameObject.SetActive(false);
                GameObject.Find("WaterParent").transform.Find("Cylinder").gameObject.SetActive(false);
                GameObject.Find("WaterParent").transform.Find("WaterSecond").gameObject.SetActive(true);
                count = 3;
            }  
            else if(count == 3){
                // transform.position = Vector3.Lerp(transform.position, targetPosition, movementSpeed * Time.deltaTime);

                // 회전 90도
                GameObject.Find("underWater").transform.Find("Cylinder_1").gameObject.SetActive(true);
                count = 4;
            }  
            else if(count == 4){
                // transform.position = Vector3.Lerp(transform.position, targetPosition, movementSpeed * Time.deltaTime);

                //회전 180도
                GameObject.Find("underWater").transform.Find("Cylinder").gameObject.SetActive(true);
                count = 5;
            }  
            else if(count == 5){
                // transform.position = Vector3.Lerp(transform.position, targetPosition, movementSpeed * Time.deltaTime);

                GameObject.Find("beaker").transform.Find("cylinder_beaker_Liquid").gameObject.SetActive(true);
                GameObject.Find("beaker").transform.Find("Cylinder (2)").gameObject.SetActive(true);
                count = 6;
            }  
        }
    }
}
