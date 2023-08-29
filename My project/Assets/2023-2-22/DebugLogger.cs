using UnityEngine;
using TMPro;

public class DebugLogger : MonoBehaviour
{
    // TextMeshPro 객체
    public TextMeshProUGUI textMeshPro;

    // 최신 로그 메시지
    private string latestLogMessage;

    void Awake()
    {
        // 디버그 로그 출력 함수를 선언
        Application.logMessageReceived += HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        // 로그가 tag1을 포함하고 있을 때 TextMeshPro에 출력
        if (logString.Contains("tag1"))
        {
            // 최신 로그 메시지 업데이트
            latestLogMessage = logString;

            // TextMeshPro에 출력
            textMeshPro.text += logString + "\n";
        }
    }

    void LateUpdate()
    {
        // 최신 로그 메시지만 출력
        if (!string.IsNullOrEmpty(latestLogMessage))
        {
            textMeshPro.text = latestLogMessage + "\n";
            latestLogMessage = null;
        }
    }
}
