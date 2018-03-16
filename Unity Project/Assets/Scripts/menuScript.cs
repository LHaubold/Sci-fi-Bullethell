using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class menuScript : MonoBehaviour
{
    bool controlsActive = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Stop();
        }
    }

    public void startLvl()
    {
        SceneManager.LoadScene("main");
    }

    public void SwitchPanels()
    {
        if (controlsActive)
        {
            transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
            controlsActive = false;
        }
        else
        {
            transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
            controlsActive = true;
        }
    }

    public void Stop()
    {
        Application.Quit();
    }
}
