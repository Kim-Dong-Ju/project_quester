using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainSceneManager : MonoBehaviour
{
    public GameObject mainPanel, img, ContactPanel, LearnText, ContactText, ExitText, BackText;
    private Color basic;
    private bool EscapeClick = false;
    // Start is called before the first frame update
    void Start()
    {
        ContactPanel.SetActive(false);
        EscapeClick = false;
        basic = LearnText.GetComponent<TMP_Text>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if(Application.platform == RuntimePlatform.Android)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if(EscapeClick) Application.Quit();
                EscapeClick = true;
            }
        }
    }

    public void QuitApplication()
    {
        ExitText.GetComponent<TMP_Text>().color = Color.white;
        Application.Quit();
    }

    public void Learn()
    {
        LearnText.GetComponent<TMP_Text>().color = Color.white;
        SceneManager.LoadScene("Learn");
    }

    public void Contact()
    {
        ContactText.GetComponent<TMP_Text>().color = Color.white;
        mainPanel.SetActive(false);
        img.SetActive(false);
        ContactPanel.SetActive(true);
    }

    public void BacktoMain()
    {
        BackText.GetComponent<TMP_Text>().color = Color.white;
        ContactText.GetComponent<TMP_Text>().color = basic;
        ContactPanel.SetActive(false);
        mainPanel.SetActive(true);
        img.SetActive(true);
        BackText.GetComponent<TMP_Text>().color = Color.white;
    }

    public void QuitPointDown()
    {
        ExitText.GetComponent<TMP_Text>().color = Color.white;
    }

    public void QuitPointUp()
    {
        ExitText.GetComponent<TMP_Text>().color = basic;
    }

    public void LearnPointDown()
    {
        LearnText.GetComponent<TMP_Text>().color = Color.white;
    }

    public void LearnPointUp()
    {
        LearnText.GetComponent<TMP_Text>().color = basic;
    }

    public void ContactPointDown()
    {
        ContactText.GetComponent<TMP_Text>().color = Color.white;
    }

    public void ContactPointUp()
    {
        ContactText.GetComponent<TMP_Text>().color = basic;
    }

    public void BackPointDown()
    {
        BackText.GetComponent<TMP_Text>().color = Color.white;
    }

    public void BackPointUp()
    {
        BackText.GetComponent<TMP_Text>().color = basic;
    }
}
