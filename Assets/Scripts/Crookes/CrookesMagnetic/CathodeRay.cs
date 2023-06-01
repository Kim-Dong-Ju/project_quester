// 테스트용 코드
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CathodeRay : MonoBehaviour
{
    public int DivideNum = 10;
    public float R = 10.0f;
    public float RAV_WIDTH = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int divideNum = (int)((2.0f * Mathf.PI * R) / DivideNum);
        float angleDiff = 2.0f * Mathf.PI / divideNum;
        float r1 = (R - RAV_WIDTH / 2.0f);
        float r2 = (R + RAV_WIDTH / 2.0f);

        float angle1 = 0.0f;
        float angle2 = angleDiff;

        Mesh mesh = GetComponent<MeshFilter>().mesh;

        Debug.Log(divideNum * 4);

        Vector3[] vertices = new Vector3[divideNum * 4];
        Vector2[] uv = new Vector2[divideNum * 4];
        int[] triangles = new int[divideNum * 6];

        int triangleCount = 0;
        for(int i = 0; i < divideNum * 4; i += 4)
        {
            // vertex
            vertices[i] = new Vector3(r2 * Mathf.Cos(angle1), r2 * Mathf.Sin(angle1), 0);
            vertices[i + 1] = new Vector3(r2 * Mathf.Cos(angle2), r2 * Mathf.Sin(angle2), 0);
            vertices[i + 2] = new Vector3(r1 * Mathf.Cos(angle1), r1 * Mathf.Sin(angle1), 0);
            vertices[i + 3] = new Vector3(r1 * Mathf.Cos(angle2), r1 * Mathf.Sin(angle2), 0);

            angle1 += angleDiff;
            angle2 += angleDiff;

            // uv(texture coordination)
            uv[i] = new Vector2(1, 0);
            uv[i + 1] = new Vector2(1, 1);
            uv[i + 2] = new Vector2(0, 0);
            uv[i + 3] = new Vector2(0, 1);

            // triangles
            triangles[triangleCount] = i;
            triangles[triangleCount + 1] = i + 1;
            triangles[triangleCount + 2] = i + 2;
            triangles[triangleCount + 3] = i + 2;
            triangles[triangleCount + 4] = i + 1;
            triangles[triangleCount + 5] = i + 3;

            triangleCount += 6;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
}
