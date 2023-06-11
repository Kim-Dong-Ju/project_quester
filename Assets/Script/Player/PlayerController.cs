using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.SetActive(false);

        // Vector2 pos = transform.position;
        // pos.x += 3;
        // transform.position = pos;
    }

    // Update is called once per frame

    public float speed = 0.01f;
    public GameObject BulletPrefab;
    public float bulletSpeed = 100;
    void Update()
    {
        if(Input.GetKey(KeyCode.W)){
            transform.Translate(0,speed,0);
            Debug.Log("w");
        }
        if(Input.GetKey(KeyCode.A)){
            transform.Translate(-speed,0,0);
        }
        if(Input.GetKey(KeyCode.S)){
            transform.Translate(0,-speed,0);
        }
        if(Input.GetKey(KeyCode.D)){
            transform.Translate(speed,0,0);
        }

        if(Input.GetKeyDown(KeyCode.Space)){

            for(int i = 0; i < 3; i++){
            GameObject bullet = Instantiate(BulletPrefab);

            Vector3 bulletStartPos = transform.position + new Vector3(0,0.5f,0);
            bulletStartPos.y += i * 0.3f;
            bullet.transform.position = bulletStartPos;

            bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bulletSpeed);        
            }
        }
    }
}
