using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUi : MonoBehaviour
{
    public GameObject pop;

    private void Start()
    {
        pop.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            popUP();
        }
    }

    public void popUP()
    {
        pop.SetActive(true);
         Time.timeScale = 0f;
    }

    public void title()
    {
        SceneManager.LoadScene(0);
    }

    public void popDown()
    {
        pop.SetActive(false);
    }
}
