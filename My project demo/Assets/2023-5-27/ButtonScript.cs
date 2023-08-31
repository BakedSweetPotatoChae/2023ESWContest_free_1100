using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public SerialController serialController;
    void Start()
    {
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>(); // 아두이노에서 유니티로 보내기
    }
      public void BoX3()
    {
        Debug.Log("box3");
        serialController.SendSerialMessage("A");

    }
}