using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private readonly float scoreScale = 20;

    public float CurremtScore { private set; get; } = 0;

    // Update is called once per frame
    void Update()
    {
        CurremtScore += Time.deltaTime * scoreScale;
    }
}
