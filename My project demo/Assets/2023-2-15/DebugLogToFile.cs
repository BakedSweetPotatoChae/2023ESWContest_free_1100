using System.IO;
using UnityEngine;
using System.Linq;

public class DebugLogToFile : MonoBehaviour
{
    private const string logFilePath = "log.txt";
    private const int maxLogCount = 20;

    private void Awake()
    {
        // 기존 로그 파일 삭제
        if (File.Exists(logFilePath))
        {
            File.Delete(logFilePath);
        }

        // 로그 파일 생성
        using (var fileStream = File.Create(logFilePath))
        {
            // 파일 스트림 닫기
            fileStream.Close();
        }
    }

    private void OnEnable()
    {
        // 로그 메시지 이벤트 등록
        Application.logMessageReceived += LogToFile;
    }

    private void OnDisable()
    {
        // 로그 메시지 이벤트 해제
        Application.logMessageReceived -= LogToFile;
    }

    private void LogToFile(string logString, string stackTrace, LogType type)
    {
        // 로그 파일에 기록할 문자열
        string log = $"{System.DateTime.Now.ToString()} - [{type.ToString()}] {logString}\n";

        // 로그 파일에 문자열 추가
        File.AppendAllText(logFilePath, log);

        // 로그 파일의 라인 수 가져오기
        int logCount = File.ReadAllLines(logFilePath).Length;

        // 로그 파일의 라인 수가 최대 개수를 초과하면 가장 오래된 로그 삭제
        if (logCount > maxLogCount)
    {
        var lines = File.ReadAllLines(logFilePath);
        File.WriteAllLines(logFilePath, lines.Skip(1).ToArray());
    }
    }
}