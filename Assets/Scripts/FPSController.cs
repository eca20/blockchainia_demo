using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;


[RequireComponent(typeof(CharacterController))]
public class FPSController : NetworkBehaviour
{
    [Header("Camera")]
    public Transform playerRoot;
    public Transform playerCam;

    public float cameraSensitivity;

    float rotX;
    float rotY;

    [Header("Movement")]
    CharacterController controller;
    public float speed;
    public float jumpHeight;
    public Transform feet;
    public bool isGrounded;
    public float gravity;
    Vector3 velocity;

    [Header("Input")]
    public InputAction move;
    public InputAction jump;
    public InputAction mouseX;
    public InputAction mouseY;


    void OnEnable() {
        move.Enable();
        jump.Enable();
        mouseX.Enable();
        mouseY.Enable();
    }

    void OnDisable() {
        move.Disable();
        jump.Disable();
        mouseX.Disable();
        mouseY.Disable();
    }

    void Start() {
        if(!isLocalPlayer){
            playerCam.GetComponent<Camera>().enabled = false;
            return;
        }
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
    }

    void Update() {
        if(!isLocalPlayer){
            return;
        }

        controller.Move(velocity * Time.deltaTime);

        Vector2 mouseInput = new Vector2(mouseX.ReadValue<float>() * cameraSensitivity, mouseY.ReadValue<float>() * cameraSensitivity);
        rotX -= mouseInput.y;
        rotX = Mathf.Clamp(rotX, -90, 90);
        rotY += mouseInput.x;

        playerRoot.rotation = Quaternion.Euler(0f, rotY, 0f);
        playerCam.localRotation = Quaternion.Euler(rotX, 0f, 0f);

        //PlayerMovement

        Vector2 moveInput = move.ReadValue<Vector2>();

        Vector3 moveVelocity = playerRoot.forward * moveInput.y + playerRoot.right * moveInput.x;

        controller.Move(moveVelocity * speed * Time.deltaTime);

        isGrounded = Physics.Raycast(feet.position, feet.TransformDirection(Vector3.down), 1.0f);

        //Jump
        if(isGrounded == true) {
            velocity = new Vector3(0f, -3f, 0f);
        } else {
             velocity -= Vector3.up * gravity * Time.deltaTime;

        }
        jump.performed += ctx => Jump();
    }

    void Jump(){
        if(isGrounded == true){
            velocity.y = Mathf.Sqrt(2f * jumpHeight * gravity);
        }
    }

}
