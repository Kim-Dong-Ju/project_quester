using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LearnManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
         if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main");
        }
    }

    public void Back()
    {
        SceneManager.LoadScene("Main");
    }

    public void ElectricScaleBtn()
    {
        Debug.Log("전자저울");
    }

    public void FlaskBtn()
    {
        Debug.Log("플라스크");
    }

    public void PowerSupplyBtn()
    {
        SceneManager.LoadScene("PowerSupply");
    }

    public void BuretteBtn()
    {
        Debug.Log("뷰렛");
    }

    public void StirrerBtn()
    {
        Debug.Log("전자교반기");
    }
}
