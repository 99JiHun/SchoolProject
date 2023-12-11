using System.Collections;
using UnityEngine;

public class CharactorMove : MonoBehaviour
{
    public Transform cameraTransform;
    public CharacterController characterController;
    public float moveSpeed = 10f;
    public float jumpSpeed = 10f;
    public float gravity = -20f;
    public float mouseSensitivity = 2f; // Adjust this value to control mouse sensitivity

    private float rotationX = 0f;

    void Update()
    {
        // Character movement
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(h, 0, v);
        moveDirection = cameraTransform.TransformDirection(moveDirection);
        moveDirection *= moveSpeed;

        // Jumping
        if (characterController.isGrounded)
        {
            yVelocity = 0;

            if (Input.GetButtonDown("Jump"))
            {
                yVelocity = jumpSpeed;
            }
        }

        // Applying gravity
        yVelocity += (gravity * Time.deltaTime);

        moveDirection.y = yVelocity;

        // Moving the character
        characterController.Move(moveDirection * Time.deltaTime);

        // Camera rotation based on mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        // Rotate the camera vertically
        cameraTransform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);

        // Rotate the character horizontally
        transform.Rotate(Vector3.up * mouseX);
    }
    
    private float yVelocity = 0f;
}