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
        SceneManager.LoadScene("SampleScene"); // 화면 전환
    }

    // 마우스 클릭시 판넬 활성화
    void OnMouseEnter(){
        panel.SetActive(true);
    }

    void OnMouseExit(){
        panel.SetActive(false);
    }
}

