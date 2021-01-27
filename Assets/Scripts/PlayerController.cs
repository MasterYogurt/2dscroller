using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


        // Add upwards force to the player to jump if they are on the ground
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        
        // Check if the movement keys are pressed and save the state

        // Right
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            movingRight = true;
        }

        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            movingRight = false;
        }

        // Left
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            movingLeft = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
        {
            movingLeft = false;
        }

        // Sprinting
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
           sprinting = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            sprinting = false;
        }

        // Only apply new velocity if we are on the ground
        if (grounded) Move();

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
