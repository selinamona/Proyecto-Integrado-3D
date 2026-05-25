using UnityEngine;

public class CameraLook : MonoBehaviour
{

    public float mouseSensitivity = 80f;

    public Transform playerBody;

    float xRotation = 0;


    void Start()
    {
        
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse x") * mouseSensitivity * Time.deltaTime; ;
        float mouseY = Input.GetAxis("Mouse y") * mouseSensitivity * Time.deltaTime; ;

        xRotation += mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        playerBody.Rotate(Vector3.up * mouseX);
        
    }
}
