using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
   public void StartGame(){
      SceneManager.LoadScene(3); // 화면 전환
   }
   
}
