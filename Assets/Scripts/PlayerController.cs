using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;

    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float gravity = -9.81f;
    [SerializeField]
    private float jumpHeight = 1.5f;

    [SerializeField]
    private LayerMask groundMask;
    [SerializeField]
    private float groundCheckDistance = 0.2f;

    private Vector3 velocity;
    private bool isGrounded;

    [SerializeField]
    private float mouseSensitivity = 100f;
    [SerializeField]
    private float xRot = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {

        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundMask);

        Playermovement();

        ApplyGravity();

        CameraMovement();
    }

    //for Player movements 
    private void Playermovement()
    {
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }


        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);


        Debug.Log(isGrounded);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    //For moveing the camers 
    private void CameraMovement()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;


        transform.Rotate(Vector3.up * mouseX);
        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRot, transform.localRotation.eulerAngles.y, 0f);
    }

    //For gravity 
    private void ApplyGravity()
    {
        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
