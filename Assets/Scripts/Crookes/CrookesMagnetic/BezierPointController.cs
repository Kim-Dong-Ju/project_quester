using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BezierPointController : MonoBehaviour
{
    public float stdXDist = 0.099f;
    public float stdDist = 0.18f;
    Vector3 point1Origin, point2Origin, point3Origin, point4Origin;
    Vector3 Mag_SouthPos;
    Vector3 Mag_NorthPos;
    public GameObject Magnetic;
    // public GameObject Mag_South;
    // public GameObject Mag_North;
    GameObject Mag_South;
    GameObject Mag_North;
    public GameObject point1;
    public GameObject point2;
    public GameObject point3;
    public GameObject point4;
    int count = 0;

    void Start()
    {
        point1Origin = point1.transform.position;
        point2Origin = point2.transform.position;
        point3Origin = point3.transform.position;
        point4Origin = point4.transform.position;
        Mag_South = Magnetic.transform.Find("Magnetic_S").gameObject;
        Mag_North = Magnetic.transform.Find("Magnetic_N").gameObject;
    }

    void LateUpdate()
    {
        if(count++ > 60)
        {
            Mag_NorthPos = Mag_North.transform.position;
            Mag_SouthPos = Mag_South.transform.position;
            float northDistToP1 = Mathf.Abs(point1Origin.x - Mag_NorthPos.x);
            float southDistToP1 = Mathf.Abs(point1Origin.x - Mag_SouthPos.x);
            float northDistToP4 = Vector3.Distance(point4Origin, Mag_NorthPos);
            float southDistToP4 = Vector3.Distance(point4Origin, Mag_SouthPos);
            Debug.Log("distance from point4 and Magnetic South: " + southDistToP4);
            Debug.Log("distance from point4 and Magnetic North: " + northDistToP4);
            if(northDistToP1 < stdXDist) // 만약 N극이 슬릿입과 더 가깝다면
            {
                float xDist = stdXDist - northDistToP1;
                if(northDistToP4 < stdDist)
                {
                    float distP4 = stdDist - northDistToP4;
                    point1.transform.position = point1Origin + (Vector3.down * (xDist + distP4) * 0.2f);
                    point2.transform.position = point2Origin + (Vector3.down * (xDist + distP4) * 0.05f);
                }
                else
                    point1.transform.position = point1Origin + (Vector3.down * xDist * 0.1f);
            }
            else if(southDistToP1 < stdXDist)
            {
                float xDist = stdXDist - southDistToP1;
                if(southDistToP4 < stdDist)
                {
                    float distP4 = stdDist - southDistToP4;
                    point1.transform.position = point1Origin + (Vector3.up * (xDist + distP4) * 0.2f);
                    point2.transform.position = point2Origin + (Vector3.up * (xDist + distP4) * 0.05f);
                }
                else
                    point1.transform.position = point1Origin + (Vector3.up * xDist * 0.1f);
            }
            else
            {
                point1.transform.position = point1Origin;
                point2.transform.position = point2Origin;
            }
            count = 0;
        }
    }
}
