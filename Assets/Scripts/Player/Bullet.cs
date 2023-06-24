using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroySelf",2.0f); 
    }

   void DestroySelf(){
        Destroy(gameObject);
   }
}
