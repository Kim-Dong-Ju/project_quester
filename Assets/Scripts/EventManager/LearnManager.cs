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
        SceneManager.LoadScene("Electric");
    }

    public void FlaskBtn()
    {
        SceneManager.LoadScene("Flask");;
    }

    public void PowerSupplyBtn()
    {
        SceneManager.LoadScene("PowerSupply");
    }

    public void BuretteBtn()
    {
        SceneManager.LoadScene("Burette");
    }

    public void StirrerBtn()
    {
        SceneManager.LoadScene("Stirrer");
    }
}
