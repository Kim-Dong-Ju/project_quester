// 라인 렌더링용 머티리얼 관리 코드
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedCRs : MonoBehaviour
{
    public int materialIndex = 0;
    public Vector2 crAnimationRate = new Vector2(1.0f, 0.0f);
    public string textureName = "_MainTex";
    
    Vector2 crOffset = Vector2.zero;
    // Update is called once per frame
    void LateUpdate()
    {
        crOffset += (crAnimationRate * Time.deltaTime);
        if(GetComponent<Renderer>().enabled)
        {
            GetComponent<Renderer>().materials[materialIndex].SetTextureOffset(textureName, crOffset);
        }
    }
}
