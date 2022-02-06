using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class FirstPersonController : MonoBehaviour
{
    public Transform cameraAxis;
    CharacterController cc;
    private float walkSpeed = 9.0f;
    private float sprintSpeed = 18.0f;
    private float crouchSpeed = 4.5f;
    private float jumpHeight = 8.0f;
    Vector3 moveDirection = Vector3.zero;
    private float mouseSpeed = 100f;
    public bool flyMode;
    public bool verticalFlip = true, horizontalFlip = false; 
    private void Start()
    {
        flyMode = false;
        Cursor.lockState = CursorLockMode.Locked;
        cc = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        float mouseX = Input.GetAxis("Mouse X") * mouseSpeed * Time.fixedDeltaTime * ( horizontalFlip ? -1 : 1 );
        float mouseY = Input.GetAxis("Mouse Y") * mouseSpeed * Time.fixedDeltaTime * ( verticalFlip ? -1 : 1);

        var rotationLR = transform.localEulerAngles;
        rotationLR.y += mouseX;
        rotationLR.x -= mouseY;
        transform.rotation = Quaternion.AngleAxis(rotationLR.y, Vector3.up);

        var cameraRot = cameraAxis.localEulerAngles;
        cameraRot.x += mouseY;
        cameraAxis.localRotation = Quaternion.AngleAxis(cameraRot.x, Vector3.right);


        float speed = walkSpeed;

        if (Input.GetKeyDown(KeyCode.G))
        {
            flyMode = !flyMode;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprintSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            speed = crouchSpeed;
        }
        else
        {
            speed = walkSpeed;
        }


        if (!flyMode)
        {
            if (cc.isGrounded)
            {
                moveDirection = (transform.right * horizontal + transform.forward * vertical);
                moveDirection *= speed;

                if (Input.GetButtonDown("Jump"))
                {
                    moveDirection.y = jumpHeight;
                }
            }
            moveDirection.y -= 20.0f * Time.fixedDeltaTime;
        }
        else
        {
            moveDirection = (transform.right * horizontal + transform.forward * vertical);
            moveDirection *= speed;
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpHeight;
            }
        }
        cc.Move(moveDirection * Time.fixedDeltaTime );
    }
}