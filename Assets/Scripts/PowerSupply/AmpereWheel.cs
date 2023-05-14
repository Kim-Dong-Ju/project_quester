using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmpereWheel : MonoBehaviour
{
    private Vector3 vLastPos = new Vector3(1f,1f,1f);
    private Vector3 vCurPos = new Vector3(1f,1f,1f);
    private Vector3 vLastRotation = new Vector3(1f,1f,1f);
    private Vector3 vCurRotation = new Vector3(1f,1f,1f);
    private bool bIsChange = false;
    // Start is called before the first frame update
    void Start()
    {
        //vLastRotation = transform.localRotation;
        vLastPos = transform.localPosition;
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
    public void OnInteract()
    {
        bIsChange = true;
    }

    public void OffInteract()
    {
        
    }

    public void SetCurPos(Vector3 vTouchPos)
    {
        vCurPos = vTouchPos;
    }

    private void SetLastPos()
    {
        vLastPos = vCurPos;
    }

    public Vector3 GetCurPos()
    {
        return vCurPos;
    }

    public Vector3 GetLastPos()
    {
        return vLastPos;
    }
}