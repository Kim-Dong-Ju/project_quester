// CBezier를 통해 그린 베지어 곡선을 토대로 사이에 Node들을 만들어 라인 렌더링을 하는 코드
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(CBezier))]
public class CathodeRayRenderer : MonoBehaviour
{
    [SerializeField]
    int node_count;

    LineRenderer line_renderer;
    CBezier bezier;
    Material mat;

    void Awake()
    {
        this.line_renderer = gameObject.GetComponent<LineRenderer>();
        this.bezier = gameObject.GetComponent<CBezier>();
        this.mat = line_renderer.material;
        set_vertex_count(node_count + 1);
    }
    
    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i <= node_count; ++i)
        {
            Vector3 to = this.bezier.bezier(i / (float)node_count);
            this.line_renderer.SetPosition(i, to);
        }
    }

    void set_vertex_count(int count)
    {
        this.line_renderer.positionCount = count;
    }

    public void SetIntensity(float intensity)
    {
        Color baseColor = mat.GetColor("_EmissionColor");
        mat.SetColor("_EmissionColor", baseColor * intensity * 10);
    }
}
