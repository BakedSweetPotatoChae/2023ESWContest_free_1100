using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Camerabutton1 : MonoBehaviour
{
    public MainCamera my_camera; //Camera 코드 이름임주의
    public int target_index = 0;
    public GameObject panel;
    public GameObject click;
    public GameObject click1;

    public float delay = 3f;

    private void OnMouseDown()
    {
        my_camera.MoveCamera(target_index); 
        Invoke("ActiveSqidPanel", 0.1f); 
        Invoke("ActiveBoxPanel", 3f); 
    }

    void ActiveBoxPanel()
    {
        click.gameObject.SetActive(true);
    }

    void ActiveSqidPanel()
    {
        click1.gameObject.SetActive(false);
    }
    
    void OnMouseEnter(){
        panel.SetActive(true);
    }

    void OnMouseExit(){
        panel.SetActive(false);
    }
}
