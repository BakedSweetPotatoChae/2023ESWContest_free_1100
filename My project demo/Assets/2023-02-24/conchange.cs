using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class conchange : MonoBehaviour
{
    public void SceneChange()
    {
        SceneManager.LoadScene("control");
    }
}
