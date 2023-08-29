using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maintext : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.moveY(gameObject,1000,1.5f).setEaseOutQuad().setLoopPingPong();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
