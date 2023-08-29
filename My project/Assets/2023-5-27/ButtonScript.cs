using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public SerialController serialController;
    void Start()
    {
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
    }
      public void BoX3()
    {
        Debug.Log("box3");
        serialController.SendSerialMessage("A");

    }
}