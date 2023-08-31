using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 3f; // 카메라 이동 속도
    public float rotateSpeed = 2f; // 카메라 회전 속도
    public float zoomSpeed = 2f; // 카메라 줌 속도
    //깨짐 현상은 clipping planes 에서 near값 최대한 작게ㅔ주기

    void Update()
    {
        // WASD 키 입력으로 카메라 이동
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float moveZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Translate(moveX, 0f, moveZ);

        // 마우스 입력으로 카메라 회전
        if (Input.GetMouseButton(1))
        {
            float rotateX = Input.GetAxis("Mouse X") * rotateSpeed;
            float rotateY = Input.GetAxis("Mouse Y") * rotateSpeed;
            transform.Rotate(Vector3.up, rotateX, Space.World);
            transform.Rotate(Vector3.left, rotateY, Space.Self);
        }

        // 마우스 휠 입력으로 카메라 줌
        float zoom = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        transform.Translate(Vector3.forward * zoom, Space.Self);
    }
}
