using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Michsky.MUIP;
using System.Diagnostics;

public class Servonotification : MonoBehaviour
{
    [SerializeField] private NotificationManager onNotification;
    [SerializeField] private NotificationManager offNotification;

    
     void Awake()
    {
        // 디버그 로그 출력 함수를 선언
        Application.logMessageReceived += HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
      {
          
          if (logString.Contains("A"))
          {
              onNotification.Open();  
          }

          if (logString.Contains("O"))
          {
              offNotification.Open();            
          }

          
      }
}