// 크룩스관 슬릿입 휘어지는 빛을 구현하기 위한 스크립트
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBezier : MonoBehaviour
{
    [SerializeField]
    Transform[] points;

    public Transform[] Points
    {
        get {
            return this.points;
        }
    }
    // Update is called once per frame
    void Update()
    {
        int count = 5;
        Vector3 prev_pos = this.points[0].position;
        for(int i = 0; i <= count; ++i)
        {
            Vector3 to = bezier(i / (float)count);
            Debug.DrawLine(prev_pos, to);

            prev_pos = to;
        }
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        for(int i = 0; i < this.points.Length; ++i)
        {
            Gizmos.DrawSphere(this.points[i].position, 0.005f);

            if(i < this.points.Length - 1)
            {
                Vector3 current = this.points[i].position;
                Vector3 next = this.points[i + 1].position;

                Gizmos.DrawLine(current, next);
            }
        }
    }

    public Vector3 bezier(float t)
    {
        if(this.points.Length == 3)
        {
            return bezier2(t);
        }
        else if(this.points.Length == 4)
        {
            return bezier3(t);
        }
        return Vector3.zero;
    }
    
    public Vector3 bezier2(float t)
    {
        Vector3 a = this.points[0].position;
        Vector3 b = this.points[1].position;
        Vector3 c = this.points[2].position;

        Vector3 aa = a + (b - a) * t;
        Vector3 bb = b + (c - b) * t;
        return aa + (bb - aa) * t;
    }

    public Vector3 bezier3(float t)
    {
        Vector3 a = this.points[0].position;
        Vector3 b = this.points[1].position;
        Vector3 c = this.points[2].position;
        Vector3 d = this.points[3].position;

        Vector3 aa = a + (b - a) * t;
        Vector3 bb = b + (c - b) * t;
        Vector3 cc = c + (d - c) * t;

        Vector3 aaa = aa + (bb - aa) * t;
        Vector3 bbb = bb + (cc - bb) * t;

        return aaa + (bbb - aaa) * t;
    }
}
