using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Camera[] subCameras;
    public void MoveCamera(int index)
    {
        Vector3 new_position = subCameras[index].transform.position;
        Vector3 new_rotation = subCameras[index].transform.eulerAngles;
        //transform.position = new_position;
        //transform.eulerAngles = new_rotation;
         iTween.MoveTo(this.gameObject, iTween.Hash("position",
            new_position, "easetype",
            iTween.EaseType.easeOutSine, "time", 3.0f));

        iTween.RotateTo(this.gameObject, iTween.Hash("rotation",
            new_rotation, "easetype",
            iTween.EaseType.easeOutSine, "time", 3.0f));//iTween 애셋스토어에서 다운받기
       
            

    }
    // Start is called before the first frame update
    private void Start()
    {
        MoveCamera(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
