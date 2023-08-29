using UnityEngine;
using TMPro;


public class Otp : MonoBehaviour
{
    public TMP_InputField usernameInputField;
    public TMP_InputField passwordInputField;
    public GameObject logincanvas;
    public GameObject maincanvas;
    public GameObject loginEventSystem;
    
    
    
    void Start()
        {
            passwordInputField.contentType = TMP_InputField.ContentType.Password;
            
        }

    public void OnLoginButtonClick()
    {
        if (usernameInputField.text == "k" && passwordInputField.text == "1028")
        {
            
            maincanvas.SetActive(true);
            loginEventSystem.SetActive(false);
        }
        else
        {
            Debug.Log("로그인 실패");
        }
    }
}
