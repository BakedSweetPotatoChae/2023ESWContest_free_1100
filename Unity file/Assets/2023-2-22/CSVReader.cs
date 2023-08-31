using System.IO;
using UnityEngine;
using TMPro;

public class CSVReader : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public string csvFilePath = @"C:\Users\raino\My project\log.csv";
    public int bRowIndex;

    private string latestData;

    private FileSystemWatcher fileSystemWatcher;
    private bool isUpdating = false;

    private void Start()
    {
        fileSystemWatcher = new FileSystemWatcher(Path.GetDirectoryName(csvFilePath));
        fileSystemWatcher.Filter = Path.GetFileName(csvFilePath);
        fileSystemWatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size;
        fileSystemWatcher.Changed += OnFileChanged;
        fileSystemWatcher.EnableRaisingEvents = true;

        UpdateLatestData();
    }

    private void UpdateLatestData()
    {
        if (!isUpdating)
        {
            isUpdating = true;

            StreamReader reader = new StreamReader(csvFilePath);
            string line = "";
            int i = 0;

            while ((line = reader.ReadLine()) != null)
            {
                if (i == bRowIndex)
                {
                    latestData = line;
                    break;
                }

                i++;
            }

            reader.Close();

            textMeshPro.text = latestData;

            isUpdating = false;
        }
    }

    private void OnFileChanged(object sender, FileSystemEventArgs e)
    {
        UpdateLatestData();
    }

    private void OnApplicationQuit()
    {
        fileSystemWatcher.EnableRaisingEvents = false;
        fileSystemWatcher.Changed -= OnFileChanged;
        fileSystemWatcher.Dispose();
    }
}