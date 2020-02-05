using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    public Transform target;

    public Vector3 offset;
    public float pitch = 2f;
    float zoomSpeed = 5f;
    [SerializeField] private float minZoom = 3f;
    [SerializeField] private float maxZoom = 20f;
    public float currentZoom = 10f;

    public float currentYaw = 0f;
    public float yawSpeed = 1;


    Camera cam;
    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (target == null)
            if (GameObject.FindGameObjectWithTag("Player"))
                target = GameObject.FindGameObjectWithTag("Player").transform;

        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        // if a ui element is opened, the mouse pointer is visible and can be used to interact with things on screen
        if (target != null)
        {
            if (target.GetComponent<Controller_Input>().uiIsOpen == false)
            {
                Cursor.lockState = CursorLockMode.Locked;
                RotateCamera();
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position - offset * currentZoom;
            transform.LookAt(target.position + Vector3.up * pitch);
            transform.RotateAround(target.position, Vector3.up, currentYaw);
        }
    }

    void RotateCamera()
    {
        if (Input.GetAxis("Mouse X") >= 0.1f || Input.GetAxis("Mouse X") <= -0.1f)
        {
            currentYaw += yawSpeed * Input.GetAxis("Mouse X");
        }
    }
}
