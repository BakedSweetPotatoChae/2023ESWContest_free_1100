/**
 * Ardity (Serial Communication for Arduino + Unity)
 * Author: Daniel Wilches <dwilches@gmail.com>
 *
 * This work is released under the Creative Commons Attributions license.
 * https://creativecommons.org/licenses/by/2.0/
 */

using UnityEngine;
using System.Collections;

/**
 * Sample for reading using polling by yourself, and writing too.
 */
public class SampleUserPolling_ReadWrite : MonoBehaviour
{
    public SerialController serialController;

    // Initialization
    void Start()
    {
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();

        Debug.Log("Press A or Z to execute some actions");
    }
    public void Box1()
    {
        //Debug.Log("box2");
       serialController.SendSerialMessage("A"); 
    }
    public void Box2()
    {
       // Debug.Log("box1");
        serialController.SendSerialMessage("B");
    }
     public void Box3()
    {
        Debug.Log("box3");
        serialController.SendSerialMessage("H");

    }
    public void up()
    {
        Debug.Log("up");
        serialController.SendSerialMessage("C");
    }
    public void down()
    {
        Debug.Log("down");
        serialController.SendSerialMessage("D");
    }
    public void left()
    {
       // Debug.Log("left");
        serialController.SendSerialMessage("E");
    }
    public void right()
    {
        //Debug.Log("right");
        serialController.SendSerialMessage("F");
    }
     public void emergency()
    {
        //Debug.Log("emergency");
        serialController.SendSerialMessage("G");
    }

    
    // Executed each frame
     public void Update()
    {
        //---------------------------------------------------------------------
        // Send data
        //---------------------------------------------------------------------

        // If you press one of these keys send it to the serial device. A
        // sample serial device that accepts this input is given in the README.
        /*
         if (Input.GetKeyDown(KeyCode.A))
        {
           // Debug.Log("Sending A");
            serialController.SendSerialMessage("A");
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            //Debug.Log("Sending Z");
            serialController.SendSerialMessage("Z");
        }
       
      */
     
       
        


        //---------------------------------------------------------------------
        // Receive data
        //---------------------------------------------------------------------

        string message = serialController.ReadSerialMessage();

        if (message == null)
            return;

        // Check if the message is plain data or a connect/disconnect event.
        if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_CONNECTED))
            Debug.Log("Connection established");
        else if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_DISCONNECTED))
            Debug.Log("Connection attempt failed or disconnection detected");
        else
            Debug.Log("Message arrived: " + message);
    }
}
