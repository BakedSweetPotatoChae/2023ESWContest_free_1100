using UnityEngine;
using TMPro;

public class ActivateButtonOnText : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public GameObject buttonObject1;
    public GameObject buttonObject2;
    public GameObject buttonObject3;
    public GameObject loading;
    

    public string targetText;

    private void Update()
    {
        if (textMeshPro.text.Contains(targetText))
        {
            buttonObject1.SetActive(true);
            buttonObject2.SetActive(true);
            buttonObject3.SetActive(false);
            loading.SetActive(false);
            
            
        }
        else
        {
            buttonObject1.SetActive(false);
            buttonObject2.SetActive(false);
            buttonObject3.SetActive(true);
            loading.SetActive(true);
            
        }
    }
}