using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using System;
using UnityEditor;
using UnityEngine.UI;

public class LoadRecentText : MonoBehaviour
{
    public Text textComponent;
    public string filePath;

    private void Start()
    {
        // 파일 경로가 지정되어 있지 않으면 오류 메시지 출력 후 함수 종료
        if (string.IsNullOrEmpty(filePath))
        {
            Debug.LogError("파일 경로가 지정되어 있지 않습니다.");
            return;
        }

        // 파일에서 문자열 읽어오기
        string text = File.ReadAllText(filePath);

        // Text 컴포넌트에 문자열 표시
        textComponent.text = text;
    }

}

