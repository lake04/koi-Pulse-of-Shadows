using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class ui : MonoBehaviour
{
    public FadeEffect fadeEffect;
    public GameObject pop;

    private void Start()
    {
        popDown();
    }
        
    public void play()
    {
        SceneManager.LoadScene(1);
    }

    public void quit()
    {
        Application.Quit();
        Debug.Log("quit");
    }

    public void popUP()
    {
        pop.SetActive(true);
    }

    public void popDown()
    {
        pop.SetActive(false);
    }

}
