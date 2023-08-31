using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;

public class LoadRecentText1 : MonoBehaviour
{
    public TextMeshProUGUI panel;
    public string csvFilePath = "data.csv";
    public int targetRow = 20;

    private List<string[]> csvData = new List<string[]>();

    private void Start()
    {
        LoadCSV(); // CSV 파일을 로드하는 함수 호출
    }

    private void Update()
    {
        SendRecentData(); // 최근 데이터를 표시하는 함수 호출
    }

    private void LoadCSV()
    {
        if (!File.Exists(csvFilePath))
        {
            Debug.LogError("CSV file does not exist: " + csvFilePath);
            return;
        } // 주어진 csvFilePath 경로의 파일이 존재하는지 확인

        using (var reader = new StreamReader(csvFilePath)) // StreamReader를 사용하여 CSV 파일 읽기 시작
        {
            while (!reader.EndOfStream) // 파일을 끝까지 반복해서 읽음
            {
                var line = reader.ReadLine();
                var values = line.Split(','); // ,를 기준으로 분리 저장
                csvData.Add(values); // 분리된 값을 csvData 리스트에 추가
            }
        }
    }

    private void SendRecentData()
    {
        if (targetRow >= csvData.Count)
        {
            Debug.LogError("Target row index is out of range.");
            return;
        } 

        var recentData = csvData[targetRow].LastOrDefault(); // csvData 리스트에서 targetRow에 해당하는 행의 마지막 데이터 가져오기
        if (recentData == null)
        {
            Debug.LogError("Target row does not have any data.");
            return;
        }

        panel.text = recentData;
    }
}