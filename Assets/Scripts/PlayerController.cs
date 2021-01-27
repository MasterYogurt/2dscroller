using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D body; 

    bool movingLeft = false;
    bool movingRight = false;
    bool grounded = false;
    bool sprinting = false;

    public float moveSpeed;
    public float sprintSpeed;
    public float jumpPower;


    // Start is called before the first frame update
    void Start()
    {
        // Get the rigidbody component
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        // Not 100% sure if there might be slight floating point errors
        // so I am taking a small epsilon into consideration
        if (body.velocity.y <= 0.01f & body.velocity.y >= -0.01f) 
        {
            // We are on solid ground
            grounded = true;
        } else 
        {
            // We are currently in the air
            grounded = false;
        }

        // Only apply new velocity if we are on the ground
        if (grounded) Move();

    }

    // Set the states depending on the button pressed
    // Adjusted from when I didnt read to use the Input System :(

    public void JumpInput(InputAction.CallbackContext context){
        if(context.ReadValueAsButton() & grounded)
        {
            Jump();
        }
    }

    public void Sprint(InputAction.CallbackContext context){
        sprinting = context.ReadValueAsButton();
    }

    public void MoveRight(InputAction.CallbackContext context){
        movingRight = context.ReadValueAsButton();
    }

    public void MoveLeft(InputAction.CallbackContext context){
        movingLeft = context.ReadValueAsButton();
    }

    private void Jump()
    {
        if (!grounded) return;
        body.AddForce(new Vector2(0.0f, jumpPower));
    }

    private void Move(){
        // Check if exactly one button is pressed
        // if not we dont need to move
        if (!(movingLeft ^ movingRight)) {
            StopMoving(); 
            return;
        }

        float directionFactor = 1f;
        float speed = moveSpeed;

        if (movingLeft) directionFactor = -1f;
        if (sprinting) speed = sprintSpeed;

        body.velocity = new Vector2(directionFactor * speed, body.velocity.y);
    }

    private void StopMoving()
    {
        // Only halt horizontal velocity
        body.velocity = new Vector2(0.0f, body.velocity.y);
    }

}
