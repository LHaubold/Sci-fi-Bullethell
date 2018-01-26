using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scenemanager : MonoBehaviour
{

    GameObject UI_Panel;
    GameObject Death_Panel;

	void Start ()
    {
        UI_Panel = GameObject.Find("Canvas").transform.GetChild(0).transform.GetChild(0).gameObject;
        Death_Panel = GameObject.Find("Canvas").transform.GetChild(1).gameObject;
	}
	

	void Update ()
    {
        UI_Management();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }

        if (staticValues.lives < 0)
        {
            Death_Panel.SetActive(true);
            Death_Panel.transform.GetChild(1).GetComponent<Text>().text = staticValues.Score.ToString();
        }
    }

    public void Quit()
    {
        SceneManager.LoadScene("start");
    }

    void UI_Management()
    {
        //Hide UI
        if (Input.GetKeyDown("c"))
        {
            if (UI_Panel.activeSelf == true)
            {
                UI_Panel.SetActive(false);
            }
            else
            {
                UI_Panel.SetActive(true);
            }
        }

        //manage pickup display
        switch (staticValues.Pickup)
        {
            case 0:
                UI_Panel.transform.GetChild(6).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/pickup orb");
                break;
            case 1:
                UI_Panel.transform.GetChild(6).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/pickup orb_1");
                break;
        }

        //manage remaining lifecounter
        switch(staticValues.lives)
        {
            case 3:
                UI_Panel.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                UI_Panel.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
                UI_Panel.transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
                break;
            case 2:
                UI_Panel.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                UI_Panel.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
                UI_Panel.transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
                break;
            case 1:
                UI_Panel.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                UI_Panel.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
                UI_Panel.transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
                break;
            case 0:
                UI_Panel.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                UI_Panel.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
                UI_Panel.transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
                break;
        }

        //Lifebar
        UI_Panel.transform.GetChild(4).GetComponent<Image>().fillAmount = staticValues.HP / 100f;

        //Score
        UI_Panel.transform.GetChild(5).GetChild(0).GetComponent<Text>().text = staticValues.Score.ToString();
    }
}
