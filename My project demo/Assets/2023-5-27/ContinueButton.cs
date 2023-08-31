using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueButton : MonoBehaviour
{
    private int sceneToContinue;

    public void ContinueGame(){
        sceneToContinue = PlayerPrefs.GetInt("SavedScene");

        if (sceneToContinue != 3)
            SceneManager.LoadScene(sceneToContinue); // 화면 전환
        else
            return;
    }



}

