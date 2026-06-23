using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Vector3 CamOffset = new Vector3(0f, 1.2f, -2.6f);
    private Transform _target;

    // Tambahkan Ini (5.1) - Mouse look variables
    public float MouseSensitivity = 2f;
    public float MaxLookAngle = 80f;
    private float _xRotation = 0f;
    private float _yRotation = 0f;
    public bool InvertY = false;

    void Start()
    {
        _target = GameObject.Find("Player").transform;

        // Tambahkan Ini (5.1) - Lock cursor ke tengah layar
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        // Tambahkan Ini (5.1) - Baca input mouse
        float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity;

        // Update rotasi Y (horizontal) - untuk player
        _yRotation += mouseX;

        // Update rotasi X (vertikal) - untuk camera, dengan batasan
        if (InvertY)
            _xRotation += mouseY;
        else
            _xRotation -= mouseY;

        _xRotation = Mathf.Clamp(_xRotation, -MaxLookAngle, MaxLookAngle);

        // Rotate player secara horizontal (Y axis)
        if (_target != null)
        {
            _target.rotation = Quaternion.Euler(0f, _yRotation, 0f);
        }

        // Apply rotasi camera secara vertikal (X axis)
        transform.position = _target.TransformPoint(CamOffset);
        transform.LookAt(_target);

        // Tambahkan Ini (5.1) - Apply rotasi X ke camera
        transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0f);
    }

    // Tambahkan Ini (5.1) - Optional: Tekan ESC untuk unlock cursor
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}