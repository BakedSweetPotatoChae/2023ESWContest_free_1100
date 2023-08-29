using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneActivator : MonoBehaviour
{
    public string scene1Name; // 첫 번째 씬의 이름
    public string scene2Name; // 두 번째 씬의 이름

    void Start()
    {
        // 첫 번째 씬 로드
        SceneManager.LoadScene(scene1Name, LoadSceneMode.Additive);

        // 두 번째 씬 로드
        SceneManager.LoadScene(scene2Name, LoadSceneMode.Additive);
    }
}