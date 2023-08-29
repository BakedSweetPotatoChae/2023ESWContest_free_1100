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
        LoadCSV();
    }

    private void Update()
    {
        SendRecentData();
    }

    private void LoadCSV()
    {
        if (!File.Exists(csvFilePath))
        {
            Debug.LogError("CSV file does not exist: " + csvFilePath);
            return;
        }

        using (var reader = new StreamReader(csvFilePath))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                csvData.Add(values);
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

        var recentData = csvData[targetRow].LastOrDefault();
        if (recentData == null)
        {
            Debug.LogError("Target row does not have any data.");
            return;
        }

        panel.text = recentData;
    }
}