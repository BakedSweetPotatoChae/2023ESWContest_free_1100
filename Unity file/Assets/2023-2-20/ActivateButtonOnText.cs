using UnityEngine;
using TMPro;

public class ActivateButtonOnText : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public GameObject buttonObject1;
    public GameObject buttonObject2;
    public GameObject buttonObject3;
    public GameObject loading;
    // 게임 오브젝트 변수들

    public string targetText;

    private void Update()
    {
        if (textMeshPro.text.Contains(targetText)) // textMeshPro에 targetText가 포함되어 있을 때
        {
            buttonObject1.SetActive(true);
            buttonObject2.SetActive(true);
            buttonObject3.SetActive(false);
            loading.SetActive(false);
            // 버튼과 로딩 오프젝트 활성화 여부
            
        }
        else // textMeshPro에 targetText가 포함되어 있지 않을 때
        {
            buttonObject1.SetActive(false);
            buttonObject2.SetActive(false);
            buttonObject3.SetActive(true);
            loading.SetActive(true);
            // 버튼과 로딩 오프젝트 활성화 여부
        }
    }
}