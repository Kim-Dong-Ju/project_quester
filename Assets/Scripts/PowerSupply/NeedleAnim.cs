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

            float dt = 0;
            while(dt < timeline.duration)
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

            float dt = (float)timeline.duration;
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
