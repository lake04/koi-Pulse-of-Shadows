using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnenmyRoating : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3(0f, 0f, 20f) * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           gameObject.SetActive(false);
            
        }
    }
}
