using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManger : MonoBehaviour
{
    public Text exeText;
    public int ex = 0;
    public int exMax = 5;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        exeText.text = ex.ToString() + "/" + exMax.ToString();

    }
}
