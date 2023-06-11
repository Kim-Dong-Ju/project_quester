using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetic : MonoBehaviour
{
    public GameObject Mag_South;
    public GameObject Mag_North;

    void Start()
    {
        
    }

    public Vector3 GetSouthPosition()
    {
        return Mag_South.transform.position;
    }

    public Vector3 GetNorthPosition()
    {
        return Mag_North.transform.position;
    }

    // public void ButtonClick()
    // {
    //     transform.Rotate(0, -180, 0);
    // }
}
