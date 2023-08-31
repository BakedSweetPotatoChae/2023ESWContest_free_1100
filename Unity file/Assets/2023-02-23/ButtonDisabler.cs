using UnityEngine;
using UnityEngine.UI;
public class ButtonDisabler : MonoBehaviour {

    public GameObject myButton;
    public GameObject myButton2;


    void OnEnable() {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable() {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type) {
        if (logString.Contains("tag1")) {
            myButton.SetActive(false);
            myButton2.SetActive(false);
        }
        else {
            myButton.SetActive(true);
            myButton2.SetActive(true);
    }
}
}






