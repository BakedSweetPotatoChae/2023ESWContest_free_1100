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
 * When creating your message listeners you need to implement these two methods:
 *  - OnMessageArrived
 *  - OnConnectionEvent
 */
public class SampleMessageListener : MonoBehaviour
{
    /// Invoked when a line of data is received from the serial device.
    public float speed = 10.2f;
    public GameObject target, target1, target2;
    public float rotationSpeed = 10f;

    // Initialization
   
    void OnMessageArrived(string msg)
    {   target = GameObject.Find("AGV");
        target1 = GameObject.Find("AGV");
        target2 = GameObject.Find("AGV");
        float movement = speed * Time.deltaTime;
        
        Debug.Log("Message arrived: " + msg);
        while(msg == "1"){
            target.transform.Translate(movement, 0, 0);
            //Vector3 a = transform.position;
            ///Vector3 b = targetmove.position;
            //transform.position = Vector3.MoveTowards(a,Vector3.Lerp(a,b,t), speed2);
        }
        
        if(msg == "2"){
            target1.transform.Translate(0, 0, -movement); 
        }
        
        if(msg == "3"){
            target2.transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime); 
        }
    }
    void OnConnectionEvent(bool success)
    {
        if (success)
            Debug.Log("Connection established");
        else
            Debug.Log("Connection attempt failed or disconnection detected");
    }
}
