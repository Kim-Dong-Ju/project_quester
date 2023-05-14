using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class ToggleAnim : MonoBehaviour
{
   // public GameObject ParentObj;
    private PlayableDirector timeline;
    // public TimelineAsset timeline;
    private bool bIsPlaying = false;
    private bool bSwapped = false;

    private void Awake()
    {
        timeline = GetComponent<PlayableDirector>();
    }


    // Start is called before the first frame update
    void Start()
    {
        transform.localRotation = Quaternion.Euler(new Vector3(75, 180, 180));
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    public void PlayAnimation()
    {
        if(bIsPlaying) return;
        StartCoroutine(Play());
        bSwapped = true;
    }

    public void ReverseAnimation()
    {
        if(bIsPlaying) return;
        StartCoroutine(Reverse());
        bSwapped = false;
    }

    public void SetToggleAnim(bool bValue)
    {
        if(bValue)
        {
            // Needle Animation when power supply is on
            PlayAnimation();
        }
        else
        {
            // Needle Animation when power supply is off
           ReverseAnimation();
        }
    }

    IEnumerator Play()
    {
        if(!bSwapped)
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
        if(bSwapped)
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
