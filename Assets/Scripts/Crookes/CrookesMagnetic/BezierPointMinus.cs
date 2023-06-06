using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierPointMinus : MonoBehaviour
{
    private Quaternion initialRotation;
    private const float maxRotation = 360f;
    private const float minRotation = 0f;
    public int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        initialRotation = transform.rotation;   
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Quaternion relativeRotation = Quaternion.Inverse(initialRotation) * transform.rotation;

        Vector3 eulerAngles = relativeRotation.eulerAngles;

        if(count++ < 360f)
        {
            eulerAngles.y += 1;
        }
        else if(count == 360f)
        {
            eulerAngles.y = 0;
        }
        else if(count++ < 720f)
        {
            eulerAngles.y -= 1;
        }
        else
        {
            eulerAngles.y = 0;
            count = 0;
        }

        if(eulerAngles.y > maxRotation)
        {
            eulerAngles.y = maxRotation;
        }

        if(eulerAngles.y < minRotation)
        {
            eulerAngles.y = minRotation;
        }

        Quaternion limitedRotation = Quaternion.Euler(eulerAngles);
        transform.rotation = initialRotation * limitedRotation;
    }

    
}
