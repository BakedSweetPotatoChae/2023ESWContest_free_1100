using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSave : MonoBehaviour
{
    private int Scene;

    public void LoadMainMenu()
    {
        Scene = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("SavedScene", Scene);
        SceneManager.LoadScene(3);
    }
}
