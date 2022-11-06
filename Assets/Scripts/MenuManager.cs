using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject controlsUI;
    public GameObject generalUI;


    public void Play()
    {
        SceneManager.LoadScene("Prueba");
    }

    public void HowToPlay()
    {
        controlsUI.SetActive(true);
        generalUI.SetActive(false);
    }

    public void Atras()
    {
        controlsUI.SetActive(false);
        generalUI.SetActive(true);
    }


    public void Exit()
    {
        Application.Quit();
    }
}
