using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;


[RequireComponent(typeof(CharacterController))]
public class EnvEnemy : MonoBehaviour
{
    [Header("Camera")]
    public Transform enemyRoot;
    public Transform enemyCam;

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
        controller = GetComponent<CharacterController>();
    }

    void Update() {
        controller.Move(velocity * Time.deltaTime);

        Vector2 mouseInput = new Vector2(mouseX.ReadValue<float>() * cameraSensitivity, mouseY.ReadValue<float>() * cameraSensitivity);
        rotX -= Random.Range(-1.0f, 1.0f);
        rotX = Mathf.Clamp(rotX, -90, 90);
        rotY += Random.Range(-1.0f, 1.0f);

        enemyRoot.rotation = Quaternion.Euler(0f, rotY, 0f);
        enemyCam.localRotation = Quaternion.Euler(rotX, 0f, 0f);

        //PlayerMovement
        Vector2 enemyInput = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;

       // Vector2 moveInput = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        Vector2 moveInput = new Vector2(0, 0).normalized;

        Vector3 moveVelocity = enemyRoot.forward * moveInput.y + enemyRoot.right * moveInput.x;

        if(isGrounded == false) {
             moveVelocity -= Vector3.up * gravity*2 * Time.deltaTime;
        }

        controller.Move(moveVelocity * speed * Time.deltaTime);

        isGrounded = Physics.Raycast(feet.position, feet.TransformDirection(Vector3.down), 0.2f);

        //Jump
      
    }
}