using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Michsky.MUIP;
using System.Diagnostics;





public class boxtwin : MonoBehaviour
{
    [SerializeField] private ButtonManager mybutton;
    [SerializeField] private ButtonManager mybutton1;
    

    
    

    
     void Awake()
    {
        // 디버그 로그 출력 함수를 선언
        Application.logMessageReceived += HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
      {
          
          if (logString.Contains("tag1"))
          {
              mybutton.useRipple = true;
          }

          

          
      }
}