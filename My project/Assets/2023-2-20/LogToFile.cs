using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;

public class LogToFile : MonoBehaviour
{
    public string logFilePath = "log.csv";
    public int maxRows = 20;
    

    private List<string> logRows = new List<string>();

    private void Start()
    {
        // 로그 파일 초기화
        if (File.Exists(logFilePath)) {
            File.Delete(logFilePath);
        }
        File.WriteAllText(logFilePath, "Time,Message\n");
    }

    private void OnEnable()
    {
        Application.logMessageReceived += LogToCSV;
    }

    private void OnDisable()
    {
        Application.logMessageReceived -= LogToCSV;
    }

    private void LogToCSV(string logString, string stackTrace, LogType type)
    {
        // 시간과 로그 문자열을 CSV 파일에 추가
        string time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        string message = logString.Replace(',', ';'); // CSV 파일에서 쉼표를 구분자로 사용하므로 쉼표를 세미콜론으로 대체
        string row = $"{time},{message} connect \n";
        logRows.Add(row);
        File.AppendAllText(logFilePath, row);

        // CSV 파일의 행 수가 maxRows를 넘어가면 가장 오래된 데이터를 삭제
        if (logRows.Count > maxRows)
        {
            string[] lines = File.ReadAllLines(logFilePath);
            File.WriteAllText(logFilePath, "Time,Message\n");
            for (int i = 1; i < lines.Length; i++) // 첫 줄인 "Time,Message"는 삭제하지 않음
            {
                if (i < lines.Length - maxRows + 1) // 오래된 데이터는 삭제
                {
                    continue;
                }
                File.AppendAllText(logFilePath, lines[i] + "\n");
            }
            logRows.RemoveRange(0, logRows.Count - maxRows); // 가장 오래된 데이터 삭제
        }
    }
}
