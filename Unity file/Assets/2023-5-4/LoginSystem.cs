using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Michsky.MUIP;

public class LoginSystem : MonoBehaviour
{   
    [SerializeField] private NotificationManager myNotification;
    public TMP_InputField usernameField;
    public TMP_InputField passwordField;
    

    private string correctUsername = "intel";
    private string correctPassword = "****";

    
    public void OnLoginButtonClicked()
    {
        string username = usernameField.text;
        string password = passwordField.text;

        if (username == correctUsername && password == correctPassword)
        {
            // 로그인 성공 - 다음 scene으로 전환
            SceneManager.LoadScene("SampleScene");
        }
        else
        {
            // 로그인 실패 - 에러 텍스트 출력
            myNotification.Open();

        }
    }


    public void OnPasswordFieldChanged()
    {
        // 비밀번호 입력 시 *로 표시
        passwordField.text = new string('*', passwordField.text.Length);
    }
}
