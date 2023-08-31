using UnityEngine;

public static class ButtonStateManager
{
    private static bool isButtonActive = false;
    private static GameObject buttonObject;

    public static void SetButtonObject(GameObject button)
    {
        buttonObject = button;
        isButtonActive = button.activeSelf;
    }

    public static void ActivateButton()
    {
        if (buttonObject != null)
        {
            buttonObject.SetActive(true);
            isButtonActive = true;
        }
    }

    public static void DeactivateButton()
    {
        if (buttonObject != null)
        {
            buttonObject.SetActive(false);
            isButtonActive = false;
        }
    }

    public static bool IsButtonActive()
    {
        return isButtonActive;
    }
}