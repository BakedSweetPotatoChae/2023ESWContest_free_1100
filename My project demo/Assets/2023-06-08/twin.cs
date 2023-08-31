using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class twin : MonoBehaviour
{

    void Update()
    {
        // w ->앞
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0.0f, 0.0f, 1.0f);
        }
        // s->뒤
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= new Vector3(0.0f, 0.0f, 1.0f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= new Vector3(1.0f, 0.0f, 0.0f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(1.0f, 0.0f, 0.0f);
        }
        
        
    }
}


