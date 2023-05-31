using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class NeedleAnim : MonoBehaviour
{
   // public GameObject ParentObj;
    private PlayableDirector timeline;
    PowerSupply powerSupply;
    // public TimelineAsset timeline;
    private bool bIsPlaying = false;
    private bool bPowered = false;
    private bool bChange = false;
    float lastAmpere;

    private void Awake()
    {
        timeline = GetComponent<PlayableDirector>();
    }


    // Start is called before the first frame update
    void Start()
    {
        powerSupply = transform.parent.gameObject.GetComponent<PowerSupply>();
        transform.localRotation = Quaternion.Euler(new Vector3(0, -140, 0));
        Debug.Log(timeline.duration);
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    public void PlayAnimation()
    {
        if(bIsPlaying) return;
        StartCoroutine(Play());
    }

    public void ReverseAnimation()
    {
        if(bIsPlaying) return;
        StartCoroutine(Reverse());
    }

    public void SetNeedleAnim(bool bValue) // 전원을 켜고 끌 때 사용하게 할 애니메이션
    {
        if(bValue)
        {
            // Needle Animation when power supply is on
            bIsPlaying = false;
            PlayAnimation();
            bPowered = true;
        }
        else
        {
            // Needle Animation when power supply is off
            bIsPlaying = false;
            ReverseAnimation(); 
            bPowered = false;
        }
    }

    public void AmpereUpAnim(bool value) // 전류를 변화시킬 때 사용하게 할 애니메이션
    {
        if(!bPowered) return;
        if(value)
        {
            bChange = true;
            bIsPlaying = false;
            PlayAnimation();
        }
        else
        {
            bChange = true;
            bIsPlaying = false;
            ReverseAnimation();
        }
    }

    IEnumerator Play()
    {
        float Ampere = powerSupply.GetAmpere() / 10.0f;
        // powerSupply.GetVoltage() => 0.0 ~ 3.6 이므로 Ampere는 0.0 ~ 0.36임
        // Needle Animation은 0.36초로 재생되게 설정
        if(!bPowered)
        {
            bIsPlaying = true;
           
            // int dt = -140; // 0
            // float check = (float)(dt + 40) * 60 / 100;
            // // needle -140 ~ -40     100

            // while(check < yRotate)
            // {
            //     dt += 1;
                
            //     transform.Rotate(new Vector3(0, dt, 0));
            //     check = (float)(dt + 40) * 60 / 100;
            //     yield return null;
            // }
           // transform.localRotation = Quaternion.Euler(new Vector3(0, dt + (yRotate/2), 0));

            float dt = 0;
            while(dt < Ampere)
            {
                dt += Time.deltaTime / (float)timeline.duration;

                timeline.time = Mathf.Max(dt, 0);
                timeline.Evaluate();
                yield return null;
            }

            bIsPlaying = false;
            lastAmpere = Ampere;
        }
        else // 만약 전원이 켜진 상태라면, 즉 전원이 켜진 상태에서 전압만 변경한다면
        {
            bIsPlaying = true;
           
            // int dt = -140; // 0
            // float check = (float)(dt + 40) * 60 / 100;
            // // needle -140 ~ -40     100

            // while(check < yRotate)
            // {
            //     dt += 1;
                
            //     transform.Rotate(new Vector3(0, dt, 0));
            //     check = (float)(dt + 40) * 60 / 100;
            //     yield return null;
            // }
           // transform.localRotation = Quaternion.Euler(new Vector3(0, dt + (yRotate/2), 0));

            float dt = lastAmpere;
            while(dt < Ampere)
            {
                dt += Time.deltaTime / (float)timeline.duration;

                timeline.time = Mathf.Max(dt, 0);
                timeline.Evaluate();
                yield return null;
            }

            bIsPlaying = false;
            lastAmpere = Ampere;
        }
    }

    private IEnumerator Reverse()
    {
        float Ampere = powerSupply.GetAmpere() / 10.0f;
        // powerSupply.GetVoltage() => 0.0 ~ 3.6 이므로 Ampere는 0.0 ~ 0.36임
        // Needle Animation은 0.36초로 재생되게 설정
        if(bPowered)
        {
            bIsPlaying = true;
            

           // float dt = (float)timeline.duration;
           float dt = Ampere;
            while(dt > 0)
            {
                dt -= Time.deltaTime / (float)timeline.duration;

                timeline.time = Mathf.Max(dt, 0);
                timeline.Evaluate();
                yield return null;
            }

            bIsPlaying = false;
            lastAmpere = Ampere; 
        }
        else
        {
            bIsPlaying = true;

           // float dt = (float)timeline.duration;
           float dt = lastAmpere;
            while(dt > Ampere)
            {
                dt -= Time.deltaTime / (float)timeline.duration;

                timeline.time = Mathf.Max(dt, 0);
                timeline.Evaluate();
                yield return null;
            }

            bIsPlaying = false;
            lastAmpere = Ampere; 
        }
    }
}
