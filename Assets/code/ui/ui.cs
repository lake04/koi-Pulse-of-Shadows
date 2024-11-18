using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ui : MonoBehaviour
{

    public void play()
    {
        SceneManager.LoadScene(0);
    }

    public void quit()
    {
        Application.Quit();
        Debug.Log("quit");
    }
}
