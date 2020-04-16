using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity;
    public Transform playerBody;
    public Transform cameraRotationPoint;
    public bool uiOpen = false;

    //privates
    float xRotation = 0f;
    float freecamRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (!uiOpen)
        {
            float mouseX = Input.GetAxis("MouseX") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("MouseY") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            //xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            freecamRotation -= mouseX;

            if (!Input.GetButton("Fire3"))
            {
                playerBody.Rotate(Vector3.up * mouseX);
                //cameraRotationPoint.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                freecamRotation = playerBody.transform.localRotation.y;
            }
            else
            {
                cameraRotationPoint.localRotation = Quaternion.Euler(xRotation, freecamRotation, 0f);
            }
        }
    }
}