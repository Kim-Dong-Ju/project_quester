using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    public GameObject mainPanel, ContactPanel;
    private bool EscapeClick = false;
    // Start is called before the first frame update
    void Start()
    {
        ContactPanel.SetActive(false);
        EscapeClick = false;
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
        Application.Quit();
    }

    public void Learn()
    {
        SceneManager.LoadScene("Learn");
    }

    public void Contact()
    {
        mainPanel.SetActive(false);
        ContactPanel.SetActive(true);
    }

    public void BacktoMain()
    {
        ContactPanel.SetActive(false);
        mainPanel.SetActive(true);
    }
}
