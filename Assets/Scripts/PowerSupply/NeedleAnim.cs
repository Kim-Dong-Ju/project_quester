using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class NeedleAnim : MonoBehaviour
{
   // public GameObject ParentObj;
    private PlayableDirector timeline;
    // public TimelineAsset timeline;
    private bool bIsPlaying = false;
    private bool bPowered = false;

    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    private void Awake()
    {
        timeline = GetComponent<PlayableDirector>();
    }


    // Start is called before the first frame update
    void Start()
    {
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
        bPowered = true;
    }

    public void ReverseAnimation()
    {
        if(bIsPlaying) return;
        StartCoroutine(Reverse());
        bPowered = false;
    }

    public void SetNeedleAnim(bool bValue)
    {
        if(bValue)
        {
            // Needle Animation when power supply is on
            bIsPlaying = false;
            PlayAnimation();
        }
        else
        {
            // Needle Animation when power supply is off
            bIsPlaying = false;
            ReverseAnimation();
        }
    }

    IEnumerator Play()
    {
        if(!bPowered)
        {
            bIsPlaying = true;

            float Voltage = transform.parent.GetComponent<PowerSupply>().GetVoltage(); // 0.0 ~ 3.6
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
            while(dt < Voltage)
            {
                dt += Time.deltaTime / (float)timeline.duration;

                timeline.time = Mathf.Max(dt, 0);
                timeline.Evaluate();
                yield return null;
            }

            bIsPlaying = false;
        }
    }

    private IEnumerator Reverse()
    {
        if(bPowered)
        {
            bIsPlaying = true;
            float Voltage = transform.parent.GetComponent<PowerSupply>().GetVoltage(); // 0.0 ~ 3.6

           // float dt = (float)timeline.duration;
           float dt = Voltage;
            while(dt > 0)
            {
                dt -= Time.deltaTime / (float)timeline.duration;

                timeline.time = Mathf.Max(dt, 0);
                timeline.Evaluate();
                yield return null;
            }

            bIsPlaying = false;
        }
    }
}
