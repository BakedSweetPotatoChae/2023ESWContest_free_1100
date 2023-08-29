using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class controltomainbutton : MonoBehaviour
{
    
    public GameObject panel;

     private void OnMouseDown()
    {
        SceneManager.LoadScene("SampleScene");
    }


    void OnMouseEnter(){
        panel.SetActive(true);
    }

    void OnMouseExit(){
        panel.SetActive(false);
    }
}

