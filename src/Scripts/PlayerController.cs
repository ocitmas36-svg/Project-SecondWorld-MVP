using UnityEngine;
using Mirror; // Pastikan sudah install Mirror di Unity

[RequireComponent(typeof(CharacterController))]
public class PlayerController : NetworkBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float gravity = -9.81f;
    public float mouseSensitivity = 2f;

    private CharacterController controller;
    private Vector3 velocity;
    private Camera playerCamera;
    private float xRotation = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();

        // Hanya aktifkan kontrol & kamera jika ini adalah karakter MILIK KITA
        if (isLocalPlayer)
        {
            Cursor.lockState = CursorLockMode.Locked;
            playerCamera.enabled = true;
        }
        else
        {
            playerCamera.enabled = false;
            // Matikan audio listener pada pemain lain agar tidak bentrok
            if(playerCamera.GetComponent<AudioListener>()) 
                playerCamera.GetComponent<AudioListener>().enabled = false;
        }
    }

    void Update()
    {
        // Jika bukan karakter kita (karakter orang lain), stop eksekusi script ini
        if (!isLocalPlayer) return;

        HandleRotation();
        HandleMovement();
    }

    void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        controller.Move(move * speed * Time.deltaTime);

        // Gravitasi sederhana
        if (controller.isGrounded && velocity.y < 0) velocity.y = -2f;
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
